using System;
using System.IO;
using System.Net.Sockets;

namespace AcceptanceTests
{
    /// <summary>
    /// Simple session
    /// </summary>
    public class SmtpSession
    {
        Socket _socket;
        private string _sessionProtocol = "";

        /// <summary>
        /// session protocol 
        /// </summary>
        public string SessionProtocol
        {
            get { return _sessionProtocol; }
            set { _sessionProtocol = value; }
        }



        /// <summary>
        /// process session
        /// </summary>
        public void Process()
        {
            NetworkStream networkStream = new NetworkStream(_socket);
            StreamWriter streamWriter = new StreamWriter(networkStream);
            StreamReader streamReader = new StreamReader(networkStream);
            streamWriter.AutoFlush = true;

            try
            {
                streamWriter.WriteLine("220 SMTP Mock Server Ready");
                bool datasent = false;

                while (_socket.Connected == true)
                {
                    string line = streamReader.ReadLine();

                    _sessionProtocol += line + "\n";

                    if (line == String.Empty)
                        continue;

                    if (line.ToUpper().StartsWith("QUIT"))
                    {
                        streamWriter.WriteLine("221 Service closing transmission channel");
                        break;

                    }
                    if (line.ToUpper().StartsWith("DATA"))
                    {
                        datasent = true;
                        streamWriter.WriteLine("354 Immediate Reply");
                    }
                    else if (datasent && line.Trim() == ".")
                    {
                        datasent = false;
                        streamWriter.WriteLine("250 OK");
                    }
                    else if (!datasent)
                    {
                        streamWriter.WriteLine("250 OK");
                    }
                }
            }
            catch (SocketException socketException)
            {
                Console.WriteLine(socketException.Message);
            }
            finally
            {
                streamReader.Close();
                streamWriter.Close();
                networkStream.Close();
                _socket.Close();
            }

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="socket"></param>
        public SmtpSession(Socket socket)
        {
            _socket = socket;
        }

    }
}
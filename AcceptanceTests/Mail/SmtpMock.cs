using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AcceptanceTests
{
    /// <summary>
    /// Simple SMTP Mock server
    /// </summary>
    public class SmtpMock
    {

        private TcpListener _smtpListener = null;
        private ArrayList _sessions = new ArrayList();

        /// <summary>
        /// list of all smtp sessions
        /// </summary>
        public SmtpSession[] Sessions
        {
            get { return (SmtpSession[])_sessions.ToArray(typeof(SmtpSession)); }
        }


        /// <summary>
        /// start server
        /// </summary>
        public void Start()
        {
            var smtpServerThread = new Thread(Run);
            smtpServerThread.Start();
        }

        /// <summary>
        /// stop server
        /// </summary>
        public void Stop()
        {
            if (_smtpListener != null)
                _smtpListener.Stop();
        }


        /// <summary>
        /// run server
        /// </summary>
        private void Run()
        {

            _smtpListener = new TcpListener(IPAddress.Any, 25); // open listener for port 
            _smtpListener.Start();

            try
            {
                while (true)
                {
                    Socket clientSocket = _smtpListener.AcceptSocket();
                    SmtpSession session = new SmtpSession(clientSocket);
                    _sessions.Add(session);
                    Thread sessionThread = new Thread(new ThreadStart(session.Process));
                    sessionThread.Start();
                    /*sessionThread.Join (); // remove comment  for more sessions at the same time*/
                }
            }
            catch (InvalidOperationException) { }
            finally
            {
                _smtpListener.Stop();
            }

        }
    }
}
using System;

namespace CommitStage
{
    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine("<" + DateTime.Now.ToLongTimeString() + "> " + message);
        }

        public void Log(string message, params object[] parameters)
        {
            Console.WriteLine("<" + DateTime.Now.ToLongTimeString() + "> " + string.Format(message, parameters));
        }
    }
}
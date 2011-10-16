namespace CommitStage
{
    public class ShellRemoteCommandConfig
    {
        public ShellRemoteCommandConfig()
        {
            WorkingPath = null;
        }

        public string User { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public string WorkingPath { get; set; }
    }
}
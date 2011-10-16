namespace CommitStage
{
    public class ShellRemoteCommand : IShellCommand
    {
        private readonly IShellCommand shellLocalCommand;

        private readonly ShellRemoteCommandConfig config;

        public ShellRemoteCommand(
            IShellCommand shellLocalCommand,
            ShellRemoteCommandConfig config)
        {
            this.shellLocalCommand = shellLocalCommand;
            this.config = config;
        }

        public string Execute(string command, bool throwExceptionIfFailed = true)
        {
            string formatted;

            if (string.IsNullOrEmpty(config.WorkingPath))
            {
                formatted = string.Format(
                    @"winrs -r:{0} {1}",
                    config.Server,
                    command);
            }
            else
            {
                formatted = string.Format(
                    @"winrs -r:{0} -d:""{1}"" {2}",
                    config.Server,
                    config.WorkingPath,
                    command);
            }

            return shellLocalCommand.Execute(
                formatted,
                throwExceptionIfFailed);
        }
    }
}
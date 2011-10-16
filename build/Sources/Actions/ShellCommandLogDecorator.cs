using System.IO;

namespace CommitStage
{
    public class ShellCommandLogDecorator : IShellCommand
    {
        private readonly IShellCommand command;
        private readonly Logger log;
        private readonly ShellLocalCommandConfig config;

        public ShellCommandLogDecorator(IShellCommand command, Logger log, ShellLocalCommandConfig config)
        {
            this.command = command;
            this.log = log;
            this.config = config;
        }

        public string Execute(string commandText, bool throwExceptionIfFailed = true)
        {
            log.Log(string.Format("Executing command '{0}'. Working directory '{1}'. Current folder '{2}'", commandText, config.WorkingDirectory, Directory.GetCurrentDirectory()));

            return command.Execute(commandText, throwExceptionIfFailed);
        }
    }
}
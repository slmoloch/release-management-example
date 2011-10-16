namespace CommitStage
{
    public class ShellCommandFactory : IShellCommandFactory
    {
        public IShellCommand Remote(ShellRemoteCommandConfig config)
        {
            return new ShellRemoteCommand(
                Local(new ShellLocalCommandConfig()),
                config);
        }

        public IShellCommand Local(ShellLocalCommandConfig config)
        {
            return new ShellCommandLogDecorator(
                new ShellLocalCommand(
                    new Logger(),
                    config),
                new Logger(),
                config);
        }

        public IShellCommand Local()
        {
            return Local(new ShellLocalCommandConfig());
        }
    }
}
namespace CommitStage
{
    public interface IShellCommandFactory
    {
        IShellCommand Remote(ShellRemoteCommandConfig config);

        IShellCommand Local(ShellLocalCommandConfig config);

        IShellCommand Local();
    }
}
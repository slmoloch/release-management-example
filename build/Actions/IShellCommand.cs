namespace CommitStage
{
    public interface IShellCommand
    {
        string Execute(string command, bool throwExceptionIfFailed = true);
    }
}
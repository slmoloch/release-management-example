namespace CommitStage
{
    public interface IMsBuildCompilerConfig
    {
        string MsBuildPath32 { get; }

        string MsBuildPath { get; }

        string MsBuildLogger { get; }

        string AspNetCompilerPath32 { get; }

        string AspNetCompilerPath { get; }
    }
}
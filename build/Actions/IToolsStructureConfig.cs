namespace CommitStage
{
    public interface IToolsStructureConfig
    {
        string ToolsRootPath { get; }

        string DotNetFramework64Path { get; }

        string DotNetFramework32Path { get; }
    }
}
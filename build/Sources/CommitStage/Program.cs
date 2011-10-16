using Actions;

namespace CommitStage
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();

            var logger = new Logger();

            new CommitStageBuild(
                new XslTransformRunner(),
                new MsBuildCompiler(new ToolsStructure(config)),
                new ShellCommandFactory(),
                new ToolsStructure(config),
                new BuildStructure(config),
                config,
                new FileSystemLogDecorator(new FileSystem(logger), logger),
                new ZipArchiver())
                .Run();
        }
    }
}
using Actions;

using CommitStage;

namespace Deployment
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();
            var logger = new Logger();

            new DeploymentBuild(
                new FileSystemLogDecorator(new FileSystem(logger), logger), 
                new ZipArchiver(), 
                new BuildStructure(config), 
                config).Run();
        }
    }
}

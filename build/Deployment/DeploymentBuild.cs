using System.IO;

using Actions;

using CommitStage;

namespace Deployment
{
    public class DeploymentBuild
    {
        private readonly IFileSystem fileSystem;
        private readonly ZipArchiver zip;
        private readonly BuildStructure buildStructure;
        private readonly Config config;

        public DeploymentBuild(IFileSystem fileSystem, ZipArchiver zip, BuildStructure buildStructure, Config config)
        {
            this.fileSystem = fileSystem;
            this.zip = zip;
            this.buildStructure = buildStructure;
            this.config = config;
        }

        public void Run()
        {
            fileSystem.DeleteFolderContents(config.DeploymentPath);

            zip.Unzip(
                Path.Combine(buildStructure.PackagePath, "build.zip"), 
                config.DeploymentPath);
        }
    }
}

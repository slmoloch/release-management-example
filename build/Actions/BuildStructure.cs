using System.IO;

using CommitStage;

namespace Actions
{
    public class BuildStructure
    {
        private readonly IBuildStructureConfig config;

        public BuildStructure(IBuildStructureConfig config)
        {
            this.config = config;
        }

        public string ReportsPath
        {
            get { return Path.Combine(BuildRootPath, "Reports"); }
        }

        public string BuildOutput
        {
            get { return Path.Combine(BuildRootPath, "Release"); }
        }

        public string PackagePath
        {
            get { return Path.Combine(BuildRootPath, "Package"); }
        } 
        
        public string TempPath
        {
            get { return Path.Combine(BuildRootPath, "Temp"); }
        }

        private string BuildRootPath
        {
            get { return config.BuildRootPath; }
        }
    }
}
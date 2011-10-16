using System.Configuration;

using CommitStage;

namespace Deployment
{
    public class Config : IToolsStructureConfig, IBuildStructureConfig
    {
        public string ToolsRootPath
        {
            get { return ConfigurationManager.AppSettings["ToolsRootPath"]; }
        }

        public string DotNetFramework64Path
        {
            get { return ConfigurationManager.AppSettings["DotNetFramework64Path"]; }
        }

        public string DotNetFramework32Path
        {
            get { return ConfigurationManager.AppSettings["DotNetFramework32Path"]; }
        }

        public string BuildRootPath
        {
            get { return ConfigurationManager.AppSettings["BuildRootPath"]; }
        }

        public string SourcesRootPath
        {
            get { return ConfigurationManager.AppSettings["SourcesRootPath"]; }
        }

        public string DeploymentPath
        {
            get { return ConfigurationManager.AppSettings["DeploymentPath"]; }
        }
    }
}
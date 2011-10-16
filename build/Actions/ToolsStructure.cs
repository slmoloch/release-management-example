using System.IO;

namespace CommitStage
{
    public class ToolsStructure :
        IMsBuildCompilerConfig,
        INUnitRunnerConfigAdapterConfig,
        IShellRemoteCommandStaticConfig
    {
        private readonly IToolsStructureConfig config;

        public ToolsStructure(IToolsStructureConfig config)
        {
            this.config = config;
        }

        public string MsBuildPath
        {
            get { return Path.Combine(config.DotNetFramework64Path, @"v4.0.30319\MSBuild.exe"); }
        }

        public string MsBuildPath32
        {
            get { return Path.Combine(config.DotNetFramework32Path, @"v4.0.30319\MSBuild.exe"); }
        }

        public string AspNetCompilerPath
        {
            get { return Path.Combine(config.DotNetFramework64Path, @"v4.0.30319\aspnet_compiler.exe"); }
        }

        public string AspNetCompilerPath32
        {
            get { return Path.Combine(config.DotNetFramework32Path, @"v4.0.30319\aspnet_compiler.exe"); }
        }

        public string MsBuildLogger
        {
            get
            {
                return @"ThoughtWorks.CruiseControl.MsBuild.XmlLogger," + Path.Combine(config.ToolsRootPath, @"MsBuildLogger\ThoughtWorks.CruiseControl.MsBuild.dll");
            }
        }

        public string NUnitPath
        {
            get { return Path.Combine(config.ToolsRootPath, @"NUnit\nunit-console-x86.exe"); }
        }

        public string MsBuildTransformPath
        {
            get { return Path.Combine(config.ToolsRootPath, @"Transformations\msbuild.xsl"); }
        }

        public string PsExecPath
        {
            get { return Path.Combine(config.ToolsRootPath, @"PsTools\psexec.exe"); }
        }
    }
}
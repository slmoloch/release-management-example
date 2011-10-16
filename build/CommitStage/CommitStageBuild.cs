using System.IO;

using Actions;

namespace CommitStage
{
    public class CommitStageBuild
    {
        private readonly XslTransformRunner xslTransformRunner;
        private readonly MsBuildCompiler msbuildCompiler;
        private readonly IShellCommandFactory factory;
        private readonly ToolsStructure toolsStructure;
        private readonly BuildStructure buildStructure;
        private readonly Config config;
        private readonly IFileSystem fileSystem;
        private readonly ZipArchiver zip;

        public CommitStageBuild(XslTransformRunner xslTransformRunner, MsBuildCompiler msbuildCompiler, IShellCommandFactory factory, ToolsStructure toolsStructure, BuildStructure buildStructure, Config config, IFileSystem fileSystem, ZipArchiver zip)
        {
            this.xslTransformRunner = xslTransformRunner;
            this.msbuildCompiler = msbuildCompiler;
            this.factory = factory;
            this.toolsStructure = toolsStructure;
            this.buildStructure = buildStructure;
            this.config = config;
            this.fileSystem = fileSystem;
            this.zip = zip;
        }

        public void Run()
        {
            fileSystem.CreateFolder(buildStructure.ReportsPath);
            fileSystem.CreateFolder(buildStructure.BuildOutput);
            fileSystem.CreateFolder(buildStructure.PackagePath);
            fileSystem.CreateFolder(buildStructure.TempPath);

            fileSystem.DeleteFolderContents(buildStructure.BuildOutput);

            Compile("Release");

            zip.ZipFolder(Path.Combine(buildStructure.PackagePath, "build.zip"), buildStructure.BuildOutput);
        }

        private void Compile(string configuration)
        {
            var sourcesPath = Path.GetFullPath(config.SourcesRootPath);
            var solutionPath = Path.Combine(sourcesPath, @"Representatives.sln");

            var reportPath = Path.Combine(buildStructure.ReportsPath, "Representatives.msbuild.xml");
            var reportHtmlPath = Path.Combine(buildStructure.ReportsPath, "Representatives.msbuild.html");

            try
            {
                msbuildCompiler.Command = factory.Local();

                msbuildCompiler.Build(solutionPath, reportPath, configuration, true, Path.GetFullPath(buildStructure.TempPath));

                fileSystem.CopyFolderContents(Path.Combine(buildStructure.TempPath, @"_PublishedWebsites", "RepresentativesSite"), buildStructure.BuildOutput);

                //var sitePath = Path.Combine(sourcesPath, @"RepresentativesSite");
                //msbuildCompiler.BuildWeb(sitePath, buildStructure.BuildOutput, true);
            }
            finally
            {
                xslTransformRunner.Run(reportPath, toolsStructure.MsBuildTransformPath, reportHtmlPath);
            }
        }
    }
}
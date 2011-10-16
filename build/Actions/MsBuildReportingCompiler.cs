using System.IO;

using Actions;

namespace CommitStage
{
    public class MsBuildReportingCompiler
    {
        private readonly IShellCommandFactory factory;
        private readonly BuildStructure buildStructure;
        private readonly MsBuildCompiler msbuildCompiler;
        private readonly XslTransformRunner xslTransformRunner;
        private readonly ToolsStructure toolsStructure;

        public MsBuildReportingCompiler(
            IShellCommandFactory factory,
            BuildStructure buildStructure,
            MsBuildCompiler msbuildCompiler,
            XslTransformRunner xslTransformRunner,
            ToolsStructure toolsStructure)
        {
            this.factory = factory;
            this.buildStructure = buildStructure;
            this.msbuildCompiler = msbuildCompiler;
            this.xslTransformRunner = xslTransformRunner;
            this.toolsStructure = toolsStructure;
        }

        public void Run(string solutionPath, string configuration, bool? targetX64 = null)
        {
            var fileName = Path.GetFileName(solutionPath);
            var reportPath = Path.Combine(buildStructure.ReportsPath, fileName + ".msbuild.xml");
            var reportHtmlPath = Path.Combine(buildStructure.ReportsPath, fileName + ".msbuild.html");

            try
            {
                msbuildCompiler.Command = factory.Local();
                msbuildCompiler.Build(solutionPath, reportPath, configuration, targetX64: targetX64);
            }
            finally
            {
                xslTransformRunner.Run(reportPath, toolsStructure.MsBuildTransformPath, reportHtmlPath);
            }
        }
    }
}
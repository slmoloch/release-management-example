namespace CommitStage
{
    public class MsBuildCompiler
    {
        private readonly IMsBuildCompilerConfig config;

        public MsBuildCompiler(IMsBuildCompilerConfig config)
        {
            this.config = config;
        }

        public IShellCommand Command { get; set; }

        public void Build(string solutionPath, string reportPath, string configuration, bool bit32Compilation = false, string outDir = null, bool? targetX64 = null)
        {
            var msbuildPath = bit32Compilation
                ? config.MsBuildPath32
                : config.MsBuildPath;

            Command.Execute(string.Format(
                @"{0} /nologo ""{1}"" /t:Clean,Rebuild /p:Configuration={5} {6} {4} /logger:{2};{3}",
                msbuildPath,
                solutionPath,
                config.MsBuildLogger,
                reportPath,
                GetOutDirectoryOption(outDir),
                configuration,
                GetTargetPlatform(targetX64)));
        }

        public void BuildWeb(string sitePath, string outDir, bool bit32Compilation = false)
        {
            var aspnetCompilerPath = bit32Compilation
                ? config.AspNetCompilerPath32
                : config.AspNetCompilerPath;

            Command.Execute(
                string.Format(
                    @"{0} -nologo /v myapp -p ""{1}"" {2}",
                    aspnetCompilerPath,
                    sitePath,
                    outDir));
        }

        private static string GetTargetPlatform(bool? targetX64)
        {
            return !targetX64.HasValue
                       ? string.Empty
                       : (targetX64.Value
                              ? "/p:Platform=x64"
                              : "/p:Platform=x86");
        }

        private static string GetOutDirectoryOption(string outDir)
        {
            return string.IsNullOrEmpty(outDir)
                ? string.Empty
                : @"/p:OutDir=""" + outDir + @"\\""";
        }
    }
}
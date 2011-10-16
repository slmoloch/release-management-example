using System;
using System.Diagnostics;

namespace CommitStage
{
    public class ShellLocalCommand : IShellCommand
    {
        private readonly Logger log;
        private readonly ShellLocalCommandConfig config;

        public ShellLocalCommand(Logger log, ShellLocalCommandConfig config)
        {
            this.log = log;
            this.config = config;
        }

        public string Execute(string command, bool throwExceptionIfFailed = true)
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                ErrorDialog = false,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = config.WorkingDirectory
            };
            using (var proc = new Process { StartInfo = processStartInfo })
            {
                proc.Start();

                var output = ReadOutput(proc);

                if (output != string.Empty)
                {
                    log.Log("Exec Output -----------------------------------------------------------------------------------------");
                    log.Log(output);
                    log.Log("-----------------------------------------------------------------------------------------------------");
                }

                var exitCode = proc.ExitCode;
                proc.Close();

                if (exitCode != 0 && throwExceptionIfFailed)
                {
                    throw new InvalidOperationException("Executed application returned error.");
                }

                return output;
            }
        }

        private static string ReadOutput(Process proc)
        {
            var standardOutput = proc.StandardOutput.ReadToEnd();
            var errorOutput = proc.StandardError.ReadToEnd();

            proc.WaitForExit();

            return standardOutput + errorOutput;
        }
    }

}

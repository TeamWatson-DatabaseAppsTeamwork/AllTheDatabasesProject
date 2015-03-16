namespace TestMongoScriptsClass
{
    using System;
    using System.Diagnostics;

    public class ExecuteBatchFileClass
    {
        ExecuteBatchFile("test", 5000, false);
        private int ExecuteBatchFile(string batchFilePath, int timeout, bool killOnTimeout)
        {
            using (var p = Process.Start(batchFilePath))
            {
                p.WaitForExit(timeout);

                if (p.HasExited)
                    return p.ExitCode;

                if (killOnTimeout)
                {
                    p.Kill();
                }
                else
                {
                    p.CloseMainWindow();
                }

                throw new TimeoutException(string.Format("Time allotted for executing `{0}` has expired ({1} ms).", batchFilePath, timeout));
            }
        }
    }
}

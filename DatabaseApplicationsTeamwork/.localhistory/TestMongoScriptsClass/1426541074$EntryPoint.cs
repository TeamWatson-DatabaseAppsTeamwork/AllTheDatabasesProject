namespace TestMongoScriptsClass
{
    using System;

    using System.Diagnostics;

    class EntryPoint
    {
        static void Main(string[] args)
        {
            string batchFilePath = "";
            int timeout = 5000;
            bool killOnTimeout = false;


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

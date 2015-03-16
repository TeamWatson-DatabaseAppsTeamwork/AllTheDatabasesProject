namespace TestMongoScriptsClass
{
    using System;
    using System.Diagnostics;

    public class ExecuteBatchFileClass
    {
        public void Run()
        {
            const string BatchFilePathStartMongo = "W:\\_SoftUni\\GitHub\\DB-Apps-TW\\AllTheDatabasesProject\\DatabaseApplicationsTeamwork\\MongoScripts\\mongodStart.bat";
            const string BatchFilePathMain = "W:\\_SoftUni\\GitHub\\DB-Apps-TW\\AllTheDatabasesProject\\DatabaseApplicationsTeamwork\\MongoScripts\\mongoImportArrayJsonS.bat";
            this.ExecuteBatchFile(BatchFilePathStartMongo, BatchFilePathMain);
            //var test = this.ExecuteBatchFile(BatchFilePathStartMongo, 5000, false, BatchFilePathMain);
        }

        private void ExecuteBatchFile(string batchFilePathStart, string batchFilePathMain)
        {
            var p = Process.Start(batchFilePathStart);

            var q = Process.Start(batchFilePathMain);
            q.CloseMainWindow();
            p.CloseMainWindow();
        }


        //private int ExecuteBatchFile(string batchFilePathStart, int timeout, bool killOnTimeout, string batchFilePathMain)
        //{
        //    var p = Process.Start(batchFilePathStart);

        //    using (var q = Process.Start(batchFilePathMain))
        //    {
        //        q.WaitForExit(timeout);

        //        if (q.HasExited)
        //        {
        //            return p.ExitCode;
        //        }

        //        if (killOnTimeout)
        //        {
        //            q.Kill();
        //        }
        //        else
        //        {
        //            q.CloseMainWindow();
        //            p.CloseMainWindow();
        //        }

        //        throw new TimeoutException(string.Format("Time allotted for executing `{0}` has expired ({1} ms).", batchFilePathStart, timeout));
        //    }
        //}
    }
}

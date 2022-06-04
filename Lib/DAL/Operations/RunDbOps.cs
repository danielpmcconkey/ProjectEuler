using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;

namespace EulerProblems.Lib.DAL.Operations
{
    public class RunDbOps
    {
        public static void WriteNewRun(Run r)
        {
            using (var db = new EulerContext())
            {
                db.Runs.Add(r);
                db.SaveChanges();
            }
        }
    }
}

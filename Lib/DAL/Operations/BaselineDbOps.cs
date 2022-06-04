using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;

namespace EulerProblems.Lib.DAL.Operations
{
    internal static class BaselineDbOps
    {
        internal static int[] FetchProblemsWithoutBaselines()
        {
            using (var db = new EulerContext())
            {
                var problemsWithoutBaselines = from probs in db.Problems
                                                join bases in db.Baselines
                                                on probs.id equals bases.id
                                                into leftSide
                                                from rightSide in leftSide.DefaultIfEmpty()
                                                where rightSide == null
                                                select probs.id;
                return problemsWithoutBaselines.ToArray();
            }
        }
        internal static void WriteNewBaseline(Baseline b)
        {
            using (var db = new EulerContext())
            {
                db.Baselines.Add(b);
                db.SaveChanges();
            }
        }
    }
}

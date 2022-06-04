using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;

namespace EulerProblems.Lib.DAL.Operations
{
    public static class RunDbOps
    {
        public static void Create(Run r)
        {
            using (var context = new EulerContext())
            {
                Create(r, context);
            }
        }
        public static void Create(Run r, EulerContext existingContext)
        {
            existingContext.Runs.Add(r);
            existingContext.SaveChanges();
        }
        public static void DeleteByProblem(int problem)
        {
            using (var context = new EulerContext())
            {
                DeleteByProblem(problem, context);
            }
        }
        public static void DeleteByProblem(int problem, EulerContext existingContext)
        {
            existingContext.Runs.RemoveRange(existingContext.Runs.Where(x => x.problem == problem));
            existingContext.SaveChanges();
        }
        public static Run[] ReadByProblem(int problem)
        {
            using (var context = new EulerContext())
            {
                return ReadByProblem(problem, context);
            }
        }
        public static Run[] ReadByProblem(int problem, EulerContext existingContext)
        {
            return existingContext.Runs.Where(x => x.problem == problem).ToArray();
        }
    }
}

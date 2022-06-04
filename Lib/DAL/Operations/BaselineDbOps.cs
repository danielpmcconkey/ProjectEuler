using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;

namespace EulerProblems.Lib.DAL.Operations
{
    public static class BaselineDbOps
    {
        public static void Create(Baseline b)
        {
            using (var context = new EulerContext())
            {
                Create(b, context);
            }
        }
        public static void Create(Baseline b, EulerContext existingContext)
        {
            existingContext.Baselines.Add(b);
            existingContext.SaveChanges();
        }
        public static Baseline[] Read()
        {
            using (var context = new EulerContext())
            {
                return Read(context);
            }
        }
        public static Baseline[] Read(EulerContext existingContext)
        {
            return existingContext.Baselines.ToArray();
        }
        /// <summary>
        /// reads all problem IDs with no corresponding baselines
        /// </summary>
        public static int[] ReadMissing()
        {
            using (var context = new EulerContext())
            {
                return ReadMissing(context);
            }
        }
        /// <summary>
        /// reads all problem IDs with no corresponding baselines
        /// </summary>
        public static int[] ReadMissing(EulerContext existingContext)
        {
            var problemsWithoutBaselines = from probs in existingContext.Problems
                                            join bases in existingContext.Baselines
                                            on probs.id equals bases.id
                                            into leftSide
                                            from rightSide in leftSide.DefaultIfEmpty()
                                            where rightSide == null
                                            select probs.id;
            return problemsWithoutBaselines.ToArray();
            
        }

    }
}

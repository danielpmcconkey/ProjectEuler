using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;
using EulerProblems.Lib.DAL.Operations;

namespace EulerProblems.Lib
{
    public static class TestHelper
    {
        public static void BenchMarkNewSolutions()
        {
            int[] problemsWithoutBenchmarks = BaselineDbOps.ReadMissing();
            foreach (int id in problemsWithoutBenchmarks)
            {
                BenchMarkPerformanceResults(id);
            }
        }
        public static void AddRunsForBenchmarking()
        {
            Problem[] problems = ProblemDbOps.ReadSolved();
            foreach (Problem p in problems)
            {
                AddRuns(p.id);
            }
        }
        public static void BenchMarkPerformanceResults(int problemId)
        {
            const int howManyRuns = 100;
            
            List<(string solution, TimeSpan runTime)> runResults = 
                new List<(string solution, TimeSpan runTime)>();
            for (int i = 0; i < howManyRuns; i++)
            {
                var euler = EulerProblemFactory.GetEulerProblemClassByNumber(problemId);
                runResults.Add(euler.Solve(true));
            }
            double averageDuration = runResults.Average(x => x.runTime.TotalMilliseconds);
            double percentile90 = runResults.OrderByDescending(x => x.runTime)
                .ToArray()[9].runTime.TotalMilliseconds;
            Console.WriteLine(string.Format("problemNumber: {0}; averageDuration: {1} milliseconds; percentile90: {2} milliseconds",
                problemId,
                averageDuration,
                percentile90
                ));
            // write to the DB
            Baseline baseline = new Baseline()
            {
                id = problemId,
                averageDuration = averageDuration,
                percentile90Duration = percentile90
            };
            BaselineDbOps.Create(baseline);
        }
        public static void AddRuns(int problemId, EulerContext existingContext = null)
        {
            const int howManyRuns = 100;

            Console.WriteLine("Adding runs for {0}", problemId);

            List<(string solution, TimeSpan runTime)> runResults =
                new List<(string solution, TimeSpan runTime)>();
            for (int i = 0; i < howManyRuns; i++)
            {
                var euler = EulerProblemFactory.GetEulerProblemClassByNumber(problemId);
                var result = euler.Solve(true);
                runResults.Add(result);
                Run run = new Run() { problem = problemId, duration = result.runTime.TotalMilliseconds };
                if (existingContext == null) RunDbOps.Create(run);
                else RunDbOps.Create(run, existingContext);
            }            
        }
        public static void ReBaselineProblem(int problemId)
        {
            // wrap this in a transaction so we don't delete and then fail to rewrite
            using(var context = new EulerContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // delete and re-run
                        RunDbOps.DeleteByProblem(problemId, context);
                        AddRuns(problemId, context);
                        var runs = RunDbOps.ReadByProblem(problemId, context);
                        // determine new average and 90th percentile
                        int cutoffFor90 = (int)Math.Round(runs.Length * 0.1, 0);
                        double averageDuration = runs.Average(x => x.duration);
                        double percentile90 = runs.OrderByDescending(x => x.duration)
                            .ToArray()[cutoffFor90].duration;
                        // update the baseline table
                        var rowToUpdate = context.Baselines.Where(x => x.id == problemId).FirstOrDefault();
                        if (rowToUpdate == null)
                        {
                            context.Add(new Baseline()
                            {
                                id = problemId,
                                averageDuration = averageDuration,
                                percentile90Duration = percentile90
                            });
                        }
                        else
                        {
                            rowToUpdate.averageDuration = averageDuration;
                            rowToUpdate.percentile90Duration = percentile90;
                        }
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

﻿using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;
using EulerProblems.Lib.DAL.Operations;

namespace EulerProblems.Lib
{
    public static class TestHelper
    {
        public static void BenchMarkNewSolutions()
        {
            int[] problemsWithoutBenchmarks = BaselineDbOps.FetchProblemsWithoutBaselines();
            foreach (int id in problemsWithoutBenchmarks)
            {
                BenchMarkPerformanceResults(id);
            }            
        }
        public static void AddRunsForBenchmarking()
        {
            Problem[] problems = ProblemDbOps.FetchSolvedProblems();
            foreach (Problem p in problems)
            {
                AddRuns(p.id);
            }
        }
        private static void BenchMarkPerformanceResults(int problemId)
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
            BaselineDbOps.WriteNewBaseline(baseline);
        }
        private static void AddRuns(int problemId)
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
                RunDbOps.WriteNewRun(run);
            }            
        }
    }
}

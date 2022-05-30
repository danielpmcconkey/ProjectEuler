using EulerProblems.Lib.DAL.Data;

namespace EulerProblems.Lib
{
    public static class TestHelper
    {
        public static void BenchMarkPerformanceResults()
        {
            const int howManyRuns = 100;

            using (var db = new EulerContext())
            {
                foreach (var row in db.Problems)
                {
                    List<(string solution, TimeSpan runTime)> runResults = new List<(string solution, TimeSpan runTime)>();
                    for (int i = 0; i < howManyRuns; i++)
                    {
                        var euler = EulerProblemFactory.GetEulerProblemClassByNumber(row.id);
                        runResults.Add(euler.Solve(true));
                    }
                    double percentile90 = runResults.OrderByDescending(x => x.runTime)
                        .ToArray()[9].runTime.TotalMilliseconds;
                    Console.WriteLine(string.Format("problemNumber: {0}; averageDuration: {1} milliseconds; percentile90: {2} milliseconds",
                        row.id,
                        runResults.Average(x => x.runTime.TotalMilliseconds),
                        percentile90
                        ));
                }
            }
        }
    }
}

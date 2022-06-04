#define SHOULDRUNPERFORMANCE

using EulerProblems.Lib;
using EulerProblems.Lib.DAL.Operations;

namespace TestBench
{
#if SHOULDRUNPERFORMANCE
    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        public void CheckPerformance()
        {
            const int howManyRuns = 100;
            var baselines = BaselineDbOps.Read();

            int numFailed = 0;

            foreach (var baseline in baselines)
            {
                Console.WriteLine(baseline.id);
                List<(string solution, TimeSpan runTime)> runResults =
                    new List<(string solution, TimeSpan runTime)>();
                for (int i = 0; i < howManyRuns; i++)
                {
                    var euler = EulerProblemFactory.GetEulerProblemClassByNumber(baseline.id);
                    runResults.Add(euler.Solve(true));
                }
                double averageDuration = runResults.Average(x => x.runTime.TotalMilliseconds);
                double percentile90 = runResults.OrderByDescending(x => x.runTime)
                    .ToArray()[9].runTime.TotalMilliseconds;

                double averageExpected = baseline.averageDuration * 1.25;
                double percentile90Expected = baseline.percentile90Duration * 1.5;

                Console.WriteLine(string.Format("average: {0}/{1} | 90th%ile {2}/{3}",
                    averageDuration, averageExpected,
                    percentile90, percentile90Expected));

                if(averageDuration > averageExpected || percentile90 > percentile90Expected)
                {
                    Console.WriteLine("FAILED");
                    numFailed++;
                }

                Console.WriteLine();

            }
            Assert.AreEqual<int>(numFailed, 0);
        } 
    }
#endif
}

using EulerProblems.Lib;
using System.Text.Json;

namespace TestBench
{
    [TestClass]
    public class RegressionTest
    {
        const string testExpectationsFile = @"E:\ProjectEuler\TestExpectations\TestExpectations.dat";

        [TestMethod]
        public void TestMethod1()
        {
            string jsonResults = File.ReadAllText(testExpectationsFile);
            List<ExpectedResult> expectedResults = JsonSerializer
                .Deserialize<List<ExpectedResult>>(jsonResults);
            
            foreach (var expectedResult in expectedResults)
            {
                var euler = EulerProblemFactory.GetEulerProblemClassByNumber(expectedResult.problemNumber);
                var result = euler.Solve();
                Assert.AreEqual(expectedResult.solution, result.solution);
                Assert.IsTrue(result.runTime <= expectedResult.maxDuration);
                Console.WriteLine();
            }
        }
    }
}
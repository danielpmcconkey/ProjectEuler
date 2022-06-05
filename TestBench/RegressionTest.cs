#define SHOULDRUNREGRESSION

using EulerProblems.Lib;
using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Models;
using EulerProblems.Lib.DAL.Operations;
using System.Text.Json;

namespace TestBench
{
#if SHOULDRUNREGRESSION
    [TestClass]
    public class RegressionTest
    {
        [TestMethod]
        public void CheckSolutions()
        {
            Problem[] problems = ProblemDbOps.ReadSolved();

            foreach (var problem in problems)
            {
                Console.WriteLine(problem.id);

                var euler = EulerProblemFactory.GetEulerProblemClassByNumber(problem.id);
                var result = euler.Solve();
                Assert.AreEqual(problem.solution, result.solution);
                Console.WriteLine();
            }

        }
    } 
#endif
}
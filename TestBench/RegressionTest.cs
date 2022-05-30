using EulerProblems.Lib;
using EulerProblems.Lib.DAL.Data;
using System.Text.Json;

namespace TestBench
{
    [TestClass]
    public class RegressionTest
    {
        [TestMethod]
        public void CheckSolutions()
        {
            using (var db = new EulerContext())
            {
                foreach (var row in db.Problems)
                {
                    Console.WriteLine(row.id);

                    var euler = EulerProblemFactory.GetEulerProblemClassByNumber(row.id);
                    var result = euler.Solve();
                    Assert.AreEqual(row.solution, result.solution);
                    Console.WriteLine();
                }
            }  
        }
    }
}
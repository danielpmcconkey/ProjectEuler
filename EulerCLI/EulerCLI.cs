using EulerProblems.Lib;
using EulerProblems.Lib.Problems;
using System.Text.Json;
using EulerProblems.Lib.DAL.Data;



//TemplateManager.CreateNewProblemFilesFromTemplate(48, 100);
//TestHelper.AddRunsForBenchmarking();
//TestHelper.AddRuns(9);
TestHelper.ReBaselineProblem(60);
//TestHelper.BenchMarkNewSolutions();

var euler = EulerProblemFactory.GetEulerProblemClassByNumber(60);
euler.Solve();





using EulerProblems.Lib;
using EulerProblems.Lib.Problems;
using System.Text.Json;
using EulerProblems.Lib.DAL.Data;
using EulerProblems.Lib.DAL.Operations;



//TemplateManager.CreateNewProblemFilesFromTemplate(48, 100);
//TestHelper.AddRunsForBenchmarking();
//TestHelper.AddRuns(9);
//TestHelper.ReBaselineProblem(73);
//TestHelper.BenchMarkNewSolutions();


var euler = EulerProblemFactory.GetEulerProblemClassByNumber(10);
euler.Solve();





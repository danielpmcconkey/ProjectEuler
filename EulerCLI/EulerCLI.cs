using EulerProblems.Lib;
using EulerProblems.Lib.Problems;
using System.Text.Json;
using EulerProblems.Lib.DAL.Data;

//TemplateManager.CreateNewProblemFilesFromTemplate(48, 100);
TestHelper.BenchMarkNewSolutions();

var euler = EulerProblemFactory.GetEulerProblemClassByNumber(56);
euler.Solve();





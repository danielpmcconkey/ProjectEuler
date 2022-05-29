using EulerProblems.Lib;
using EulerProblems.Lib.Problems;

const string testExpectationsFile = @"E:\ProjectEuler\TestExpectations\TestExpectations.dat";


// TemplateManager.CreateNewProblemFilesFromTemplate(45, 100);

var euler = EulerProblemFactory.GetEulerProblemClassByNumber(2);
euler.Solve();

﻿using EulerProblems.Lib;
using EulerProblems.Lib.Problems;
using System.Text.Json;
using EulerProblems.Lib.DAL.Data;



//TemplateManager.CreateNewProblemFilesFromTemplate(48, 100);
//TestHelper.AddRunsForBenchmarking();
//TestHelper.AddRuns(9);
TestHelper.ReBaselineProblem(29);
//TestHelper.BenchMarkNewSolutions();

var euler = EulerProblemFactory.GetEulerProblemClassByNumber(9);
euler.Solve();





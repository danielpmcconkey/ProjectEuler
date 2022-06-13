open System
open Euler004

let stopWatch = System.Diagnostics.Stopwatch()
stopWatch.Start()


run()


stopWatch.Stop()
printfn "run time: %d ms" stopWatch.ElapsedMilliseconds



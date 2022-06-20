open System
open Euler002

let stopWatch = System.Diagnostics.Stopwatch()
stopWatch.Start()



let (answer : int) = run() 

stopWatch.Stop()
printfn "answer is: %d" answer
printfn "run time: %d ms" stopWatch.ElapsedMilliseconds



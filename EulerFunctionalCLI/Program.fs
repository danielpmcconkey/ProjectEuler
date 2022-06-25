open System
open Euler076

let stopWatch = System.Diagnostics.Stopwatch()
stopWatch.Start()



let (answer : string) = run() 

stopWatch.Stop()
printfn "answer is: %s" answer
printfn "run time: %d ms" stopWatch.ElapsedMilliseconds



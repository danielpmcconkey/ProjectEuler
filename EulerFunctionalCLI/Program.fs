open System
open Euler074

let stopWatch = System.Diagnostics.Stopwatch()
stopWatch.Start()



let (answer : string) = run() 

stopWatch.Stop()
printfn "answer is: %s" answer
printfn "run time: %d ms" stopWatch.ElapsedMilliseconds



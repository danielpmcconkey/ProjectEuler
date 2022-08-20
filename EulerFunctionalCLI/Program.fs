let stopWatch = System.Diagnostics.Stopwatch()
stopWatch.Start()

let (answer : string) = Euler070.run() 

stopWatch.Stop()
printfn "answer is: %s" answer
printfn "run time: %d ms" stopWatch.ElapsedMilliseconds



module Euler012

let run () =

    (* 
    This one was a good lesson for me in F# control flow. It's not hard to 
    write idiomatically functional code that produces a correct answer but it
    is hard to do so in a reasonable time. I tried many versions and kept 
    getting nowhere close to finishing under a minute.
    
    The problem is that any function that calculates the number of divisors, in
    order to run efficiently, needs to be able to break out of a loop. By 
    definition, F# doesn't support anything like a break statement. You can use
    mutable variables and while statements to simulate this. That's what the 
    getDivisorsCountNotFP function below does. But that just feels so wrong in
    a functional language. Yes, F# is a hybrid and it *is* allowable. (I could
    also have just called the C# EulerProblems.Lib.CommonAlgorithms.GetFactors
    function from within F#, too) but that wouldn't have taught me anything rad
    about functional thinking.

    The solution I ultimately landed on uses Seq.unfold to build sequences 
    until a target was hit. For the getDivisorsCount function, that target was
    when my state's i variable matched by lowest opposite factor. And for the 
    main triangles code block, that was when it calculated a triangle number
    that had 500 or more divisors. Note, that's the same logic as the C#
    solution. But the implementation is ever so different.
    *)

    let target = 500
    

    let getDivisorsCountNotFP n =
        // this function mimics the C# control flow and is *definitely* not 
        // idiomatically FP. But it *is* 10 times faster
        if n = 1 then
            1
        else
            let mutable factorsCount = 0
            let maxVal = (n / 2)
            let mutable lowestOppositeFactor = n
            let mutable solutionNotFound = true
            let mutable i = 1
            while i <= maxVal && solutionNotFound do
                if i >= lowestOppositeFactor then
                    solutionNotFound <- false
                else
                    solutionNotFound <- true
                    if n % i = 0 then
                        factorsCount <- factorsCount + 1
                        let oppositeFactor = n / i
                        if oppositeFactor <> i then factorsCount <- factorsCount + 1
                        lowestOppositeFactor <- oppositeFactor
                    i <- i + 1
            factorsCount

    let getDivisorsCount n = 
        // this is the fastest idiomatically FP function I could create. it 
        // runs remarkably fast compared to other FP attempts (which clocked in
        // at about a half hour for the full problem to run.
        if n = 1 then 1
        else
            let last = 
                (1, n, 0)
                |> Seq.unfold (fun state ->
                    let i, lowestOpposite, numFactors = state
                    if i >= lowestOpposite then 
                        None
                    elif n % i = 0 then
                        let opposite = n / i
                        if opposite = i then
                            Some(state, (i + 1, opposite, numFactors + 1))
                        else
                            Some(state, (i + 1, opposite, numFactors + 2))
                    else                
                        Some(state, (i + 1, lowestOpposite, numFactors)))
                |> Seq.last
            let i, lowestOpposite, numFactors = last
            numFactors


    let triangles = 
        (1, 0, 0)
        |> Seq.unfold (fun state ->
            let i, totalSum, lastNumDivisors = state
            if lastNumDivisors > target then
                None
            else
                let iNext = i + 1
                let sumNext = totalSum + i
                let thisNumDivisors = getDivisorsCount sumNext
                Some(state, (iNext, sumNext, thisNumDivisors)))
    let i, triangleNum, numDivisors = Seq.last triangles
    let answer = triangleNum + i

    answer.ToString()
    

    
    

    //let answer = newNumDivisors 76576500 // 685 milliseconds
    //let answer = EulerProblems.Lib.CommonAlgorithms.GetFactors(76576500L).Length
    //answer.ToString()
    



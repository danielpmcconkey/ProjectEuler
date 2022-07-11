module Euler009

let run () =
    
    let target = 1000
    let largestValueToSquare = target / 2
    let perfectSquares = seq { for i in 1 .. largestValueToSquare -> i * i }
    

    let isPerfectSquare (n:int) = 
        let root = int (floor (sqrt (float n * 1.0)))
        if root * root = n then true else false

    let isWinner aSquared bSquared = 
        if aSquared = 0 || bSquared = 0 then false
        else
            let cSquared = aSquared + bSquared
            if isPerfectSquare cSquared 
                then 
                    let a = sqrt (float aSquared)
                    let b = sqrt (float bSquared)
                    let c = sqrt (float cSquared)
                    if a + b + c = 1000 
                        then true
                        else false
                else false

    let createCrossOfWinners a =      
        Array.create (Seq.length perfectSquares) a
        |> Seq.zip perfectSquares
        |> Seq.filter (fun (t) -> isWinner (fst t) (snd t))    

    let winners = 
        Seq.init largestValueToSquare (fun c -> createCrossOfWinners (c * c))
        |> Seq.filter (fun x -> Seq.length x > 0)
        |> Seq.toArray

    let calculatePythagoreanProduct t =
        let aSquared = fst t
        let bSquared = snd t
        let cSquared = aSquared + bSquared
        let a = sqrt (float aSquared)
        let b = sqrt (float bSquared)
        let c = sqrt (float cSquared)
        int (a * b * c)

    let firstWinner = winners[0] |> Seq.toArray |> Seq.min
    let answer = calculatePythagoreanProduct firstWinner


    answer.ToString()
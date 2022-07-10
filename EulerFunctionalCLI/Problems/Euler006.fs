module Euler006


let run () =

    
    let limit = 100L
    let square n = n * n

    let sumOfSquares n = 
        seq { for i in 1L .. n -> square i }
        |> Seq.sum
    let squareOfSums n = 
        seq { 1L .. n }
        |> Seq.sum
        |> square
    let answer = squareOfSums limit - sumOfSquares limit

    answer.ToString()
  
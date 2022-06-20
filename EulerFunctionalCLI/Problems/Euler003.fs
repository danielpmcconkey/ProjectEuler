module Euler003

let run () =

    let numberToCheck : int64 = 600851475143L
    let answer = Algorithms.primeFactorsLong numberToCheck |> Seq.max
    answer.ToString()
    




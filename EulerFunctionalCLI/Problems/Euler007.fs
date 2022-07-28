module Euler007
open Primes

let run () =

    let limit = 10001
    let primes = getFirstNPrimes limit
    let answer = primes[limit - 1]
    answer.ToString()
  
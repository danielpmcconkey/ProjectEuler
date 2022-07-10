module Euler007

let run () =

    let limit = 10001
    let primes = Algorithms.getFirstNPrimes limit
    let answer = primes[limit - 1]
    answer.ToString()
  
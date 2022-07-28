module Euler077


let run () =
    
    let target = 5000
    let primesPack = Algorithms.getPrimesUpToNSieve target
    let primes = primesPack.primes
    let primeBools = primesPack.primeBools

    let cache = Array.create target -1
    let rec primePartition n primes =
        if n = 1 then 0
        elif cache[n] <> -1 then cache[n]
        else 
            let sopfN = Algorithms.sumOfPrimeFactors n primes
            let stateForward state j sum =
                let nextJ = j - 1
                if j < 1 then None
                else
                    let sopfJ = Algorithms.sumOfPrimeFactors j primes
                    let subPrimePartition = primePartition (n - j) primes
                    //printfn "j = %d | sopfJ = %d | partition = %d" j sopfJ subPrimePartition
                    let product = sopfJ * subPrimePartition
                    Some(state, (nextJ, sum + product))
            let sumPrimePartiton =
                (n - 1, sopfN) // initial state = j, the unfolding sum
                |> Seq.unfold (fun state -> 
                    let j = fst state 
                    let sum = snd state 
                    stateForward state j sum
                )
            let result = (sumPrimePartiton |> Seq.last |> snd) / n
            cache[n] <- result
            //printfn "result written: %d = %d" n result
            result
    let add1 n = n + 1

    let start = 10
    let answer = 
        start // initial state
        |> Seq.unfold (fun i ->
            let primePartitionOfI = if primeBools[i] then (primePartition i primes) - 1 else primePartition i primes
            if primePartitionOfI > target then None
            else Some(i, i + 1)
        ) 
        |> Seq.last
        |> add1

    
    answer.ToString()
    
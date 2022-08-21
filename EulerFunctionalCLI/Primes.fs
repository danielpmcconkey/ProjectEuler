module Primes
open DomainTypes


let isPrime n = 
    if n = 2 then true
    else
        let sqrtN = (int)(ceil (sqrt ((float)n)))
        [|2..sqrtN|] |> Array.forall (fun x -> n % x <> 0)
let getPrimesUpToNSimple n = 
    let primeBools = [|0..n|] |> Array.map (fun x -> isPrime x)
    let primes = [|2..n|] |> Array.filter (fun x -> primeBools[x])
    (primes, primeBools )
let getPrimesUpToNSieve n =

    let limit = ((n - 2) / 2) + 1
    let sieve = Array.zeroCreate (limit + 1)

    let updateSieve () = 
        let updateSieveForJVals i = 
            let iVals = seq {i..(limit - 1)}
            let setSievePositionToOne j =
                sieve[i + j + 2 * i * j] <- 1
            let isWithinJLimit (i:uint64) (j:uint64) = 
                // this exists to check if j is large enough to cause int overrun
                // in f#, it seems to evaluate the (i + j + 2 * i * j) expression
                // as an int before comparing to limit. Which means that high i and
                // j combinations will overrun an int down to a negative int. I 
                // don't know why I don't see this is C#
                ( i + j + 2UL *  i * j) < uint64 limit
            let jVals =
                iVals
                |> Seq.takeWhile (fun j -> (isWithinJLimit (uint64 i) (uint64 j)) )
                |> Seq.iter (fun j -> setSievePositionToOne j)
            ()

        seq {1..(limit - 1)}
        |> Seq.iter (fun i -> (updateSieveForJVals i))
        ()
    updateSieve ()
    
    let primeBools = 
        [|0..n|]
        |> Array.map (fun x -> 
            if x = 1 then false
            elif x = 2 then true 
            else
                let sievePlaceF = (((float)x) - 1.0) * 0.5
                if sievePlaceF % 1.0 = 0 then
                    let sievePlaceI = (int)sievePlaceF
                    if sieve[sievePlaceI] = 0 then true else false
                else false
        )
    let primes = [|2..n|] |> Array.filter (fun x -> primeBools[x])

    { primes = primes; primeBools = primeBools }
let getFirstNPrimes n =  
    // this is sketch
    let primeTuples = 
        (0, 3, 2)
        |> Seq.unfold (fun state ->
            let count, checkVal, lastPrime = state

            if count >= n then
                None
            else
                let nextCheckVal = checkVal + 1

                if (isPrime checkVal) then
                    //primes[count] <- checkVal
                    let nextCount = count + 1
                    Some(state, (nextCount, nextCheckVal, checkVal))
                else
                    Some(state, (count, nextCheckVal, lastPrime))
        )    
    

    let primes : int[] = Array.zeroCreate n
    for x in primeTuples do
        let count, checkVal, lastPrime = x
        //printfn "%d %d" count lastPrime
        primes[count] <- lastPrime

    primes
let primeFactors n =
    
    // code adapted from https://www.markheath.net/post/finding-prime-factors-in-f
    let rec testFactorRecursively n potentialFactorToTest knownFactorsSoFar =
        if potentialFactorToTest = n then
            potentialFactorToTest::knownFactorsSoFar
        else if n % potentialFactorToTest = 0 then
            testFactorRecursively 
                (n/potentialFactorToTest) 
                potentialFactorToTest 
                (potentialFactorToTest::knownFactorsSoFar)
        else
            testFactorRecursively n (potentialFactorToTest + 1) knownFactorsSoFar

    let factorize n = testFactorRecursively n 2 []
    factorize n
let primeFactorsOf0ToN (n:int) = 
    let factors:int[][] = Array.create (n + 1) [||]
    for i in 2 .. n do
        let existingFactors = factors[i]
        if existingFactors.Length > 0 then () // not prime
        else
            // prime, expand it into higher positions
            for j in i .. i .. n do
                if j > n then ()
                else factors[j] <- Array.concat [|factors[j]; [|i|]|]
    factors
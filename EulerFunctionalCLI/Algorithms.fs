module Algorithms

open System.Numerics
open System.Collections.Generic
open DomainTypes

let isPrime n =
    let rec check i =
        i > n/2 || (n % i <> 0 && check (i + 1))
    check 2

let convertIntToIntSequence n =  
    seq { 
        for pow10 in 0..12 do 
            let floatN = float n
            let floatPow10 = float pow10
            if floatN > 10.0 ** pow10 then 
                int (floor (floatN % (10.0 ** (floatPow10 + 1.0)) / (10.0 ** floatPow10)))        
        }
    |> Seq.rev

let convertIntToArray n = n |> convertIntToIntSequence |> Seq.toArray

let isPalindrome (n : int)  =

    let intArray = convertIntToArray n
    let length = intArray.Length
    let halfWayPoint = (length / 2) + 1
    let seqOfTruths =
        seq {
            for i in 0 .. halfWayPoint do
                let checkLeft = intArray[i]
                let checkRight = intArray[length - i - 1]
                if checkLeft = checkRight then 0 else 1
            }
    
    let sumOfTruths = Seq.sum seqOfTruths
    if sumOfTruths = 0 then true else false

let sumOfDigitFactorials n = 
    
    let factorials = [ 1; 1; 2; 6; 24; 120; 720; 5040; 40320; 362880 ]

    let rec factorial n =
        match n with
        | 0 | 1 -> 1
        | _ -> n * factorial(n-1)

    let digits = convertIntToArray n
    let factorialSum = (Seq.fold (fun a b -> a + factorial b) 0 digits)
    factorialSum
(*
let primeFactorsLong (n : int64) :List<int64> =
    (* 
        https://www.markheath.net/post/finding-prime-factors-in-f 

        returns a list of all the prime factors of n
        
        Uses a recursive function to divide out known factors. 
        
        start by testing if 2 is a factor of n. if it is, divide n by 2 and try 
        again. Once we've divided out all the factors of 2, we increment the
        test number and repeat. We'll know we've hit the final factor when your 
        potential factor to check = n
    *)
    let rec recurse (numberToBeFactorized : int64) (potentialFactorToCheck : int64) (factorsSoFar : List<int64>) = 
        if potentialFactorToCheck = numberToBeFactorized then
            potentialFactorToCheck::factorsSoFar
        elif numberToBeFactorized % potentialFactorToCheck = 0L then 
            let newNumberToFactorize = (numberToBeFactorized/potentialFactorToCheck)
            let newFactorsSoFar = (potentialFactorToCheck::factorsSoFar)
            recurse newNumberToFactorize potentialFactorToCheck newFactorsSoFar
        else
            let newPotentialFactor = (potentialFactorToCheck+1L)
            recurse numberToBeFactorized newPotentialFactor factorsSoFar

    let emptyFactorsSet : List<int64> = []
    recurse n 2L emptyFactorsSet
*)

let getPrimeFactorsOfInt n =
    
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

let createAppendedIntArray (origArray : int[]) (newVal : int) =

    let newArray = Array.zeroCreate<int> (origArray.Length + 1)
    for i in 0 .. newArray.Length - 1 do
        if i = (newArray.Length - 1) 
            then Array.set newArray i newVal
            else Array.set newArray i origArray[i]

    newArray

let getFibonacciSeq limit = 
    (1, 1) // initial state
    |> Seq.unfold ( 
        fun state -> 
            if ( fst state + snd state > limit ) 
                then None 
                else Some (fst state + snd state, (snd state, fst state + snd state))
    )

let getPrimesUpToN n =

    let limit = ((n - 2) / 2) + 1
    let sieve = Array.zeroCreate limit
    let primeBools = Array.create ((limit * 2) + 1) false

    let updateSieve () = 
        let updateSieveForJVals i = 
            let iVals = seq {i..(limit - 1)}
            let setSievePositionToOne j =
                //printfn "i: %d || j: %d" i j
                sieve[i + j + 2 * i * j] <- 1
            //printfn "i: %d" i
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
    let updatePrimeBools () = 
        primeBools[2] <- true // the below sieve won't surface 2 as a prime
        let updatePrimeBoolForI i = 
            if sieve[i] = 0 then
                primeBools[(i * 2) + 1] <- true

        updateSieve ()
        seq{1..(limit - 1)} |> Seq.iter (fun i -> updatePrimeBoolForI i)
        ()
    updatePrimeBools ()
    let primes =
        (0, []) // initial state of i and an empty primes list
        |> Seq.unfold (fun state ->
            let i = fst state
            let primes = snd state
            let nextI = i + 1
            if i >= primeBools.Length then None
            elif primeBools[i] = false then
                Some(state, (nextI, primes))
            else
                Some(state, (nextI, i::primes))
            ) 
        |> Seq.last
        |> snd
        |> Seq.rev

    { primes = primes; primeBools = primeBools }

let sumOfPrimeFactors n primes = 
    let isMaFactorOfN m = n % m = 0
    primes
    |> Seq.filter isMaFactorOfN
    |> Seq.sum

let partitionFunction n (cache : int[]) = 
            
    cache[0] <- 1
    let rec P n = // (n : int) :int =
        if n < 0 then 0
        elif cache[n] > -1 then cache[n]
        else 
            let result = 
                seq<int> { 
                    for k in 1 .. n do 
                        let n1 = n - k * (3 * k - 1) / 2
                        let n2 = n - k * (3 * k + 1) / 2
                        let Pn1 = P n1
                        let Pn2 = P n2
                        (Pn1 + Pn2) * pown -1 (k+1)
                } 
                |>Seq.sum
            cache[n] <- result
            result
        
    (P n, cache)




let partitionFunctionBig (n : BigInteger) (cache : Dictionary<BigInteger, BigInteger>) = 
            
    cache[0] <- 1
    let rec P (n:BigInteger) :BigInteger = 
        if n < BigInteger 0 then BigInteger 0
        elif cache.ContainsKey n then 
            cache[n]
        else 
            
            
            let result = //lazy( 
                seq {
                    for (k:BigInteger) in (BigInteger 1) .. n do 
                        let n1 = n - k * (BigInteger 3 * k - BigInteger 1) / BigInteger 2
                        let n2 = n - k * (BigInteger 3 * k + BigInteger 1) / BigInteger 2
                        let Pn1 = P n1
                        let Pn2 = P n2
                        let combined = Pn1 + Pn2
                        if (k % BigInteger 2 = BigInteger 1)
                            then 
                                combined
                            else 
                                BigInteger -1 * combined
                } |> Seq.sum
            //)
            let sum = result//.Force()
            cache[n] <- sum
            sum
        
    (P n, cache)

let getFirstNPrimes n =  

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
    

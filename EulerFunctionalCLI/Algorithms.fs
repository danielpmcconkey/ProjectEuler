module Algorithms

open System.Numerics
open System.Collections.Generic
open DomainTypes
open Conversions

let factorize n = 
    let sqrtN = (int)(floor (sqrt ((float)n)))  
    [1..sqrtN]
    |> List.fold ( fun factorsList i -> 
        if n % i = 0 then
            let opposite = n / i
            let newFactors = if opposite = i then factorsList @ [i] else factorsList @ [i; opposite] 
            newFactors
        else factorsList
    ) ([])
let getFibonacciSeq limit = 
    (1, 1) // initial state
    |> Seq.unfold ( 
        fun state -> 
            if ( fst state + snd state > limit ) 
                then None 
                else Some (fst state + snd state, (snd state, fst state + snd state))
    )
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
let isPalindromeString (s : string)  =
    let stringArray = stringToChars s
    let length = stringArray.Length
    if length = 1 then true
    elif length = 2 then 
        if stringArray[0] = stringArray[1] then true else false
    else
        let halfWayPoint = (length / 2) + 1
        let seqOfTruths =
            seq {
                for i in 0 .. halfWayPoint do
                    let checkLeft = stringArray[i]
                    let checkRight = stringArray[length - i - 1]
                    if checkLeft = checkRight then 0 else 1
                }
        
        let sumOfTruths = Seq.sum seqOfTruths
        if sumOfTruths = 0 then true else false
let isPalindromeBase10 (n : int)  = n |> intToString |> isPalindromeString
let isPalindromeBase2 (n : int)  = n |> intToBase2String |> isPalindromeString
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






let sumOfDigitFactorials n = 
    
    let factorials = [ 1; 1; 2; 6; 24; 120; 720; 5040; 40320; 362880 ]

    let rec factorial n =
        match n with
        | 0 | 1 -> 1
        | _ -> n * factorial(n-1)

    let digits = intToIntArray n
    let factorialSum = (Seq.fold (fun a b -> a + factorial b) 0 digits)
    factorialSum












    

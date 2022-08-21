module Algorithms

open System.Numerics
open System.Collections.Generic
open DomainTypes
open Conversions

let crossJoinLists lx ly = lx |> List.collect (fun x -> ly |> List.map (fun y -> x, y))
let crossJoinArrays lx ly = lx |> Array.collect (fun x -> ly |> Array.map (fun y -> x, y))
let crossJoinSequences lx ly = lx |> Seq.collect (fun x -> ly |> Seq.map (fun y -> x, y))
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
let factorizeLong (n:int64) = 
    // find a close approximation of square root to use as a limit because Longint doesn't support sqrt functions
    let sqrtN = (int64)(floor (sqrt ((float)n)))
    [1L..sqrtN]
    |> List.fold ( fun factorsList i -> 
        if n % i = 0L then
            let opposite = n / i
            let newFactors = if opposite = i then factorsList @ [i] else factorsList @ [i; opposite] 
            newFactors
        else factorsList
    ) ([])
let factorizeBig (n:bigint) = 
    // find a close approximation of square root to use as a limit because bigint doesn't support sqrt functions
    let numDigitsN = n.ToString().Length
    let pow10 = ((float)numDigitsN - 1.0) / 2.0
    let maxSqrt = (int)(3.2 * (10.0**pow10))
    let max = BigInteger((int64)(floor (sqrt ((float)n))))
    [1I..max]
    |> List.fold ( fun factorsList i -> 
        if n % i = 0I then
            let opposite = n / i
            let newFactors = if opposite = i then factorsList @ [i] else factorsList @ [i; opposite] 
            newFactors
        else factorsList
    ) ([])
let getAllPermutationsOfList l =
    // this code stolen from https://stackoverflow.com/questions/1526046/f-permutations
    let rec distribute e = function
    | [] -> [[e]]
    | x::xs' as xs -> (e::xs)::[for xs in distribute e xs' -> x::xs]

    let rec permute = function
    | [] -> [[]]
    | e::xs -> List.collect (distribute e) (permute xs)

    permute l
let getAnswerFromIntOption o = match o with | Some(x)  -> x; | None -> -1
let getAnswerFromFractionOption o = match o with | Some(x)  -> x; | None -> -1
let getFibonacciSeq limit = 
    (1, 1) // initial state
    |> Seq.unfold ( 
        fun state -> 
            if ( fst state + snd state > limit ) 
                then None 
                else Some (fst state + snd state, (snd state, fst state + snd state))
    )
let getPerfectSquaresUpToN n =
    let limit = (int)(floor (sqrt ((float)n)))
    [|1..limit|] |> Array.map (fun i -> i * i)
let greatestCommonFactor a b =
    // this is not FP idiomatic
    let mutable r = a % b;
    let mutable a' = a
    let mutable b' = b
    while r <> 0 do
        a' <- b';
        b' <- r;
        r <- a' % b';
    b';
let getPythagoreanTriangles maxPerimeter =
    // use Euclid's formula to generate all primitives
    let maxM = (float)maxPerimeter * 0.5 |> sqrt |> ceil |> floatToInt
    let primatives =
        [2..maxM]
        |> List.collect (fun m -> [1..(m - 1)] |> List.map (fun n -> m, n))
        |> List.filter (fun (m, n) -> ((m + n) % 2 = 1 && (greatestCommonFactor n m) = 1))
        |> List.map (fun (m, n) -> { 
                    aLength = (m * m) - (n * n);
                    bLength = 2 * m * n;
                    cLength = (m * m) + (n * n);
                    perimeter = (2 * m * n) + (m * m) + (m * m) 
                }
            )
    // now expand each primative out through its multiples to produce all the non-primitives
    let all = 
        primatives
        |> List.collect (fun t -> 
                let maxK = ((float)maxPerimeter / (float)t.perimeter) |> floatToInt
                [1..maxK] 
                |> List.map (fun k -> {
                    aLength = k * t.aLength; 
                    bLength = k * t.bLength; 
                    cLength = k * t.cLength; 
                    perimeter = k * t.perimeter}
                    )
            )
    all
let isListPandigital l =
    let sorted = l |> List.sort
    if sorted[0] = 0 || sorted.Length <> 9 then false
    elif sorted |> List.distinct |> List.length <> 9 then false
    else true
let isPalindromeChars (chars : char[])  =
        let length = chars.Length
        if length = 1 then true
        elif length = 2 then 
            if chars[0] = chars[1] then true else false
        else
            let halfWayPoint = (length / 2) + 1
            let seqOfTruths =
                seq {
                    for i in 0 .. halfWayPoint do
                        let checkLeft = chars[i]
                        let checkRight = chars[length - i - 1]
                        if checkLeft = checkRight then 0 else 1
                    }
        
            let sumOfTruths = Seq.sum seqOfTruths
            if sumOfTruths = 0 then true else false
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
let isPandigital n =
    let sorted = n |> intToListInt |> List.sort
    if sorted[0] = 0 || sorted.Length <> 9 then false
    elif sorted |> List.distinct |> List.length <> 9 then false
    else true
let isPentagonal n = (sqrt (1.0 + (24.0 * (float)n))) % 6.0 = 5
let isPerfectSquare (n:int) = (sqrt ((float)n)) % 1.0 = 0.0
let orderOfMagnitude (n:int) = (n.ToString().Length) - 1
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
let factorial n = 
    [1..n] |> List.fold (fun acc elem -> acc * elem) 1
let arraySwap place1 place2 (A:'T[]) =
    A |> Array.mapi (fun i v ->
        match i with 
        | _ when i = place1 -> A[place2]
        | _ when i = place2 -> A[place1]
        | _ -> v
        )
let factorialsUpToN n =
    (0, 1) 
    |> Array.unfold (fun (i, fact) -> 
        if i > n then None
        elif i = 0 then Some((i, fact), (1, 1))
        else
            Some((i, fact), (i + 1, fact * (i + 1)))
        )
    |> Array.map (fun t -> snd t)
let permuteArray (array:'T[]) =
    let mod6Swaps i fs = 
        // pos 2 is the position of the largest of the factorials for which i mod it is 0
        let pos2 = 
            fs 
            |> Array.mapi (fun iter v -> (iter, i % v))
            |> Array.filter (fun (iter, modulo) -> modulo = 0)
            |> Array.maxBy (fun t -> fst t)
            |> fst
        let pos1 = 
            if pos2 % 2 = 0 then 0
            else
                ((i / fs[pos2]) % (pos2 + 1)) - 1
        pos1, pos2
    let length = array.Length 
    let factorials = factorialsUpToN length
    let max = factorials[length] 
    let period = 6 // don't know why it's 6, but it is
    (1, array)
    |> Array.unfold (fun (i, lastPermutation) ->
        if i > max then None
        else 
            let modI = i % 2
            let modPeriod = i % period
            let pos1, pos2 =
                if modPeriod = 0 then mod6Swaps i factorials
                elif modI % 2 = 1 then 0, 1 
                else 0, 2
            let nextArray = 
                if i = max then lastPermutation // you can't make anymore swaps and this gets thrown away
                else arraySwap pos1 pos2 lastPermutation
            Some((i, lastPermutation), (i + 1, nextArray))
        )
    |> Array.map (fun (i, p) -> p)
let areIntegersPermutations m n = 
    let mChars = m |> intToString |> stringToChars |> Array.sort
    let nChars = n |> intToString |> stringToChars |> Array.sort
    Array.zip mChars nChars
    |> Array.forall (fun (m, n) -> m = n)



















    

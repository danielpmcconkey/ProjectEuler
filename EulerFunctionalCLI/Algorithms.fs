module Algorithms

let convertIntToIntArray n =
    
    let getDigitAtPow10 (n: int) (pow10: int) = 

        let floatN = float n
        let floatPow10 = float pow10
        if floatN >= 10.0 ** pow10 then
            int (floor (floatN % (10.0 ** (floatPow10 + 1.0)) / (10.0 ** floatPow10)))
        else
            int 0

    let countSignificantDigits a b = 
        if a > 0 then 
            a + 1
        else 
            if b = 0 then 0
            else 1

    let ordersOfMagnitudeToSupport = 12;
    let (arrayOfDigits: int[]) = [| for i in 0 .. ordersOfMagnitudeToSupport -> getDigitAtPow10 n i |]
    let reversed = Array.rev arrayOfDigits
    let significantDigits = (Seq.fold (fun a -> countSignificantDigits a) 0 reversed) - 1
    let arrayStart = reversed.Length - significantDigits - 1
    let arrayEnd = reversed.Length - 1
    reversed[arrayStart..arrayEnd]

let sumOfDigitFactorials n = 
    
    let factorials = [ 1; 1; 2; 6; 24; 120; 720; 5040; 40320; 362880 ]

    let rec factorial n =
        match n with
        | 0 | 1 -> 1
        | _ -> n * factorial(n-1)

    let digits = convertIntToIntArray n
    let factorialSum = (Seq.fold (fun a b -> a + factorial b) 0 digits)
    factorialSum



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

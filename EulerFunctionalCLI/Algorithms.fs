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


let getPrimeFactorsOfLong (n:int64) =
    
    // code adapted from https://www.markheath.net/post/finding-prime-factors-in-f
    let rec testFactorRecursively (n:int64) (potentialFactorToTest:int64) knownFactorsSoFar =
        if potentialFactorToTest = n then
            potentialFactorToTest::knownFactorsSoFar
        else if n % potentialFactorToTest = 0 then
            testFactorRecursively 
                (n/potentialFactorToTest) 
                potentialFactorToTest 
                (potentialFactorToTest::knownFactorsSoFar)
        else
            testFactorRecursively n (potentialFactorToTest + 1L) knownFactorsSoFar

    let factorize (n:int64) = testFactorRecursively n 2 []
    factorize n


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

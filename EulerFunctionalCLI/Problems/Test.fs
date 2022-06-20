module Test




type Chain = {
    chainElements : int[];
}
type DigitalFactorialChain = {
    n : int;
    length : int;
    chain : Chain;
}
type ChainCount = {
    n : int;
    count : int
}


let unknownValue = -1 // used for determining whether we know a count in the dictionary


let createAppendedIntArray (origArray : int[]) (newVal : int) =

    let newArray = Array.zeroCreate<int> (origArray.Length + 1)
    for i in 0 .. newArray.Length - 1 do
        if i = (newArray.Length - 1) 
            then Array.set newArray i newVal
            else Array.set newArray i origArray[i]

    newArray

let appendDigitalFactorialChain (newChainElement:int) (dfc:DigitalFactorialChain) =

    let oldElements = dfc.chain.chainElements
    let appended:int[] = createAppendedIntArray oldElements newChainElement

    let newDfc : DigitalFactorialChain = {
        n = dfc.n;
        length = dfc.length + 1;
        chain = { chainElements =  appended};
    }
    newDfc

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

// recursive function for calculating how many non-repeaters from a given starting pos
let rec getRepeatChain n (currentChain:DigitalFactorialChain) (dictionary : int[]) = 

    let recurse () = 
        let sumOfFacts = sumOfDigitFactorials n
        let newCurrentChain = appendDigitalFactorialChain n currentChain
        getRepeatChain sumOfFacts newCurrentChain dictionary
    let exit countRemainder =
        let exitChain : DigitalFactorialChain = { 
            n = currentChain.n; 
            length = (currentChain.length + countRemainder); 
            chain = { chainElements = currentChain.chain.chainElements };
        }
        exitChain
    if dictionary[n] = unknownValue then recurse ()
    else exit dictionary[n]


let getFinalAnswer start limit (dictionary : int[])=

    let mutable mutableDict = dictionary

    let skipRow n = 
        mutableDict 
    let processRow n = 
        let startChain : DigitalFactorialChain = {
            n = n; 
            length = 0;
            chain = {chainElements = [||]}    
        }
        let dfc = getRepeatChain n startChain mutableDict
        // now add the chain elements to the dictionary addChainElementsToDictionary varl dict
        let chainLength = dfc.chain.chainElements.Length
        for i in 0 .. chainLength - 1 do        
            mutableDict[dfc.chain.chainElements[i]] <- dfc.length - i
        mutableDict

    for i in start .. limit do
        // check to see if i has already been determined
        if dictionary[i] = unknownValue then 
            mutableDict <- processRow i
        else 
            mutableDict <- skipRow i
        


    let countSixtySequenceEntries (countSoFar : int) (thisChainLength : int) = 
        if thisChainLength = 60 then 
            countSoFar + 1
        else 
            countSoFar

    let numSixties = Seq.fold (fun a -> countSixtySequenceEntries a) 0 mutableDict
    numSixties


let limit = 1000000
let start = 1




let stopWatch = System.Diagnostics.Stopwatch()
stopWatch.Start()

let biggestPossibleFatorialSum = 1 + (sumOfDigitFactorials 999999)
let dictionary = Array.create biggestPossibleFatorialSum unknownValue
// add known values to the dictionary to prevent recurrsive loops
dictionary[1]      <- 1
dictionary[2]      <- 1
dictionary[145]    <- 1
dictionary[169]    <- 3
dictionary[871]    <- 2
dictionary[872]    <- 2
dictionary[1454]   <- 3
dictionary[40585]  <- 1
dictionary[45361]  <- 1
dictionary[45362]  <- 1
dictionary[363601] <- 3

let answer = getFinalAnswer start limit dictionary
printfn "answer is: %d" answer

stopWatch.Stop()
printfn "run time: %d ms" stopWatch.ElapsedMilliseconds

stopWatch.ElapsedMilliseconds



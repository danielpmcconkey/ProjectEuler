module Euler074

open Algorithms


type DigitalFactorialChain = {
    n : int;
    length : int;
    chain : int[];
}



let run ():int =

    let theValueOfUnknownCount = -1 // used for determining whether we know a count in the dictionary
    let limit = 1000000
    let start = 1

    let calculate60ChainCount start limit =

        (*
            define helper functions 
        *)

        let appendDigitalFactorialChain (newChainElement:int) (dfc:DigitalFactorialChain) =

            let oldElements = dfc.chain
            let appended:int[] = createAppendedIntArray oldElements newChainElement
            

            let newDfc : DigitalFactorialChain = {
                n = dfc.n;
                length = dfc.length + 1;
                chain = appended;
            }
            newDfc

        let rec getRepeatChain n (currentChain:DigitalFactorialChain) (dictionary : int[]) = 
            // recursive function for calculating chain length from a given 
            // starting pos. This is the workhorse of this solution

            if dictionary[n] = theValueOfUnknownCount then 
                let sumOfFacts = sumOfDigitFactorials n
                let newCurrentChain = appendDigitalFactorialChain n currentChain
                getRepeatChain sumOfFacts newCurrentChain dictionary
            else  
                { 
                    n = currentChain.n; 
                    length = (currentChain.length + dictionary[n]); 
                    chain = currentChain.chain;
                }

        let processRow n dictionary = 
            let startChain : DigitalFactorialChain = {
                n = n; 
                length = 0;
                chain = [||];    
            }
            let dfc = getRepeatChain n startChain dictionary
            // now add the chain elements to the dictionary addChainElementsToDictionary varl dict
            let chainLength = dfc.chain.Length
            for i in 0 .. chainLength - 1 do        
                dictionary[dfc.chain[i]] <- dfc.length - i
            dictionary

        let countSixtySequenceEntries (countSoFar : int) (thisChainLength : int) = 
            if thisChainLength = 60 then 
                countSoFar + 1
            else 
                countSoFar


        (* 
            Set up a mutable dictionary that each iteration will add its entire
            chain to. This is not very FP idiomatic and I'd like to figure out
            how to do this right
        *)
        let biggestPossibleFatorialSum = 1 + (sumOfDigitFactorials 999999)
        let mutable mutableDict = Array.create biggestPossibleFatorialSum theValueOfUnknownCount
        // add the integers whose factorial sums are themselves (like 145 in 
        // the problem statement) to the dictionary to prevent recurrsive loops
        mutableDict[1]      <- 1
        mutableDict[2]      <- 1
        mutableDict[145]    <- 1
        mutableDict[40585]  <- 1
        
        // now add the integers whose chain lengths are given in the problem 
        // statement
        mutableDict[169]    <- 3
        mutableDict[871]    <- 2
        mutableDict[872]    <- 2
        mutableDict[1454]   <- 3
        mutableDict[45361]  <- 2
        mutableDict[45362]  <- 2
        mutableDict[363601] <- 3

         
        (* 
            Now put it all together. Iterater from start through limit and 
            determine the chain length for each element. All the while, update
            your dictionary for values in that element's chain so that A) you
            won't have to recalculate them; and B) to make it faster to find
            future chain lengths (the factorial summing stops when you've 
            found a "known" value)
        *)
        for i in start .. limit do
            // don't process this row if i has already been determined
            if mutableDict[i] = theValueOfUnknownCount then 
                mutableDict <- processRow i mutableDict
                ()
            else 
                () // skip processing

        let numSixties = Seq.fold (fun a -> countSixtySequenceEntries a) 0 mutableDict
        numSixties

    calculate60ChainCount start limit 
    

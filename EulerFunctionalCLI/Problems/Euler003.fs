module Euler003

let run () =

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

    let numberToCheck : int64 = 600851475143L
    let answer = primeFactorsLong numberToCheck |> Seq.max
    answer.ToString()
    




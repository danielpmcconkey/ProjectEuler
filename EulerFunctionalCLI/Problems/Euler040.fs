module Euler040
open Algorithms
open Conversions

let run () =
    let startingState = 
        (
                (int)1, // product
                (int)0, // last number
                (int)0  // last position
        )
    let product, lastNumber, lastPosition = 
        [0..6]
        |> List.fold (fun state oom -> 
            let target = (int)(10.0 ** (float)oom)
            let product, lastNumber, lastPosition = state
            let numDigitsLastNumber = (lastNumber |> orderOfMagnitude) + 1
            let nextOOMJump = (int)(10.0 ** (float)(numDigitsLastNumber))
            let countOfIntsUntilNextOOMJump = nextOOMJump - lastNumber - 1
            let willThatGetUsToNextTarget = 
                if (lastPosition + (countOfIntsUntilNextOOMJump * numDigitsLastNumber) >= target) 
                    then true 
                    else false
            let positionAtNextOOMJump = lastPosition + (countOfIntsUntilNextOOMJump * numDigitsLastNumber)
            let numberNeededAtThisOOM = 
                if willThatGetUsToNextTarget
                    then (int)(ceil (((float)target - (float)lastPosition) / 2.0))
                    else countOfIntsUntilNextOOMJump
            let gap = 
                if willThatGetUsToNextTarget
                    then 0 
                    else target - lastPosition - (numberNeededAtThisOOM * numDigitsLastNumber)
            let numberNeededAtNextOOM = 
                if willThatGetUsToNextTarget 
                    then 0 
                    else (int)(ceil ((float)gap / ((float)numDigitsLastNumber + 1.0)))
            let numberAtNextOOM = lastNumber + numberNeededAtThisOOM + numberNeededAtNextOOM

            // now that we've got the number that crosses the target
            // position, we need to find out which position of that number
            // is the one that crosses the line

            let numAsArray = intToIntArray numberAtNextOOM
            let positionAfterThisHit:int = lastPosition + (numberNeededAtThisOOM * numDigitsLastNumber) + (numberNeededAtNextOOM * (numDigitsLastNumber + 1)) 
            let positionOfDigitInArray = numAsArray.Length - 1 - (positionAfterThisHit - target)
            let digit = numAsArray[positionOfDigitInArray]
            let newProduct = product * digit
            let newLastNum = numberAtNextOOM
            let newLastPosition = positionAfterThisHit
            let nextState = (newProduct, newLastNum, newLastPosition)
            nextState
        ) startingState 
    product |> intToString

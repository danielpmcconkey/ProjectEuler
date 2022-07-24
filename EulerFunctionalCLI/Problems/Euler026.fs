module Euler026

let run () =

    (*
    I'm pretty sure I'm doing FP wrong here. I don't think I shoud be using 
    unfold. But at this point in my learning journey, I don't know any other 
    way to stop looping code logic once a desired result has been found. And 
    what this algorithm needs to do is perform gradeschool style long division
    until you've found a repeating remainder. Like so:
        
         let n = 11
         
                     0.09
                   ___________
              11   ) 1.0000000
                     1 00 
                    -  99
                    -----
                        1 <-- you've identified a repeat because 1 here is the same as when you started
                     
    And, BTW, this runs about 50 times slower than the C# version. Another
    indicator that I'm doing it wrong. Update: I took out an extraneous 
    function call in my tryFindIndex and now it only runs 3 times slower :)

    *)


    let maxI l = l |> List.maxBy (fun (x, y) -> y) |> fst
    let unitFractionDivisor n =
        if n = 1 then 0 
        else
            // unfold a sequence until you either have an even division, 
            // meaning no repeat, or until you find a repeat
            let unfolded = 
                (10, [], -1)
                |> Seq.unfold (fun accumulator ->
                    let currentTrialNumerator, (trialNumerators:List<int>), answer = accumulator
                    if answer > -1 then None
                    else
                        if n > currentTrialNumerator then
                            let newTrialNumerator = currentTrialNumerator * 10
                            let newNumerators = newTrialNumerator::trialNumerators
                            let nextAccumulator = (newTrialNumerator, newNumerators, answer)
                            Some(nextAccumulator, nextAccumulator)
                        elif n = currentTrialNumerator then
                            let nextAccumulator = (n, trialNumerators, 0)
                            Some(nextAccumulator, nextAccumulator)
                        elif n < currentTrialNumerator then
                            // how many times will n go into currentTrialNumerator?
                            let numberOfTimes = currentTrialNumerator / n
                            let product = n * numberOfTimes
                            let remainder = currentTrialNumerator - product
                            if remainder = 0 then
                                let nextAccumulator = (currentTrialNumerator, trialNumerators, 0)
                                Some(nextAccumulator, nextAccumulator)
                            else
                                let newTrialNumerator = remainder * 10
                                // now check if this new trial numerator is already in the list
                                // if so, you have identified your repeat
                                match List.tryFindIndex (fun x -> x = newTrialNumerator) trialNumerators with
                                | Some lastPosition -> 
                                    let nextAccumulator = (newTrialNumerator, trialNumerators, lastPosition + 1)
                                    Some(nextAccumulator, nextAccumulator)
                                | None ->
                                    let newNumerators = newTrialNumerator::trialNumerators
                                    let nextAccumulator = (newTrialNumerator, newNumerators, answer)
                                    Some(nextAccumulator, nextAccumulator)
                        else 
                            // you should never get here, but I don't think 
                            // it'll compile without closing out the if
                            Some(accumulator, accumulator)                
                ) 
            let x,y,result = unfolded |> Seq.last
            result
    
    let limit = 1000
    let answer = [for i in 2..limit do (i, unitFractionDivisor i)] |> maxI
    answer.ToString()



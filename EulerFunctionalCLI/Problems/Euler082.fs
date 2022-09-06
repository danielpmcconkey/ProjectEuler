module Euler082


open Conversions
open IO

let run () = 

    (*
    I tried to be cute and re-use my A-star pathfinder from the previous 
    problem. Set up an array of all the possible start and end positions and 
    take the min of mins. I knew I'd have to run it 6400 times but A-star is 
    fast. Well mine wasn't. It took about 20 minutes. Produced the right 
    answer, though.

    So here I went with an approach similar to the triangle problem (67?) by 
    updating one column at a time, moving right to left, finding the cheapest 
    way to get to the next column. It works in 34 milliseconds. The problem is 
    that this isn't very functional. I maintain an array of "actual" costs that 
    I updated with every pass. I may modify it to recurse and pass the latest 
    column actuals to the next column processor. But, for now, this works.
    *)

    // set the number of up/down movements we're willing to inspect to find a
    // cheaper path to get to the next row over
    let maxRowsToTravel = 5

    // create a very large value to use as a proxy for going off the graph. 
    // It's easier to do this than to create varibly sized arrays, I think
    let maxInt = (int)1e7 


    let input = get82Input ()
    let height = input.Length
    let width = input[0].Length

    // warning, this actual costs array gets updated by the processing 
    // functions
    let actualCosts:int[][] =
        [|0..(height - 1)|]
        |> Array.map (fun i -> Array.zeroCreate width)

    // update actuals for the last column manually
    [|0..(height - 1)|]
    |> Array.iter (fun i -> 
        actualCosts[i][width - 1] <- input[i][width - 1]
        )

    let processNode x y = 
        let minMaxAtOffset offset = 
            if offset <= 0
            then
                (offset + maxRowsToTravel), maxRowsToTravel
            else
                maxRowsToTravel, (offset + maxRowsToTravel)
        let sumMinToMax minPlace maxPlace (valsToSum:int[]) = 
            valsToSum[minPlace..maxPlace]
            |> Array.sum
        let sumsUpAndDown (valsToSum:int[]) = 
            [|(maxRowsToTravel * -1)..maxRowsToTravel|]
            |> Array.map (fun offset ->
                let minPlace, maxPlace = minMaxAtOffset offset
                let sumAtOffset = sumMinToMax minPlace maxPlace valsToSum
                sumAtOffset
                )
        let valsThisColumn () = 
            [|(maxRowsToTravel * -1)..maxRowsToTravel|]
            // first get the input value at (x, y + offset)
            |> Array.map (fun offset ->
                let targetCol = y + offset
                if targetCol < 0 || targetCol >= height then maxInt
                else input[targetCol][x]
                )
        let valInRightColumn i =
            let offsetPos = y - maxRowsToTravel + i
            if offsetPos < 0 || offsetPos >= height then maxInt
            else actualCosts[offsetPos][x + 1]
        let addNextColumn (sumValsAtOffset:int[]) = 
            sumValsAtOffset
            |> Array.mapi (fun i verticalSum -> (valInRightColumn i) + verticalSum)
        let valsToSum = valsThisColumn ()
        let sumValsAtOffset = sumsUpAndDown valsToSum
        let totalCosts = addNextColumn sumValsAtOffset            
        let minVal = totalCosts |> Array.min
        // update the actual
        actualCosts[y][x] <- minVal
        ()
    let processColumn x = 
        [|0..(height - 1)|]
        |> Array.iter (fun y -> processNode x y)
        ()
    let answerAfterProcessing () =
        [|0..(height - 1)|]
        |> Array.map (fun y ->
            actualCosts[y][0]
            )
        |> Array.min
    // now go column at a time and figure out the cheapest way to get to the 
    // column to the right
    [|0..(width - 2)|]
    |> Array.rev
    |> Array.iter (fun x -> processColumn x)
     
    answerAfterProcessing ()
    |> intToString
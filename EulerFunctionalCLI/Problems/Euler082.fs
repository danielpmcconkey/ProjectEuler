module Euler082


open Conversions
open IO
open PathFinder

let run () = 

    (*
    I tried to be cute and re-use my A-star pathfinder from the previous 
    problem. Set up an array of all the possible start and end positions and 
    take the min of mins. I knew I'd have to run it 6400 times but A-star is 
    fast. Well mine wasn't. It took about 20 minutes. Produced the right 
    answer, though.

    So here I went with an approach similar to the triangle problem (67?) by 
    updating one column at a time, moving right to left, finding the cheapest 
    way to get to the next column. It works in 34 milliseconds. 
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
    
    pathSum3Ways input height width maxInt maxRowsToTravel
    |> intToString
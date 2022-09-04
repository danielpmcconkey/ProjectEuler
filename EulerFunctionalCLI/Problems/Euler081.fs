module Euler081
open DomainTypes
open Conversions
open IO
open PathFinder

let run () =

    (*
    My original approach, when I went through these problems in C#, was to make
    this just like problem 67. But, here, the "triangle" had to be rotated 45 
    degrees. I thought I was quite clever. This go around, I decided to 
    implement a full A-star pathfinder in F#. I knew I'd be using it in future 
    problems so it just made sense to use it here. And that's why you see I've 
    implemented it to be re-usable to future versions that will allow up and 
    left travel.

    As far as implementing A-star in F#, well that was a beast. I don't feel 
    good about it. There's a lot of passing around and recreating arrays 
    whereas I could've just had a top level dictionary to serve for my queue 
    and finished lists. Or I could've just straight up modified the original
    nodes list. But, this seems more FP idiomatic to me, though much less 
    elegant IMO.
    *)
 
    let input = get81Input ()
    let height = input.Length
    let width = input[0].Length
    let start = {x = 0; y = 0}
    let goal = {x = width - 1; y = height - 1}
    let averageMCost = 
        let sumAll = input |> Array.sumBy (fun row -> row |> Array.sum)  
        (float)sumAll / (float)height / (float)width
    // create a version of the Heuristic cost function that only needs the last 2 args
    let hFuction = heuristicCostModifiedManhattan averageMCost goal.x goal.y
    let nodes = nodesFromInput2Ways input start hFuction
    aStarLeastCost nodes [|nodes[0]|] [||] goal
    |> (fun a -> a.Value)
    |> intToString
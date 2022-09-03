module Euler081
open DomainTypes
open Conversions
open IO
open PathFinder

let run () =
    let heuristicCostFunction averageMCost goalX goalY x y =
        let heuristicModifier = 0.5
        let manhattanDistance = (float)((abs (goalX - x)) + (abs (goalY - y)))
        (int)(round (manhattanDistance * heuristicModifier * averageMCost))
 
    let input = get81Input ()
    let height = input.Length
    let width = input[0].Length
    let start = {x = 0; y = 0}
    let goal = {x = width - 1; y = height - 1}
    let averageMCost = 
        let sumAll = input |> Array.sumBy (fun row -> row |> Array.sum)  
        (float)sumAll / (float)height / (float)width
    // create a version of the Heuristic cost function that only needs the last 2 args
    let hFuction = heuristicCostFunction averageMCost goal.x goal.y
    let nodes = nodesFromInput2Ways input start hFuction
    aStarLeastCost nodes [|nodes[0]|] [||] goal
    |> (fun a -> a.Value)
    |> intToString
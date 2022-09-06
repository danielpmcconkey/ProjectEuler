module Euler083
open DomainTypes
open Conversions
open IO
open PathFinder

let run () =

    (*
    Since I built out 81 to use an A-star pathfinding algorithm, this was a 
    straight copy of 81. All I had to do was pass a true value to node creation
    function to let it know to add "left" as a route. 

    Now, this does take 3 seconds to run, which makes me think it's evaluating 
    more nodes than it should. Rather, I think it's re-evaluating nodes that I
    think are dead. Maybe I'll fix that later.
    *)
 
    let input = get83Input ()
    let height = input.Length
    let width = input[0].Length
    let start = {x = 0; y = 0}
    let goal = {x = width - 1; y = height - 1}
    let averageMCost = 
        let sumAll = input |> Array.sumBy (fun row -> row |> Array.sum)  
        (float)sumAll / (float)height / (float)width
    // create a version of the Heuristic cost function that only needs the last 2 args
    let hFuction = heuristicCostModifiedManhattan averageMCost goal.x goal.y
    let nodes = nodesFromInput4Ways input start hFuction
    aStarLeastCost nodes [|nodes[0]|] [||] goal
    |> intToString
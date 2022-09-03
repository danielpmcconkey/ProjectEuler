module PathFinder
open DomainTypes

let nodesFromInput (input:int[][]) start hFunction canUp canRight canDown canLeft =
    let height = input.Length
    let width = input[0].Length
    let infinityInt = System.Int32.MaxValue
    input
    |> Array.mapi (fun y row -> 
        row |> Array.mapi (fun x cost -> 
            {
                position = {x = x; y = y}
                heuristicCost = Some(hFunction x y)
                distanceTo = if x = start.x && y = start.y then Some(input[y][x]) else None
                pathVia = None
                r_up = None
                r_right = 
                    if canRight && x < (width - 1)
                    then Some({cost = input[y][x + 1]; destination = {x = x + 1; y = y}}) 
                    else None
                r_down = 
                    if canDown && y < height - 1 
                    then Some({cost = input[y + 1][x]; destination = {x = x; y = y + 1}}) 
                    else None
                r_left = None
            }
            )
        )
    |> Array.concat
let nodesFromInput2Ways (input:int[][]) start hFunction =
    nodesFromInput input start hFunction false true true false
let nodeTryFindByPosition (position:XyCoordinate) (nodes:Node[]) =
    // use for when you don't know if it's in the queue
    nodes |> Array.tryFind (fun n -> n.position = position)
let nodeFindByPositon (position:XyCoordinate) (nodes:Node[]) =
    // use for when you're certain you know it's in the list
    let tryFindResult = nodeTryFindByPosition position nodes
    match tryFindResult with
    | None -> failwith "node not found in list"
    | Some(x) -> x
let rec aStarIteration (nodes:Node[]) (queue:Node[]) (finished:Node[]) goalPosition = 
    let evaluateRouteOption (r:option<Route>) (currentNode:Node) (queue:Node[]) =
        match r with
        | None -> queue
        | Some(route) -> 
            let cost = route.cost
            // see if the destination is already in queue
            let destinationMatch = nodeTryFindByPosition route.destination queue
            match destinationMatch with
            | None ->
                // copy from nodes, update values, add to return queue, and return
                let origNode = nodeFindByPositon route.destination nodes
                let newNode = {
                    position = origNode.position
                    heuristicCost = origNode.heuristicCost
                    distanceTo = Some(cost + currentNode.distanceTo.Value)
                    pathVia = Some(currentNode.position)
                    r_up = origNode.r_up
                    r_right = origNode.r_right
                    r_down = origNode.r_down
                    r_left = origNode.r_left
                }
                Array.concat [|queue; [|newNode|]|]
            | Some(dNode) ->
                // evaluate whether this needs to update, update if so, and return
                let currentDistanceTo = dNode.distanceTo
                let newDistanceTo = Some(cost + currentNode.distanceTo.Value)
                if newDistanceTo.Value < currentDistanceTo.Value
                    then 
                        // replace and return
                        let newNode = {
                            position = dNode.position
                            heuristicCost = dNode.heuristicCost
                            distanceTo = Some(newDistanceTo.Value)
                            pathVia = Some(currentNode.position)
                            r_up = dNode.r_up
                            r_right = dNode.r_right
                            r_down = dNode.r_down
                            r_left = dNode.r_left
                        }
                        let indexOfMatch = queue |> Array.findIndex (fun n -> n.position = route.destination)
                        Array.concat [|
                            queue[0..(indexOfMatch - 1)];
                            [|newNode|];
                            queue[(indexOfMatch + 1)..(queue.Length - 1)]|]
                    else queue

    if finished.Length > 0 && (finished[finished.Length - 1]).position = goalPosition 
        then finished
        else
            let sortedQueue = queue |> Array.sortBy (fun n -> 
                let hCost = 
                    match n.heuristicCost with
                    | None -> System.Int32.MaxValue
                    | Some(x) -> x
                let dCost = 
                    match n.distanceTo with
                    | None -> System.Int32.MaxValue
                    | Some(x) -> x
                hCost + dCost
                )
            let first = sortedQueue[0]
            // expand the first in queue
            let queueAfterUp = evaluateRouteOption first.r_up first sortedQueue
            let queueAfterRight = evaluateRouteOption first.r_right first queueAfterUp
            let queueAfterDown = evaluateRouteOption first.r_down first queueAfterRight
            let queueAfterLeft = evaluateRouteOption first.r_left first queueAfterDown

            // take first off the queue and move it to finished
            let nextQueue = queueAfterLeft[1..(queueAfterLeft.Length - 1)]
            let nextFinished = Array.concat [|finished; [|first|]|]
            aStarIteration nodes nextQueue nextFinished goalPosition
let aStarLeastCost (nodes:Node[]) (queue:Node[]) (finished:Node[]) goalPosition = 
    let finished = aStarIteration nodes queue finished goalPosition
    finished[finished.Length - 1].distanceTo
    
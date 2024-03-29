﻿module PathFinder
open DomainTypes

let heuristicCostModifiedManhattan averageMCost goalX goalY x y =
    let heuristicModifier = 0.5
    let manhattanDistance = (float)((abs (goalX - x)) + (abs (goalY - y)))
    (int)(round (manhattanDistance * heuristicModifier * averageMCost))
let nodesFromInput (input:int[][]) start hFunction canUp canRight canDown canLeft =
    let height = input.Length
    let width = input[0].Length
    input
    |> Array.mapi (fun y row -> 
        row |> Array.mapi (fun x cost -> 
            {
                position = {x = x; y = y}
                heuristicCost = Some(hFunction x y)
                distanceTo = if x = start.x && y = start.y then Some(input[y][x]) else None
                pathVia = None
                r_up = 
                    if canUp && y > 0 
                    then Some({cost = input[y - 1][x]; destination = {x = x; y = y - 1}}) 
                    else None
                r_right = 
                    if canRight && x < (width - 1)
                    then Some({cost = input[y][x + 1]; destination = {x = x + 1; y = y}}) 
                    else None
                r_down = 
                    if canDown && y < height - 1 
                    then Some({cost = input[y + 1][x]; destination = {x = x; y = y + 1}}) 
                    else None
                r_left = 
                    if canLeft && x > 0
                    then Some({cost = input[y][x - 1]; destination = {x = x - 1; y = y}}) 
                    else None
            }
            )
        )
    |> Array.concat
let nodesFromInput2Ways (input:int[][]) start hFunction =
    nodesFromInput input start hFunction false true true false
let nodesFromInput3Ways (input:int[][]) start hFunction =
    nodesFromInput input start hFunction true true true false
let nodesFromInput4Ways (input:int[][]) start hFunction =
    nodesFromInput input start hFunction true true true true
let nodeTryFindByPosition (position:XyCoordinate) (nodes:Node[]) =
    // use for when you don't know if it's in the list
    nodes |> Array.tryFind (fun n -> n.position = position)
let nodeFindByPositon (position:XyCoordinate) (nodes:Node[]) =
    // use for when you're certain you know it's in the list
    nodes |> Array.find (fun n -> n.position = position)
let rec aStarIteration (nodes:Node[]) (queue:Node[]) (finished:Node[]) goalPosition = 
    let evaluateRouteOption (r:option<Route>) (currentNode:Node) (queue:Node[]) =
        match r with
        | None -> queue
        | Some(route) -> 
            let cost = route.cost
            // see if the destination is already in queue or finished
            let destinationMatch = nodeTryFindByPosition route.destination (Array.concat [|queue;finished|])
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
                        // replace and return if we took it from the queue
                        // add and return if we took it from finished
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
                        let indexOfMatch = queue |> Array.tryFindIndex (fun n -> n.position = route.destination)
                        match indexOfMatch with
                        | Some(i) ->
                            Array.concat [|
                                queue[0..(i - 1)];
                                [|newNode|];
                                queue[(i + 1)..(queue.Length - 1)]|]
                        | None -> Array.concat [|queue; [|newNode|]|]
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
            //printfn "evaluating (%d, %d)" first.position.x first.position.y
            // expand the first in queue
            let queueAfterUp = evaluateRouteOption first.r_up first sortedQueue
            let queueAfterRight = evaluateRouteOption first.r_right first queueAfterUp
            let queueAfterDown = evaluateRouteOption first.r_down first queueAfterRight
            let queueAfterLeft = evaluateRouteOption first.r_left first queueAfterDown

            // take first off the queue and move it to finished
            let nextQueue = queueAfterLeft[1..(queueAfterLeft.Length - 1)]
            //printfn "Queue after processing"
            //nextQueue 
            //|> Array.iter (fun e ->
            //    printfn "     (%d, %d) %d" e.position.x e.position.y (e.heuristicCost.Value + e.distanceTo.Value)
            //    )
            let nextFinished = Array.concat [|finished; [|first|]|]
            aStarIteration nodes nextQueue nextFinished goalPosition
let aStarLeastCost (nodes:Node[]) (queue:Node[]) (finished:Node[]) goalPosition = 
    try
        let finished = aStarIteration nodes queue finished goalPosition
        finished[finished.Length - 1].distanceTo.Value
    with
        | ex -> printfn "burp"; System.Int32.MaxValue
let pathSum3Ways (input:int[][]) height width maxInt maxRowsToTravel =

    let getColumn x = [|0..(height - 1)|] |> Array.map (fun i -> input[i][x])
    let processNode x y (thisColumn:int[]) (nextColumn:int[]) = 
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
                let targetRow = y + offset
                if targetRow < 0 || targetRow >= height then maxInt
                else thisColumn[targetRow]
                )
        let valInRightColumn i =
            let offsetPos = y - maxRowsToTravel + i
            if offsetPos < 0 || offsetPos >= height then maxInt
            else nextColumn[offsetPos]
        let addNextColumn (sumValsAtOffset:int[]) = 
            sumValsAtOffset
            |> Array.mapi (fun i verticalSum -> (valInRightColumn i) + verticalSum)
        
        valsThisColumn ()
        |> sumsUpAndDown 
        |> addNextColumn             
        |> Array.min

    let rec processColumn x (nextColumn:int[]) = 
        if x = -1 then nextColumn
        else
            let thisColumn = getColumn x 
            let newNextColumn = 
                [|0..(height - 1)|]
                |> Array.map (fun y -> processNode x y thisColumn nextColumn)
            processColumn (x - 1) newNextColumn

    let lastColumn = getColumn (width - 1)
    processColumn (width - 2) lastColumn
    |> Array.min

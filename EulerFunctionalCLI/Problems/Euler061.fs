module Euler061

open Conversions

let run () =

    let makeNGonals funct min max =
        let sequence =
            { 1..max }
            |> Seq.map funct
            |> Seq.filter (fun n -> n >= min && n < max)

        let bools = seqToBoolsArray sequence max
        (sequence, bools)
    let getNextNumberSet (polygons:seq<int>) (currentSet:seq<int[] * int>) = 
        currentSet
        |> Seq.filter (fun (numsArray, last2) -> last2 > 10)
        |> Seq.collect (fun (numsArray, last2) ->
            polygons 
            |> Seq.filter (fun numNext -> 
                    let numNextFirst2 = numNext / 100
                    Array.contains numNext numsArray = false && last2 = numNextFirst2
                )
            |> Seq.map (fun numNext -> 
                let numNextLast2 = numNext % 100
                (Array.append numsArray [|numNext|], numNextLast2))
            )
    let hasAllPolygons (polyBools:List<bool[]>) (nums:int[]) = 
        let trisInNums = nums |> Array.filter (fun x -> polyBools[0][x])
        let squaresInNums = nums |> Array.filter (fun x -> polyBools[1][x])
        let pentsInNums = nums |> Array.filter (fun x -> polyBools[2][x])
        let hexesInNums = nums |> Array.filter (fun x -> polyBools[3][x])
        let heptsInNums = nums |> Array.filter (fun x -> polyBools[4][x])
        let octsInNums = nums |> Array.filter (fun x -> polyBools[5][x])

        let triCount = trisInNums.Length 
        let squareCount = squaresInNums.Length 
        let pentCount = pentsInNums.Length 
        let hexCount = hexesInNums.Length 
        let heptCount = heptsInNums.Length 
        let octCount = octsInNums.Length 

        // throw out any where an entire polygon is missing
        // also, no duplicates except with hexes and tris
        if triCount = 0 || triCount > 2 then false
        elif squareCount <> 1 then false
        elif pentCount <> 1 then false
        elif hexCount = 0 || hexCount > 2 then false
        elif heptCount <> 1 then false
        elif octCount <> 1 then false

        else true

    let min = 1001
    let max = 10000

    let triangles, triangleBools = makeNGonals (fun n -> n * (n + 1) / 2) min max
    let squares, squareBools = makeNGonals (fun n -> n * n) min max
    let pents, pentBools = makeNGonals (fun n -> n * ((3 * n) - 1) / 2) min max
    let hexes, hexBools = makeNGonals (fun n -> n * ((2 * n) - 1)) min max
    let hepts, heptBools = makeNGonals (fun n -> n * ((5 * n) - 3) / 2) min max
    let octs, octBools = makeNGonals (fun n -> n * ((3 * n) - 2)) min max
    let polygons = seq [hexes; hepts; pents; squares; triangles] |> Seq.concat |> Seq.distinct
    let polyBools = [triangleBools; squareBools; pentBools; hexBools; heptBools; octBools]

    let result = 
        octs
        |> Seq.map (fun num1 -> ([|num1|], (num1 % 100)))
        |> getNextNumberSet polygons
        |> getNextNumberSet polygons
        |> getNextNumberSet polygons
        |> getNextNumberSet polygons
        |> getNextNumberSet polygons
        |> Seq.filter (fun (nums, last2) -> nums[0] / 100 = last2)
        |> Seq.filter (fun (nums, last2) -> hasAllPolygons polyBools nums)
    let answer = 
        (Array.ofSeq result)[0] 
        |> fst 
        |> Array.sum
        |> intToString
    answer
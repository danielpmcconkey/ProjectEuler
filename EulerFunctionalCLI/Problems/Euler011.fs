module Euler011
open System.Text.RegularExpressions


let run () =

    (*
     * this was the first of the F# Euler problems where I felt like I was 
     * doing functional right. It's very satisfying to create a pipeline like
     *
     *      let numbers = 
                inputString
                |> removeFirstLineBreak
                |> removeLastLineBreak
                |> splitLines
                |> removeIndents
                |> splitSpaces
                |> convertToNumbers
     *
     *)
 
    let inputString = @"
        08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
        49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
        81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
        52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
        22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
        24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
        32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
        67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
        24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
        21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
        78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
        16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
        86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
        19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
        04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
        88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
        04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
        20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
        20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
        01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48
        "

    let removeFirstLineBreak s = 
        Regex.Replace(s, @"^\r\n", "")

    let removeLastLineBreak s = 
        Regex.Replace(s, @"[\s\r\n]{1,}$", "")

    let splitLines s = 
        Regex.Replace(s, @"\r\n", "-").Split("-")

    let removeIndents (strings: string[]) =
        seq { for s in strings do Regex.Replace(s, @"^[\s]{1,}", "") } |> Seq.toArray

    let splitSpaces (strings: string[]) =
        seq { for s in strings do s.Split(" ") } |> Seq.toArray

    let convertToNumbers (strings: string[][]) =
        let stringToNumber s = 
            System.Int32.Parse(s)
        seq { for stringArray in strings do
            seq { for s in stringArray do stringToNumber s } |> Seq.toArray
        } |> Seq.toArray

    let numbers = 
        inputString
        |> removeFirstLineBreak
        |> removeLastLineBreak
        |> splitLines
        |> removeIndents
        |> splitSpaces
        |> convertToNumbers

    let gridWidth = 20
    let gridHeight = 20
    let howManyToConnect = 4

    (*
     * For each number in the grid, there are four products you need to test:
     *      1) Horizontal, going left to right
     *      2) Vertical, going top to bottom
     *      3) Diagonal, going from up left to bottom right
     *      4) Diagonal, going from up right to bottom left
     *
     * So we're gonna go through this grid 4 times and check each point for 
     * products in the 4 different directions. That's the conceptual flow. What
     * we'll do programatically is, for row Y, we'll use a starting point of 
     * column X and calculate the product at that starting point. We'll then 
     * take the max product from the row (the max of the products starting from
     * each X) and call that the row max. We'll then determine the max of the 
     * row max values and that'll be the max for the whole grid in that 
     * direction. Do that process for each of the product directions and take 
     * the max of all of those.
     *)

    let maxHorizontal = 

        let multiplyHorizontal x y = 
            if x > gridWidth - howManyToConnect then -1
            else
                numbers[y][x] * numbers[y][x + 1] * numbers[y][x + 2] * numbers[y][x + 3]

        let maxHorizontalForRow y =
            seq { for x in 0..(gridWidth - howManyToConnect) do multiplyHorizontal x y}
            |> Seq.max

        seq { for y in 0..(gridHeight - howManyToConnect) do maxHorizontalForRow y }
        |> Seq.max

    let maxVertical = 

        let multiplyVertical x y = 
            if y > gridHeight - howManyToConnect then -1
            else
                numbers[y][x] * numbers[y + 1][x] * numbers[y + 2][x] * numbers[y + 3][x]

        let maxVerticalForRow y =
            seq { for x in 0..(gridWidth - howManyToConnect) do multiplyVertical x y}
            |> Seq.max

        seq { for y in 0..(gridHeight - howManyToConnect) do maxVerticalForRow y }
        |> Seq.max


    let maxULDR = // up left to down right diagonal

        let multiplyULDR x y = 
            if x > gridWidth - howManyToConnect then -1
            elif y > gridHeight - howManyToConnect then -1
            else
                numbers[y][x] * numbers[y + 1][x + 1] * numbers[y + 2][x + 2] * numbers[y + 3][x + 3]

        let maxULDRForRow y =
            seq { for x in 0..(gridWidth - howManyToConnect) do multiplyULDR x y}
            |> Seq.max

        seq { for y in 0..(gridHeight - howManyToConnect) do maxULDRForRow y }
        |> Seq.max

    let maxURDL = // up right to down left diagonal

        let multiplyURDL x y = 
            if x - howManyToConnect < 0 then -1
            elif y > gridHeight - howManyToConnect then -1
            else
                numbers[y][x] * numbers[y + 1][x - 1] * numbers[y + 2][x - 2] * numbers[y + 3][x - 3]

        let maxURDLForRow y =
            seq { for x in 0..(gridWidth - howManyToConnect) do multiplyURDL x y}
            |> Seq.max

        seq { for y in 0..(gridHeight - howManyToConnect) do maxURDLForRow y }
        |> Seq.max

    let answer = [maxHorizontal; maxVertical; maxULDR; maxURDL] |> Seq.max
    answer.ToString()
    
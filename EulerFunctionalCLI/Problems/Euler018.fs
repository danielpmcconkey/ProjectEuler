module Euler018
open System.Text.RegularExpressions

let run () = 


    let inputString = @"
            75
            95 64
            17 47 82
            18 35 87 10
            20 04 82 47 65
            19 01 23 75 03 34
            88 02 77 73 07 63 67
            99 65 04 28 06 16 70 92
            41 41 26 56 83 40 80 70 33
            41 48 72 33 47 32 37 16 94 29
            53 71 44 65 25 43 91 52 97 51 14
            70 11 33 28 77 73 17 78 39 68 17 57
            91 71 52 38 17 14 91 43 58 50 27 29 48
            63 66 04 68 89 53 67 30 73 16 69 87 40 31
            04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
         "

    let removeFirstLineBreak s = 
        Regex.Replace(s, @"^[\r\n]{1,}", "")

    let removeLastLineBreak s = 
        Regex.Replace(s, @"[\s\r\n]{1,}$", "")

    let splitLines s = 
        Regex.Replace(s, @"[\r\n]{1,}", "-").Split("-")

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

    let getMaxFromRows (rowTop:int[]) (rowBottom:int[]) =

        seq { for i in 0 .. (rowTop.Length - 1) 
        do 
            let thisVal = rowTop[i] 
            let left = thisVal + rowBottom[i]
            let right = thisVal + rowBottom[i + 1]
            if left >= right then left else right
        } |> Seq.toArray
    
    let lastRow = Seq.last numbers
    let allButLast = numbers[..(numbers.Length - 2)] |> Seq.rev
    let route = 
        allButLast 
        |> Seq.fold (fun acc a -> (getMaxFromRows a acc)) lastRow

    route[0].ToString()

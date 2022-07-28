module Euler036
open Conversions

let run () = 
    let isPalindromeString (s : string)  =
        let stringArray = stringToChars s
        let length = stringArray.Length
        if length = 1 then true
        elif length = 2 then 
            if stringArray[0] = stringArray[1] then true else false
        else
            let halfWayPoint = (length / 2) + 1
            let seqOfTruths =
                seq {
                    for i in 0 .. halfWayPoint do
                        let checkLeft = stringArray[i]
                        let checkRight = stringArray[length - i - 1]
                        if checkLeft = checkRight then 0 else 1
                    }
        
            let sumOfTruths = Seq.sum seqOfTruths
            if sumOfTruths = 0 then true else false
    let isPalindromeBase10 (n : int)  = n |> intToString |> isPalindromeString
    let isPalindromeBase2 (n : int)  = n |> intToBase2String |> isPalindromeString

    let limit = (int)1e6
    [1..(limit - 1)] 
    |> List.filter (fun i -> i |> isPalindromeBase10 && i |> isPalindromeBase2)
    |> List.sum
    |> intToString


module Euler079
open Conversions
open IO

let run () =

    (*
    This is a markedly different approach than my C# solution. Runs in 18 ms.
    *)

    let getDistinctKeys (keyLogs:int[][]) =
        [|0..9|]
        |> Array.filter (fun i ->
            keyLogs 
            |> Array.filter (fun k -> k[0] = i || k[1] = i || k[2] = i)
            |> Array.length > 0
            )
    let isNext n (keyLogs:int[][]) =
        // is n the left-most in all the keylogs it is present in 
        keyLogs
        |> Array.map (fun log ->
            match log with
            | x when x[0] = n -> 0
            | x when x[1] = n -> 1
            | x when x[2] = n -> 2
            | _ -> -1
            )
        |> (fun a -> (Array.max a) = 0)
    let shiftLeft n (keyLogs:int[][]) =
        // find any keylog with n as its first value and, for those, remove the first
        // postion, shift the other two values left, and add a -1 to the end
        keyLogs
        |> Array.map (fun a ->
            if a[0] = n then [|a[1];a[2];-1|]
            else a
            )
    let rec getNext (currentPassword:string) (keyLogs:int[][]) (distinctKeys:int[]) =
        // determine which number is always left-most in the current keylogs list
        // then remove that digit and shift key logs left
        // repeat until your password has used every digit
        // assumes no repeating digits
        if currentPassword.Length = distinctKeys.Length then currentPassword
        else
            let nextDigit = distinctKeys |> Array.find (fun i -> isNext i keyLogs)
            let nextKeys = shiftLeft nextDigit keyLogs
            let nextPassword = currentPassword + (intToString nextDigit)
            getNext nextPassword nextKeys distinctKeys

    let keylogs = get79Input ()
    let distinctKeys = getDistinctKeys keylogs
    getNext "" keylogs distinctKeys


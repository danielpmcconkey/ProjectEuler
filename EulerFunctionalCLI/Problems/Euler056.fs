module Euler056

open Conversions
open Algorithms
open System.Numerics

let run () =
    (*
    Reading the problem thread, user euler points out the following:
    
    99 * (log10 90) = 193.5. Therefore, 90^99 will contain 194 digits. If 
    your average digit is a 5, then your expected sum for 90^99 is 970. 
    Using the same method, you get this:
    
         9 ^  9 yields a digit sum of:  45
        19 ^ 19 yields a digit sum of: 125
        29 ^ 29 yields a digit sum of: 215
        39 ^ 39 yields a digit sum of: 315
        49 ^ 49 yields a digit sum of: 415
        59 ^ 59 yields a digit sum of: 525
        69 ^ 69 yields a digit sum of: 635
        79 ^ 79 yields a digit sum of: 750
        89 ^ 89 yields a digit sum of: 870
        99 ^ 99 yields a digit sum of: 990

    Given how sharp the drop is as numbers go down, he felt a safe bet was to 
    start with a and b both equalling 90. This allows the program to run in 28
    milliseconds. Setting my minima to 1 has it run in 61 milliseconds. So, 
    yeah...wow.
    *)

    let digitSum (a:int) b =
        let bigA = new BigInteger(a)
        {1..b}
        |> Seq.fold (fun acc elem -> bigA * acc) 1I
        |> bigIntToIntArray
        |> Array.sum
    let minA = 90
    let minB = 90
    let maxA = 100
    let maxB = 100

    let allA = { minA..maxA }
    let allB = { minB..maxB }
    crossJoinSequences allA allB
    |> Seq.map (fun (a, b) -> digitSum a b)
    |> Seq.max
    |> intToString
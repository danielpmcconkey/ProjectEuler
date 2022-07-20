module Euler014

let run () =

    (*
    This was a pretty straightforward port from C# to F# and highlights 
    functional ways for dealing with things like loops. I'm particularly a fan
    of the recursion I put in for getCollatzLength.

    One thing surprised me. In trying to improve upon the C# version, I figured
    I could memoize. I first tried the collatzNext function and next the 
    getCollatzLength function. Both made the  solution take slightly longer. I 
    guess there's some overhead for memoization and this problem didn't have 
    enough repetition to make that overhead worthwhile.
    *)

    let collatzNext n = 
        if n % 2L = 0L then n / 2L
        else (n * 3L) + 1L

    let rec getCollatzLength n =
        if n = 1L then 1L
        else (getCollatzLength (collatzNext n)) + 1L

    let toString n = 
        n.ToString()

    let limit = (int64) 1e6
    seq {for i in 1L..limit do (i, getCollatzLength i) }
    |> Seq.maxBy (fun (i, collatzLength) -> collatzLength)
    |> fst
    |> toString

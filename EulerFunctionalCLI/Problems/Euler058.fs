module Euler058
open Conversions
open Primes

let run () =
    (*
    I still haven't figured out a great way to accumulate multiple changing 
    values over large periods in F#. Here I use Seq.unfold, but I'm using it 
    wrong. The problem is too big for recursion and I can't use a map function 
    without also resorting to mutable variables. This is the best I've got. :(

    Also, this is pretty slow (~3600 ms) because I'm calling isPrime over and 
    over. I typically like to generate primes up front using a sieve, but the 
    final side length of 26241 would require primes up to 26241^2. It takes a 
    tad bit longer to generate all primes <= 688MM
    *)
    let unfolded =
        (1, 1, 0)
        |> Seq.unfold (fun state ->
            let priorSideLength, totalCount, primesCount = state
            if (priorSideLength > 1 && (primesCount * 10) < totalCount) then None 
            else
                let sideLength = priorSideLength + 2
                let sideLengthMinus1 = sideLength - 1
                let lowerRight = sideLength * sideLength
                let lowerLeft = lowerRight - sideLengthMinus1
                let upperLeft = lowerLeft - sideLengthMinus1
                let upperRight = upperLeft - sideLengthMinus1

                let primesThisLength =
                    [ upperRight; lowerLeft; upperLeft ]
                    |> List.map (fun x -> if isPrime x then 1 else 0)
                    |> List.sum

                let nextPrimesCount = primesCount + primesThisLength
                let next = (sideLength, totalCount + 4, nextPrimesCount)
                Some(next, next))

    let last = unfolded |> Seq.last
    let sideLength, totalCount, primesCount = last
    sideLength |> intToString
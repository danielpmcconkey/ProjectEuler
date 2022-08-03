module Euler053
open Conversions

let run () =
    
    (*
    There is a way more sophisticated way to do this using Pascal's triangle, 
    but this way runs in 169 ms and is super easy to read / understand. I don't
    see a need to nerd out on this one any further.
    *)
    
    let rec factorialBigInteger (n:bigint) = if n = 0I then 1I else n * factorialBigInteger (n - 1I)
    let combinatoric (r:int) (n:int) = 
        let nfact = factorialBigInteger (bigint n)
        let rfact = factorialBigInteger (bigint r)
        let nMinusRFact = factorialBigInteger (bigint (n-r))
        nfact / (rfact * nMinusRFact)

    [|1..100|]
    |> Array.collect (fun n ->
        [|1..n|] 
        |> Array.map (fun r -> (n, r))
        )
    |> Array.filter (fun (n, r) -> combinatoric r n > 1000000I)
    |> Array.length
    |> intToString
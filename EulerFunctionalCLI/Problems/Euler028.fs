module Euler028

(* Sorry, I saw a code golf moment and I took it. *)
let run() = (1 + ([for n in 3..2..1001 do (4 * (n * n)) - (6 * n) + 6] |> List.sum)).ToString()
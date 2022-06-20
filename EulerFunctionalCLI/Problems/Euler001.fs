module Euler001


let run () =

    let limit = 1000

    let isDiv3or5 n =
        if n % 3 = 0 || n % 5 = 0  then true
        else false

    isDiv3or5 9
    let allProductsOf305 = 
        seq { 
            for n in 1 .. (limit - 1) do
                if isDiv3or5 n then 
                    n
        }

    let answer = 
        allProductsOf305
        |> Seq.sum

    answer.ToString()
    



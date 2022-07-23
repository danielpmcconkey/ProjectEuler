module Euler021

let run () = 

    let getDivisorsSum n = 
        if n = 1 then 0
        else
            let last = 
                (1, n, 0)
                |> Seq.unfold (fun state ->
                    let i, lowestOpposite, factorSum = state
                    if i >= lowestOpposite then 
                        None
                    elif n % i = 0 then
                        let opposite = n / i
                        if opposite = i then
                            Some(state, (i + 1, opposite, factorSum + i))
                        else
                            Some(state, (i + 1, opposite, factorSum + opposite + i))
                    else                
                        Some(state, (i + 1, lowestOpposite, factorSum)))
                |> Seq.last
            let i, lowestOpposite, factorSum = last
            factorSum - n

    let isAmicableNumber n = 
        let divSumN = getDivisorsSum n 
        if n = divSumN then false
        else 
            let m = getDivisorsSum divSumN 
            if m = n then true else false
    
    let toString n = n.ToString()

    let limit = 10000
    [3..limit] 
    |> List.filter(fun x -> isAmicableNumber x)
    |> List.sum
    |> toString

    



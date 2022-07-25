module Euler032

let run () = 

    let intToDigitList n = n.ToString().ToCharArray() |> Array.toList |> List.map (fun c -> (int)c - (int)'0')
    let doesIntHaveDuplicates n = 
        let a = intToDigitList n
        let d = List.distinct a
        a.Length = d.Length
    let factorPairs n = 
        let sqrtN = (int)(floor (sqrt ((float)n)))  
        [1..sqrtN]
        |> List.fold ( fun factorsList i -> 
            if n % i = 0 then
                let opposite = n / i
                let newFactors = (i, opposite)::factorsList 
                newFactors
            else factorsList
        ) ([])
    let isPanDigital n m =
        let an = intToDigitList n
        let am = intToDigitList m
        let ap = intToDigitList (n * m)
        let a' = an @ am @ ap
        let sorted = a' |> List.sort
        if sorted[0] = 0 || sorted.Length <> 9 then false
        elif sorted |> List.distinct |> List.length <> 9 then false
        else true
    let toString n = n.ToString()

    [1234..9877] 
    |> List.filter (fun x -> doesIntHaveDuplicates x)
    |> List.collect (fun x -> factorPairs x)
    |> List.map (fun (n, m) -> (n, m, n*m))
    |> List.filter (fun (n, m, p) -> isPanDigital n m)
    |> List.map (fun (n, m, p) -> p)
    |> List.distinct 
    |> List.sum
    |> toString



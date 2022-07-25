module Euler033

let run () = 

    (*
    Problem 33 was rather complicated in F#, more so than C#. There are 53 
    lines of meaningful code here. If you count the Fraction.Reduce and 
    CommonAlgorithms.GetFactors methods, there are 56 meaningful lines in the 
    C# solution. So maybe it's not more complicate, but it looks it to me. I 
    don't know. Maybe I'm just weird.
    *)
    let factorize n = 
        let sqrtN = (int)(floor (sqrt ((float)n)))  
        [1..sqrtN]
        |> List.fold ( fun factorsList i -> 
            if n % i = 0 then
                let opposite = n / i
                let newFactors = if opposite = i then factorsList @ [i] else factorsList @ [i; opposite] 
                newFactors
            else factorsList
        ) ([])

    let reduce numerator denominator = 
        if numerator = 0 || denominator = 0 then (numerator, denominator)
        else
            let numFactors = factorize numerator
            let denomFactors = factorize denominator
            let gcf = 
                numFactors 
                |> List.collect (fun f1 -> denomFactors |> List.map (fun f2 -> (f1,f2)))
                |> List.filter (fun (x, y) -> x = y)
                |> List.maxBy (fun (x, y) -> x)
                |> fst
            (numerator / gcf, denominator / gcf)

    let reduceTuple t = reduce (fst t) (snd t)

    let isDigitCancelling numerator denominator =
        if ((numerator % 10 = 0) && (denominator % 10 = 0)) then false // example of trivial reduction
        else
            let origFraction = reduce numerator denominator
            let numeratorTens = (int)(floor ((float)numerator / 10.0))
            let denominatorTens = (int)(floor ((float)denominator / 10.0))
            let numeratorOnes = numerator % 10
            let denominatorOnes = denominator % 10
            if numeratorTens = denominatorOnes && denominatorTens <> 0 then
                let newFraction = reduce numeratorOnes denominatorTens
                if ((fst newFraction) = (fst origFraction)) && ((snd newFraction) = (snd origFraction)) then true 
                else false
            elif (denominatorTens = numeratorOnes && denominatorOnes <> 0) then
                let newFraction = reduce numeratorTens denominatorOnes
                if ((fst newFraction) = (fst origFraction)) && ((snd newFraction) = (snd origFraction)) then true 
                else false
            else false

    let toString n = n.ToString()

    let numbers = [10..99]
    numbers
    |> List.collect (
        fun numerator -> 
            numbers 
            |> List.filter (fun x -> x > numerator) 
            |> List.map (fun denominator -> (numerator, denominator)))
    |> List.filter (fun (n, d) -> isDigitCancelling n d)
    |> List.fold (fun acc elem -> ((fst acc) * (fst elem), (snd acc) * (snd elem))) (1, 1)
    |> reduceTuple
    |> snd
    |> toString

module Euler033
open DomainTypes
open FractionCalculator

let run () = 

    (*
    Problem 33 was rather complicated in F#, more so than C#. There are 53 
    lines of meaningful code here. If you count the Fraction.Reduce and 
    CommonAlgorithms.GetFactors methods, there are 56 meaningful lines in the 
    C# solution. So maybe it's not more complicate, but it looks it to me. I 
    don't know. Maybe I'm just weird.
    *)
 

    let isDigitCancelling f =
        if ((f.numerator % 10 = 0) && (f.denominator % 10 = 0)) then false // example of trivial reduction
        else
            let origFraction = reduce f
            let numeratorTens = (int)(floor ((float)f.numerator / 10.0))
            let denominatorTens = (int)(floor ((float)f.denominator / 10.0))
            let numeratorOnes = f.numerator % 10
            let denominatorOnes = f.denominator % 10
            if numeratorTens = denominatorOnes && denominatorTens <> 0 then
                let newFraction = reduce (toFraction numeratorOnes denominatorTens)
                if (newFraction.numerator = origFraction.numerator) 
                    && (newFraction.denominator = origFraction.denominator) then true 
                else false
            elif (denominatorTens = numeratorOnes && denominatorOnes <> 0) then
                let newFraction = reduce (toFraction numeratorTens denominatorOnes)
                if (newFraction.numerator = origFraction.numerator) 
                    && (newFraction.denominator = origFraction.denominator) then true
                else false
            else false

    let toString n = n.ToString()
    let getDenominator f = f.denominator

    let numbers = [10..99]    
    numbers
    |> List.collect (
        fun numerator -> 
            numbers 
            |> List.filter (fun x -> x > numerator) 
            |> List.map (fun denominator -> toFraction numerator denominator))
    |> List.filter (fun f -> isDigitCancelling f)
    |> List.fold (fun acc elem -> 
        toFraction (acc.numerator * elem.numerator) (acc.denominator * elem.denominator)
        ) (toFraction 1 1)
    |> FractionCalculator.reduce
    |> getDenominator
    |> toString
    
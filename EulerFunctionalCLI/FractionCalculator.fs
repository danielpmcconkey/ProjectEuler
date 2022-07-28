module FractionCalculator
open DomainTypes
 

let toFraction n d = {numerator = n; denominator = d}
let reduce f = 
    if f.numerator = 0 || f.denominator = 0 then f
    else
        let numFactors = Algorithms.factorize f.numerator
        let denomFactors = Algorithms.factorize f.denominator
        let gcf = 
            numFactors 
            |> List.collect (fun f1 -> denomFactors |> List.map (fun f2 -> (f1,f2)))
            |> List.filter (fun (x, y) -> x = y)
            |> List.maxBy (fun (x, y) -> x)
            |> fst
        {numerator = f.numerator / gcf; denominator = f.denominator / gcf}


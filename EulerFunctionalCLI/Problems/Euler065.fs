module Euler065

open System.Numerics

type BigFraction = {
    numerator : BigInteger;
    denominator : BigInteger;
}

let run () =

    let add n x = x + n
    let multiply n x = x * n
    let divide n x = n / x
    let addCharNums x y = 
        let xNum = System.Int32.Parse(x.ToString())
        let yNum = System.Int32.Parse(y.ToString())
        add xNum yNum

    let getECoefficientAtPosition p = 
        let mod3 = p % 3
        if mod3 = 0 then 1
        else if mod3 = 1 then 1
        else divide p 3 |> add 1 |> multiply 2

    let reciprocate f = 
        let numerator = f.denominator
        let denominator = f.numerator
        { numerator = numerator; denominator = denominator}

    let addBigFractions f1 f2 =
        let normalizedDenominator = f1.denominator * f2.denominator
        let newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
        let newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;
        { numerator = newF1Numerator + newF2Numerator; denominator = normalizedDenominator }

    let addCoefficient c f =
        let cAsFraction = { numerator = c; denominator = 1 }
        addBigFractions cAsFraction f

    let getConvergenceAtCoefficient 
        (priorFraction:BigFraction) 
        (coefficient:int) = 
    
        // add the primary coefficient and invert
        addBigFractions
            { numerator = coefficient; denominator = 1 }
            priorFraction
        |> reciprocate


    // set up e as a continued fraction
    let targetPosition = 100
    let coefficient_0 = 2
    let subsequentCoefficients = seq {for i in 1 .. targetPosition - 1 do getECoefficientAtPosition i }

    let revCoefficients = subsequentCoefficients |> Seq.rev
    let starterFraction = { numerator = 0; denominator = 1 }
    let convergence = 
        Seq.fold (fun x y -> getConvergenceAtCoefficient x y) starterFraction revCoefficients
        |> addBigFractions { numerator = 2; denominator = 1 }


    let numeratorAsChars = convergence.numerator.ToString().ToCharArray()        
         
    let answer = Seq.fold (fun x y -> addCharNums x y) 0 numeratorAsChars

    printfn "answer: %d" answer



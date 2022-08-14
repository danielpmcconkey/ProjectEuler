module FractionCalculator

open DomainTypes
open Conversions


let add (f1:Fraction) (f2:Fraction) =
    let normalizedDenominator = f1.denominator * f2.denominator
    let newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
    let newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;
    let f:Fraction = { 
        numerator = newF1Numerator + newF2Numerator
        denominator = normalizedDenominator 
        }
    f
let addLong (f1:FractionLong) (f2:FractionLong) =
    let normalizedDenominator = f1.denominatorLong * f2.denominatorLong
    let newF1Numerator = normalizedDenominator / f1.denominatorLong * f1.numeratorLong;
    let newF2Numerator = normalizedDenominator / f2.denominatorLong * f2.numeratorLong;
    let f:FractionLong = { 
        numeratorLong = newF1Numerator + newF2Numerator
        denominatorLong = normalizedDenominator 
        }
    f
let addBig (f1:FractionBig) (f2:FractionBig) =
    let normalizedDenominator = f1.denominatorBig * f2.denominatorBig
    let newF1Numerator = normalizedDenominator / f1.denominatorBig * f1.numeratorBig;
    let newF2Numerator = normalizedDenominator / f2.denominatorBig * f2.numeratorBig;
    let f:FractionBig = { 
        numeratorBig = newF1Numerator + newF2Numerator
        denominatorBig = normalizedDenominator 
        }
    f


let continuedFractionOfSqrtN n =
    // https://web.archive.org/web/20151221205104/http://web.math.princeton.edu/mathlab/jr02fall/Periodicity/mariusjp.pdf
    let rec checkAlpha a_0 twiceAlpha_0 b_i c_i alphas =
        let a_i = (a_0 + b_i) / c_i
        let newAlphas = Array.concat [| alphas; [| a_i |] |]
        let newB_i = (a_i * c_i) - b_i
        let newC_i = (n - newB_i * newB_i) / c_i
        // check if we're done
        if a_i = twiceAlpha_0 then
            { firstCoefficient = a_0
              subsequentCoefficients = newAlphas
              doCoefficientsRepeat = true }
        else
            checkAlpha a_0 twiceAlpha_0 newB_i newC_i newAlphas
    // init static values
    let a_0 = (int) (floor (sqrt ((float) n)))
    let twiceAlpha_0 = a_0 * 2
    // init starter values
    let b_i = a_0
    let c_i = n - (a_0 * a_0)
    let alphas = [||]
    checkAlpha a_0 twiceAlpha_0 b_i c_i alphas

let reciprocate (f: Fraction) =
    let numerator = f.denominator
    let denominator = f.numerator    
    { numerator = numerator
      denominator = denominator }
let reciprocateLong (f: FractionLong) =
    let numerator = f.denominatorLong
    let denominator = f.numeratorLong
    { numeratorLong = numerator
      denominatorLong = denominator }
let reciprocateBig (f: FractionBig) =
    let numerator = f.denominatorBig
    let denominator = f.numeratorBig
    { numeratorBig = numerator
      denominatorBig = denominator }

let reduce (f: Fraction) =
    if f.numerator = 0 || f.denominator = 0 then
        f
    else
        let numFactors = Algorithms.factorize f.numerator
        let denomFactors = Algorithms.factorize f.denominator

        let gcf =
            numFactors
            |> List.collect (fun f1 -> denomFactors |> List.map (fun f2 -> (f1, f2)))
            |> List.filter (fun (x, y) -> x = y)
            |> List.maxBy (fun (x, y) -> x)
            |> fst

        intToFraction (f.numerator / gcf) (f.denominator / gcf)
let reduceLong (f: FractionLong) =
    if f.numeratorLong = 0 || f.denominatorLong = 0 then
        f
    else
        let numFactors = Algorithms.factorizeLong f.numeratorLong
        let denomFactors = Algorithms.factorizeLong f.denominatorLong

        let gcf =
            numFactors
            |> List.collect (fun f1 -> denomFactors |> List.map (fun f2 -> (f1, f2)))
            |> List.filter (fun (x, y) -> x = y)
            |> List.maxBy (fun (x, y) -> x)
            |> fst

        intToFractionLong (f.numeratorLong / gcf) (f.denominatorLong / gcf)
let reduceBig (f: FractionBig) =
    if f.numeratorBig = 0 || f.denominatorBig = 0 then
        f
    else
        let numFactors = Algorithms.factorizeBig f.numeratorBig
        let denomFactors = Algorithms.factorizeBig f.denominatorBig

        let gcf =
            numFactors
            |> List.collect (fun f1 -> denomFactors |> List.map (fun f2 -> (f1, f2)))
            |> List.filter (fun (x, y) -> x = y)
            |> List.maxBy (fun (x, y) -> x)
            |> fst

        intToFractionBig (f.numeratorBig / gcf) (f.denominatorBig / gcf)

let continuedFractionConvergenceAtI (f:ContinuedFraction) i = 
    let rec fractionAtJ j (currentFraction:Fraction) =
        // try
        let newNumber = f.subsequentCoefficients[j]
        // add the new number and take the reciprocal
        let newNumOver1:Fraction = { 
            numerator = newNumber 
            denominator = 1
            }
        let newCurrentFraction:Fraction =
            currentFraction
            |> add newNumOver1
            |> reciprocate
        if j = 0 
            then newCurrentFraction
            else fractionAtJ (j - 1) newCurrentFraction
    // get the convergence sequence of the subsequent coefficients
    let startFraction:Fraction = { numerator = 0; denominator = 1}
    let initialConvergence = fractionAtJ i startFraction
    // lastly, add the first coefficient
    let newFirstFraction = add initialConvergence { numerator = f.firstCoefficient; denominator = 1}
    newFirstFraction
let continuedFractionConvergenceAtILong (f:ContinuedFraction) i = 
    let rec fractionAtJ j (currentFraction:FractionLong) =
        // try
        let newNumber = f.subsequentCoefficients[j]
        // add the new number and take the reciprocal
        let newNumOver1:FractionLong = { 
            numeratorLong = newNumber 
            denominatorLong = 1L
            }
        let newCurrentFraction:FractionLong =
            currentFraction
            |> addLong newNumOver1
            |> reciprocateLong
        if j = 0 
            then newCurrentFraction
            else fractionAtJ (j - 1) newCurrentFraction
    // get the convergence sequence of the subsequent coefficients
    let startFraction:FractionLong = { numeratorLong = 0L; denominatorLong = 1L}
    let initialConvergence = fractionAtJ i startFraction
    // lastly, add the first coefficient
    let newFirstFraction = addLong initialConvergence { numeratorLong = f.firstCoefficient; denominatorLong = 1L}
    newFirstFraction
let continuedFractionConvergenceAtIBig (f:ContinuedFraction) i = 
    let rec fractionAtJ j (currentFraction:FractionBig) =
        // try
        let newNumber = f.subsequentCoefficients[j]
        // add the new number and take the reciprocal
        let newNumOver1:FractionBig = { 
            numeratorBig = newNumber 
            denominatorBig = 1I
            }
        let newCurrentFraction:FractionBig =
            currentFraction
            |> addBig newNumOver1
            |> reciprocateBig
        if j = 0 
            then newCurrentFraction
            else fractionAtJ (j - 1) newCurrentFraction
    // get the convergence sequence of the subsequent coefficients
    let startFraction:FractionBig = { numeratorBig = 0I; denominatorBig = 1I}
    let initialConvergence = fractionAtJ i startFraction
    // lastly, add the first coefficient
    let newFirstFraction = addBig initialConvergence { numeratorBig = f.firstCoefficient; denominatorBig = 1I}
    newFirstFraction
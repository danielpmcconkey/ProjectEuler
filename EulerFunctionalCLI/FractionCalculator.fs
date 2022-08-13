module FractionCalculator

open DomainTypes
open Conversions


let addBig (f1:FractionBig) (f2:FractionBig) =
    let normalizedDenominator = f1.denominator * f2.denominator
    let newF1Numerator = normalizedDenominator / f1.denominator * f1.numerator;
    let newF2Numerator = normalizedDenominator / f2.denominator * f2.numerator;
    { numerator = newF1Numerator + newF2Numerator; denominator = normalizedDenominator }

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

let reciprocateBig (f: FractionBig) =
    let numerator = f.denominator
    let denominator = f.numerator

    { numerator = numerator
      denominator = denominator }

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

let reduceBig (f: FractionBig) =
    if f.numerator = 0 || f.denominator = 0 then
        f
    else
        let numFactors = Algorithms.factorizeBig f.numerator
        let denomFactors = Algorithms.factorizeBig f.denominator

        let gcf =
            numFactors
            |> List.collect (fun f1 -> denomFactors |> List.map (fun f2 -> (f1, f2)))
            |> List.filter (fun (x, y) -> x = y)
            |> List.maxBy (fun (x, y) -> x)
            |> fst

        intToFractionBig (f.numerator / gcf) (f.denominator / gcf)

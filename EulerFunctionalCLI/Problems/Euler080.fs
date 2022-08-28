module Euler080



open DomainTypes
open Algorithms
open Conversions
open FractionCalculator

let run () =

    (*
    This is super complex code, but much of it was taken from prior problems. 
    I first get the continued fraction of all the non-perfect-squares (used in 
    problems 64 and 66). I then wrote out the decimal expansion using the 
    technique described in 
    
    https://crypto.stanford.edu/pbc/notes/contfrac/convert.html. 
    
    That part was tricky because I didn't know how many times I had to loop 
    through the repeating coefficients in my continued fraction. I arrived at 
    175 coefficients through trial and error.
    *)

    let expandCoefficients (coefficients:int[]) (countNeeded:int) =
        let rec add (c:int[]) =
            if c.Length >= countNeeded then c
            else 
                let newC = Array.concat [|c; coefficients|]
                add newC
        add coefficients
    let decimalExpansion (cf:ContinuedFraction) numDigits (padding:float) =
        let rec expand (p:FractionBig) (u:FractionBig) (digits:bigint[]) =
            let floor_p = p.numeratorBig / p.denominatorBig
            let floor_u = u.numeratorBig / u.denominatorBig
            if floor_p <> floor_u then digits
            else
                let newDigits = Array.concat [|digits; [|floor_p|]|]
                let newP = {
                    numeratorBig = 10I * (p.numeratorBig - (floor_p * p.denominatorBig))
                    denominatorBig = p.denominatorBig
                    }
                let newU = {
                    numeratorBig = 10I * (u.numeratorBig - (floor_u * u.denominatorBig))
                    denominatorBig = u.denominatorBig
                    }
                expand newP newU newDigits

        let countNeeded = (int)(ceil (padding * (float)numDigits))
        let (convergentUltimate, convergentPenultimate) = 
            expandCoefficients cf.subsequentCoefficients countNeeded
            |> (fun coefficientsArray -> {
                doCoefficientsRepeat = false
                firstCoefficient = cf.firstCoefficient
                subsequentCoefficients = coefficientsArray
                })
            |> (fun doubledCf -> 
                (continuedFractionConvergenceAtIBig doubledCf (doubledCf.subsequentCoefficients.Length - 1),
                 continuedFractionConvergenceAtIBig doubledCf (doubledCf.subsequentCoefficients.Length - 2))
                )
        expand convergentPenultimate convergentUltimate [||]


    let limit = 100
    let start = 2
    let numDigits = 100
    let padding = 1.75

    [|start..limit|]
    |> Array.filter (fun x -> isPerfectSquare x = false)
    |> Array.map (fun x -> continuedFractionOfSqrtN x)
    |> Array.map (fun cf -> decimalExpansion cf numDigits padding)
    |> Array.map (fun digits -> digits[0..(numDigits - 1)])
    |> Array.map (fun digits -> Array.sum digits)
    |> Array.sum
    |> bigIntToString


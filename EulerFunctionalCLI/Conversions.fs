module Conversions
open DomainTypes

let intToString n = n.ToString()
let stringToChars (s:string) = s.ToCharArray()
let intToIntArray n = n |> intToString |> stringToChars |> Array.map (fun x -> ((int)x) - ((int)'0'))
let intToBase2String (n:int) = System.Convert.ToString(n, 2)
let intToFraction n d = {numerator = n; denominator = d}

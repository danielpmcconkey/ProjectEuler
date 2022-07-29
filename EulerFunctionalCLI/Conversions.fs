module Conversions
open DomainTypes

let intToString n = n.ToString()
let stringToChars (s:string) = s.ToCharArray()
let intToIntArray n = n |> intToString |> stringToChars |> Array.map (fun x -> ((int)x) - ((int)'0'))
let intToBase2String (n:int) = System.Convert.ToString(n, 2)
let intToFraction n d = {numerator = n; denominator = d}
let stringToInt s = System.Int32.Parse(s)
let listIntToInt (l:List<int>) = l |> List.fold (fun acc elem -> sprintf "%s%d" acc elem) "" |> stringToInt
let intArrayToInt (l:int[]) = l |> Array.fold (fun acc elem -> sprintf "%s%d" acc elem) "" |> stringToInt
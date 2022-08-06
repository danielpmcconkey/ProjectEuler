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
let intToListInt n = n.ToString().ToCharArray() |> Array.toList |> List.map (fun c -> (int)c - (int)'0')
let combineListOfLists (ll:List<List<'T>>) = ll |> List.fold (fun acc elem -> acc @ elem) []
let floatToInt (f:float) = (int)f
let charsArrayToListInt (chars:char[]) = [for i in 0..(chars.Length - 1) do (int)chars[i] - (int)'A' + 1]
let stringToLong s = System.Int64.Parse(s)
let listIntToLong (l:List<int>) = l |> List.fold (fun acc elem -> sprintf "%s%d" acc elem) "" |> stringToLong
let longToString (l:int64) = l.ToString()
let charsArrayToListLong (chars:char[]) = chars |> Array.map (fun x -> (int64)(((int)x) - ((int)'0'))) |> Array.toList
let listLongToLong (l:List<int64>) = l |> List.fold (fun acc elem -> sprintf "%s%d" acc elem) "" |> stringToLong
let longToListLong n = n.ToString().ToCharArray() |> Array.toList |> List.map (fun c -> (int64)((int)c - (int)'0'))
let bigIntToString (n:bigint) = n.ToString()
let bigIntToCharArray (n:bigint) = n |> bigIntToString |> stringToChars 
let bigIntToListInt (n: bigint) = n |> bigIntToCharArray |> Array.map (fun c -> (int) c - (int) '0') |> Array.toList
let bigIntToIntArray (n: bigint) = n |> bigIntToCharArray |> Array.map (fun c -> (int) c - (int) '0')
let charToUpper c = System.Char.ToUpper(c)
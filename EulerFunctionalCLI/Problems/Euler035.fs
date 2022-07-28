module Euler035
open DomainTypes

let run () = 
    
    let toString n = n.ToString()
    let toChars (s:string) = s.ToCharArray() |> Array.toList
    let toListInt n = n |> toString |> toChars |> List.map (fun x -> ((int)x) - ((int)'0'))
    let getRotations (l:List<int>) = 
        [0..(l.Length - 1)] 
        |> List.map (fun i -> [i] @ [(i + 1)..(l.Length - 1)] @ [0..(i - 1)])
        |> List.map (fun i -> i |> List.map (fun j -> l[j]))
    let parseInt s = System.Int32.Parse(s)
    let toInt (l:List<int>) = l |> List.fold (fun acc elem -> sprintf "%s%d" acc elem) "" |> parseInt
    let isCircularPrime n (primesBools:bool[]) =
        if primesBools[n] <> true then false
        elif n = 2 || n = 5 then true
        else
            let digits = toListInt n
            if 
                digits |> List.contains 2 
                || digits |> List.contains 4 
                || digits |> List.contains 6 
                || digits |> List.contains 8 
                || digits |> List.contains 0 
                || digits |> List.contains 5  
                then false
            else 
                digits 
                |> getRotations 
                |> List.map (fun l -> l |> toInt)
                |> List.forall (fun x -> primesBools[x])

    let limit = (int)1e6
    let primeStarterPack = Algorithms.getPrimesUpToNSieve limit
    let primes = primeStarterPack.primes
    let primeBools = primeStarterPack.primeBools
    primes |> Array.filter (fun x -> isCircularPrime x primeBools) |> Seq.length |> toString

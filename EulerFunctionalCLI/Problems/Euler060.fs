module Euler060

open Primes
open Conversions

let run () = 
    let isValidCombo (primes:int[]) (primeBools:bool[]) = 

        // only check the last value to all the others
        // assume the others have been checked in a prior round
        let thisPrime = primes[primes.Length - 1]
        let others = [|0..(primes.Length - 2)|] |> Array.map (fun i -> primes[i])
        others
        |> Array.forall (fun o -> 
            let arrangement1 = System.Int32.Parse(sprintf "%d%d" thisPrime o)
            let arrangement2 = System.Int32.Parse(sprintf "%d%d" o thisPrime)
            if  primeBools[arrangement1] = false || primeBools[arrangement2] = false then false
            else true
        )

    let maxPrimeToMake = (int)9e7
    let maxPrimeToTry = 9000
    let primePack = getPrimesUpToNSieve maxPrimeToMake
    let primes = primePack.primes
    let primeBools = primePack.primeBools
    let maxIndex = primes |> Array.filter (fun x -> x <= maxPrimeToTry) |> Array.length
    let indices = [|1..(maxIndex - 1)|]
    let winners = 
        indices
        |> Array.collect (fun i -> 
            [|(i + 1)..(maxIndex - 1)|] 
            |> Array.map (fun j -> [|primes[i]; primes[j]|])
            )
        |> Array.filter (fun arr -> isValidCombo arr primeBools)
        |> Array.collect (fun arr -> 
            let lastIndex = primes |> Array.findIndex (fun x -> x = arr[1])
            [|(lastIndex + 1)..(maxIndex - 1)|] 
            |> Array.map (fun k -> [|arr[0]; arr[1]; primes[k]|])
            )
        |> Array.filter (fun arr -> isValidCombo arr primeBools)
        |> Array.collect (fun arr -> 
            let lastIndex = primes |> Array.findIndex (fun x -> x = arr[2])
            [|(lastIndex + 1)..(maxIndex - 1)|] 
            |> Array.map (fun l -> [|arr[0]; arr[1]; arr[2]; primes[l]|])
            )
        |> Array.filter (fun arr -> isValidCombo arr primeBools)
        |> Array.collect (fun arr -> 
            let lastIndex = primes |> Array.findIndex (fun x -> x = arr[3])
            [|(lastIndex + 1)..(maxIndex - 1)|] 
            |> Array.map (fun m -> [|arr[0]; arr[1]; arr[2]; arr[3]; primes[m]|])
            )
        |> Array.filter (fun arr -> isValidCombo arr primeBools)
    winners[0]
    |> Array.sum
    |> intToString
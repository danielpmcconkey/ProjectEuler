module Euler043
open Conversions
open Algorithms

let run () =

    (*
    This was a very difficult conversion. The run_inelegant function is nearly
    a straight port of the C# version with a different permutations function.
    It runs a good bit slower than the C# permutations function that uses 
    Heap's algorithm. I did'n have the mental energy to convert it to F#. 
    Instead, my optimization went in a different direction. 

    First, find all the 3-digit numbers divisible by 17 and all the 3-digit 
    numbers divisible by 13 and see which combinations, when smashed together
    such that the last two digits of the 13-divisible number and the first two
    of the 17-divisible number are the same.

    Ex: (130, 306) are smashed into 1306 but (143, 306) won't work because it 
    doesn't share the inner two digits.

    Now check that the resulting smash up doesn't have any duplicate digits and
    you have a potential solution. Do this same process by combining with the 
    11-divisible numbers, the 7, 5, etc.

    At that point, you have a list of numbers whose digits are all unique and 
    follow the rules for the d2 - d10 digits. So you know that your d1 must be
    the one digit that isn't already present. Find it, append it, and you now 
    have a list of all your winning solutions. Add that up and BLAMMO!!
    *)

    let findAll3DigitNumbersDivisibleByN n =
        let max = 999 / n
        [1..max] |> List.map (fun i -> (int64)(i * n))
        |> List.map (fun n -> n.ToString("000"))
        |> List.map (fun s -> s.ToCharArray())
        |> List.map (fun a -> charsArrayToListLong a)

    let innardsMatch (l1:List<int64>) (l2:List<int64>) = 
        let l1Length = List.length l1
        let l1_ultimate = l1[l1Length - 1]
        let l1_penUltimate = l1[l1Length - 2]
        if l1_ultimate = l2[1] && l1_penUltimate = l2[0] then true else false
    let combine (l1:List<int64>) (l2:List<int64>) = [l1[0]] @ l2
    let noDuplicates (l:List<int64>) = (l |> List.distinct |> List.length) = (l |> List.length)
    let combineAndCheck l1 l2 =
        l2
        |> crossJoinLists l1 
        |> List.filter (fun t -> innardsMatch (fst t) (snd t))
        |> List.map (fun t -> combine (fst t) (snd t))
        |> List.filter (fun l -> noDuplicates l)
    let getFirstListELement (l:List<'T>) = l[0]
    let appendMissingDigit l =
        [0L; 1L; 2L; 3L; 4L; 5L; 6L; 7L; 8L; 9L]
        |> List.map (fun d -> d::l)
        |> List.filter (fun l -> noDuplicates l)
        |> getFirstListELement
    let appendFirstDigits (ll:List<List<int64>>) =
        ll |> List.map (fun l -> appendMissingDigit l)

    let all2 = findAll3DigitNumbersDivisibleByN 2
    let all3 = findAll3DigitNumbersDivisibleByN 3
    let all5 = findAll3DigitNumbersDivisibleByN 5
    let all7 = findAll3DigitNumbersDivisibleByN 7
    let all11 = findAll3DigitNumbersDivisibleByN 11
    let all13 = findAll3DigitNumbersDivisibleByN 13
    let all17 = findAll3DigitNumbersDivisibleByN 17

    all17 
    |> combineAndCheck all13 
    |> combineAndCheck all11 
    |> combineAndCheck all7 
    |> combineAndCheck all5 
    |> combineAndCheck all3 
    |> combineAndCheck all2
    |> appendFirstDigits
    |> List.map (fun l -> listLongToLong l)
    |> List.sum
    |> longToString


let run_inelegant () =

    
    let fitsPattern (p:List<int>) =
        if 
            // throw out anything that wouldn't be divisible by 2 with d2, d3, d4
            (p[3] = 0 || p[3] = 2 || p[3] = 4 || p[3] = 6 || p[3] = 8) &&
            // throw out anything that wouldn't be divisible by 5 with d4, d5, d6
            (p[5] = 0 || p[5] = 5) &&
            // throw out anything that wouldn't be divisible by 3 with d3, d4, d5
            // all multiples of 3 must have the sum of its digits be divisble by 
            ((p[2]+p[3]+p[4]) % 3 = 0) &&
            // throw out anything that wouldn't be divisible by 7 with d5, d6, d7
            // there is a divisibility rule of 7, but it would be more processing
            // intensive than just checking the digits, I think
            (((p[4] * 100) + (p[5] * 10) + p[6]) % 7 = 0) &&
            // throw out anything that wouldn't be divisible by 11 with d6, d7, d8
            // there is a divisibility rule of 11, but it would be more processing
            // intensive than just checking the digits, I think
            (((p[5] * 100) + (p[6] * 10) + p[7]) % 11 = 0) &&
            // throw out anything that wouldn't be divisible by 13 with d7, d8, d9
            (((p[6] * 100) + (p[7] * 10) + p[8]) % 13 = 0) &&
            // throw out anything that wouldn't be divisible by 17 with d8, d9, d10
            (((p[7] * 100) + (p[8] * 10) + p[9]) % 17 = 0) 
            // winner, winner, chicken dinner!
            then true else false
    
    [0;1;2;3;4;5;6;7;8;9]
    |> getAllPermutationsOfList
    |> List.filter (fun l -> l |> fitsPattern)
    |> List.map (fun l -> l |> listIntToLong)
    |> List.sum
    |> longToString
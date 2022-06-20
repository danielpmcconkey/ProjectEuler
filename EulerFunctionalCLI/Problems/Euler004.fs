module Euler004


let run () =
    let powInt (n:int) (x:int) =
        int((n:float) ** (x:float))



    let convertIntToDigitsList n =

        let digitsAtOOM (oom : int) (digits : int list) = 
            if n >= powInt 10 oom then
                (n % powInt 10 (oom + 1)) / (powInt 10 oom)::digits
            else
                digits
        let ordersOfMagnitudeToSupport = 12
        let digits = [0 .. 10]
        let OOMs = [ 0 .. ordersOfMagnitudeToSupport ]
        OOMs
        //let convergence = List.fold (fun x y -> digitsAtOOM x y) digits OOMs
        //convergence
        //for i in 0..ordersOfMagnitudeToSupport do
        //    let digitCandidate = digitsAtOOM i
        //    if digitCandidate <> -1 then digitCandidate::digits
        //    else digits
        //digits

    let test = convertIntToDigitsList 44506
    test
    4
        //{
        //    ;
        //    List<int> digitsInReverse = new List<int>();
        //    for (int i = 0; i < ordersOfMagnitudeToSupport; i++)
        //    {
        //        if (n >= Math.Pow(10, i))
        //        {
        //            digitsInReverse.Add(
        //               (int)(Math.Floor(
        //                    n % Math.Pow(10, i + 1)
        //                    /
        //                    Math.Pow(10, i)
        //                    )));
        //        }
        //    }
        //    // now turn it to an array and reverse
        //    int[] digits = digitsInReverse.ToArray().Reverse().ToArray();
        //    return digits;
        //}

//let isPalindromic n =
//    for i in 
//    false










//open Algorithms
//open System

//let run () =

//    let answer = getPrimeFactorsOfLong 600851475143L |> Seq.max
//    printfn "answer: %d" answer




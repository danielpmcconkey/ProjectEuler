module Euler004


let run () =

    let threeDigitNums = seq { 100 .. 999 }

    let palindromeProducts = seq { 
        for i in threeDigitNums do 
            for j in threeDigitNums do 
                let product = i * j
                if product |> Algorithms.isPalindromeBase10 then product }
    
    let answer = Seq.max palindromeProducts
    answer.ToString()

module Euler034

let run () =
    (*
    There were 2 difficult parts to this solution. The first was remembering 
    that 0 factorial is 1. The second was naming the function that determines
    whether a number is equal to the sum of its digit factorials. isMagic was
    the best I could do.

    The reason I chose my max as 7 * 9! is that, with each new digit added to 
    your input number, you only increase your max digit factorial sum by 9!

                9 -> 1 * 9! ->   362,880
               99 -> 2 * 9! ->   725,760
              999 -> 3 * 9! -> 1,088,640
            9,999 -> 4 * 9! -> 1,451,520
           99,999 -> 5 * 9! -> 1,814,400
          999,999 -> 6 * 9! -> 2,177,280 
        9,999,999 -> 7 * 9! -> 2,540,160   <- this is the transition
       99,999,999 -> 8 * 9! -> 2,903,040

    Somewhere between 6 * 9! and 7 * 9!, the number on the left gets bigger 
    than the number on the right, meaning it's impossible for any number 
    larger than 7 * 9! to have a sum of its digit factorials to be >= to it. 
    The actual largest number whose sum of digit factorials is >= to it is
    1,999,999, but I needed to use this program to find that out. So I didn't
    count that.
    *)

    let factorials = [|1; 1; 2; 6; 24; 120; 720; 5040; 40320; 362880|]
    let toString n = n.ToString()
    let toChars (s:string) = s.ToCharArray() |> Array.toList
    let intToListInt n = n |> toString |> toChars |> List.map (fun x -> ((int)x) - ((int)'0'))
    let digitFactSum n = n |> intToListInt |> List.map (fun x -> factorials[x]) |> List.sum
    let isMagic n = (digitFactSum n) = n
    let max = factorials[9] * 7
    let min = 3
    [min..max]
    |> List.filter (fun x -> isMagic x)
    |> List.sum
    |> toString



module Euler017

let run () =

    (*
    This one's just a good ole' bit of fun to be had. I sort of wish it had the 
    extra challenge of needing spaces and hyphens. But with just the letters, 
    this is fairly straightforward. My original C# solution used enums for the
    words lists (wordsUpToNineteen and tensWords) and I can't for the life of 
    me remember why I went that way. Just using a list or array that you can 
    reference with an index seems so much easier.
    *)
    let wordsUpToNineteen = [
            ""; "one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine"; "ten";  // don't use zero
            "eleven"; "twelve"; "thirteen"; "fourteen"; "fifteen"; "sixteen"; "seventeen";
            "eighteen"; "nineteen"
    ]
    let tensWords = [    
            ""; "teen"; "twenty"; "thirty"; "forty"; "fifty"; "sixty"; "seventy";  // don't use zero or teen
            "eighty"; "ninety"
    ]

    let charToNum d = (int)d - (int)'0'

    let printNumberAsWord n = 
        if n = 1000 then "onethousand"
        else
            let nAsCharArray = n.ToString().PadLeft(4, '0').ToCharArray()
            let hundreds = charToNum nAsCharArray[1]
            let tens = charToNum nAsCharArray[2]
            let ones = charToNum nAsCharArray[3]

            let hundredsWord = if hundreds > 0 then sprintf "%shundred" wordsUpToNineteen[hundreds] else ""
            let andWord = if hundreds >= 1 && (tens > 0 || ones > 0) then "and" else ""
            let tensOnesWord = 
                
                if tens >= 2 then
                    sprintf "%s%s" tensWords[tens] wordsUpToNineteen[ones]
                    else wordsUpToNineteen[ones + (tens * 10)]
                
            sprintf "%s%s%s" hundredsWord andWord tensOnesWord

    let countLetters (s:string) = s.Length

    let limit = 1000

    let answer = seq { for i in 1..limit do i |> printNumberAsWord |> countLetters } |> Seq.sum
    answer.ToString()



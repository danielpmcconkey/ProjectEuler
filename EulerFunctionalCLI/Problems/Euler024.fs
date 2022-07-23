module Euler024

let run () = 

    (*
    This is an almost word for word port of the C# solution. The only 
    difference is that here, I use a fold instead of a for loop. Though the F# 
    version runs in about 10x slower. 7ms vs 0.7ms. I'm not certain why, but I
    don't know if I care.

    What I do care about is that I didn't adequately explain how this works in 
    the C# version. Basically, we know there are 10! different permutations of
    digits 0 - 9. That's 3,628,800 different permutations and we know we want
    to find the 1MMth. That'll be about one third of the way through our sorted
    list of permutations.

    One tenth of 10! is 362,880, which means that you will exhaust all of your
    permutations that start with "0" and you'll only be at position 362,881. Go
    another 362,880 through the sorted list, and you'll have removed all the 
    permutations that start with a "1". But, if you exhaust all of them that 
    start with "2", then you'll be at 3 * 362,880. So you know your first digit
    is a "2".

    So now, it's really just a recursion exercise. Only instead of starting at 
    postition 1 we'll start at position 2 * (1MM / 10) and instead of having 
    all 10 digits to play with, we only have 9 (because we used up 2). So our 
    new start is 725,760 and now, instead of 10! possible permutations, we only 
    have to worry about 9! possibilities. 9! is 362,880 and we only have 9 
    digits left, so each digit accounts for 40,320 permutations (9! / 9). So 
    since we already know that our answer starts with "2", we look at all the 
    permutations that start with "20". We know there are 40,320 of them, and 
    the "2" series started at 725,760, that means that "20" only takes us to 
    766,080. "21" gets us to 806,400 and we can keep going through our 
    remaining digits to see that we have to go to "28" before going over 1MM. 
    So our second digit is a "7". We're at "27" for our answer so far.

    Here's a depiction of this logic in a spreadsheet...

    /------------------------------------------------------------------------------------------------------------------------------------------------------------\
    | numerals               | target    | start   | how many digits | how much  | total number    | count per | which | this  |                     | new start |
    | left                   | position  | value   | are left        | is Left   | of permutations | digit     | block | digit | new numerals left   | value     |
    |------------------------|-----------|---------|-----------------|-----------|-----------------|-----------|-------|-------|---------------------|-----------|
    | [0,1,2,3,4,5,6,7,8,9]  | 1,000,000 |       0 |              10 | 1,000,000 |       3,628,800 |   362,880 |     2 |     2 | [0,1,3,4,5,6,7,8,9] |   725,760 |
    | [0,1,3,4,5,6,7,8,9]    | 1,000,000 | 725,760 |               9 |   274,240 |         362,880 |    40,320 |     6 |     7 | [0,1,3,4,5,6,8,9]   |   967,680 |
    | [0,1,3,4,5,6,8,9]      | 1,000,000 | 967,680 |               8 |    32,320 |          40,320 |     5,040 |     6 |     8 | [0,1,3,4,5,6,9]     |   997,920 |
    | [0,1,3,4,5,6,9]        | 1,000,000 | 997,920 |               7 |     2,080 |           5,040 |       720 |     2 |     3 | [0,1,4,5,6,9]       |   999,360 |
    | [0,1,4,5,6,9]          | 1,000,000 | 999,360 |               6 |       640 |             720 |       120 |     5 |     9 | [0,1,4,5,6]         |   999,960 |
    | [0,1,4,5,6]            | 1,000,000 | 999,960 |               5 |        40 |             120 |        24 |     1 |     1 | [0,4,5,6]           |   999,984 |
    | [0,4,5,6]              | 1,000,000 | 999,984 |               4 |        16 |              24 |         6 |     2 |     5 | [0,4,6]             |   999,996 |
    | [0,4,6]                | 1,000,000 | 999,996 |               3 |         4 |               6 |         2 |     1 |     4 | [0,6]               |   999,998 |
    | [0,6]                  | 1,000,000 | 999,998 |               2 |         2 |               2 |         1 |     1 |     6 | [0]                 |   999,999 |
    | [0]                    | 1,000,000 | 999,999 |               1 |         1 |               1 |         1 |     0 |     0 | []                  |   999,999 |
    \------------------------------------------------------------------------------------------------------------------------------------------------------------/
    *)

    let factorial (n:int) = [1..n] |> List.fold (fun acc a -> acc * a) 1

    let targetPosition = (int)1e6
    let answer, numeralsLeft, startValue =
        [1..10] 
        |> List.rev
        |> List.fold (
            fun (currentAnswer, (numeralsLeft:List<int>), startValue) howManyDigitsAreLeft -> 
                let howMuchIsLeft = targetPosition - startValue
                let totalNumberOfPermutations = factorial howManyDigitsAreLeft
                let countPerDigit = totalNumberOfPermutations / howManyDigitsAreLeft
                let whichBlock = (int)(floor (((float)howMuchIsLeft - 1.0) / (float)countPerDigit))
                let thisDigit = numeralsLeft[whichBlock]
                let newNumeralsLeft = numeralsLeft |> List.filter (fun x -> (x <> thisDigit)) 
                let newAnswer = currentAnswer + thisDigit.ToString() 
                let newStartValue = startValue + (countPerDigit * whichBlock )

                (newAnswer, newNumeralsLeft, newStartValue)
            ) ("", [0; 1; 2; 3; 4; 5; 6; 7; 8; 9], 0)

    answer


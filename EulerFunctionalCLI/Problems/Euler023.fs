module Euler023


let run () = 
    (*
    This is very similar in logic to the C# solution I used for problem 23, 
    though the mechanics are rather different. In C#, I used linq to cross 
    join the abundant numbers twice to add up all combinations. Here in F#, I
    don't believe we have that ability. So I used List.iter to mimic a 
    for i...for j set of loops. It's probably all doing the same thing under 
    the hood.

    The factorize method here started out as a copy from problem # 21, but I 
    noticed a bug in it when I tried to run 12 through it. And that showed me
    that any number whose last divisor pair were 1 apart (in this case 3 and 4)
    wouldn't count the last pair. This lead me to completely rebuild and I 
    realized that I don't really understand unfolding in F#. I was using my 
    "state" variable to mean "prior" state but it's really the accumulator. So
    now my question is, if this is the accumulator what's the second part of 
    the Some tuple?

    I'll figure it out over the weekend (I hope).

    Update, I have refactored my factorize method to use a fold rather than an
    unfold. This runs only a few milliseconds faster but it's a LOT more 
    readable.
    *)

    let factorize n = 
        let sqrtN = (int)(floor (sqrt ((float)n)))  
        [1..sqrtN]
        |> List.fold ( fun factorsList i -> 
            if n % i = 0 then
                let opposite = n / i
                let newFactors = if opposite = i then factorsList @ [i] else factorsList @ [i; opposite] 
                newFactors
            else factorsList
        ) ([])

    
    let getDiviorsSum n = (factorize n |> List.sum) - n  
    let isAbundant n = if (getDiviorsSum n > n) then true else false
    let toString n = n.ToString()
    let limit = 28123
    let bools = Array.create (limit + 1) false
    let abundants = [1 .. limit] |> List.filter(fun x -> isAbundant x)
    
    abundants
    |> List.iter (
        fun i -> 
            abundants
            |> List.iter (
                fun j -> 
                    if i + j <= limit then bools[i + j] <- true
            )
    )
    [1..limit] 
    |> List.filter(fun i -> bools[i] = false)
    |> List.sum
    |> toString

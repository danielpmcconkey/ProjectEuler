module Euler029

let run () = 

    (*
    This one was a lot of fun for me. I got to improve upon the C# solution in 
    a couple of meaningful ways. First, I got rid of the BigInteger use in 
    favor of floats. Maybe not as accurate, but floats are primitives that 
    every language supports, making this less .NET-y. Next, I ditched the two
    loops through the possible numbers in favor of my newly found cross-joining
    skills. I could've combined my cross join and my map down below, but I like
    leaving the cross join on its own. This code also runs significantly faster
    than the .NET, BTW, so yay!
    *)

    let toString n = n.ToString()
    let crossJoin lx ly =
        lx |> List.collect (fun x -> ly |> List.map (fun y -> x, y))

    let nums = [2 .. 100]
    (crossJoin nums nums)
    |> List.map (fun (x, y) -> ((float)x)**((float)y))
    |> List.distinct
    |> List.length
    |> toString


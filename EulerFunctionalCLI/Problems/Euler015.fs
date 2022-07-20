module Euler015

let run () =

    (*
    This is one where the C# solution is almost identical to the F# port. 
    What's funny is that I did them both independently. I had forgotten how I 
    did C# in the end and only remembered the Run_originalSolution() algorithm
    from C#. Instead, I remembered seeing a dynamic programming tutorial on 
    YouTube that solved this exact problem.

    https://www.youtube.com/watch?v=oBt53YbR9Kk

    In that tutorial, the guy goes through the thinking on how this recursive
    pattern works quite memorably. So I just coded up my memory of his solution
    and only remembered that I'd implemented the recursive solution in C# when
    doing this write-up. The beauty is that, because of the YouTube video, I 
    know understand *why* the recursive algorithm works.
    *)

    let cache = System.Collections.Generic.Dictionary<string, int64>();

    let rec countRoutes n m = 
        if n = 0L || m = 0L then 1L
        else
            let key = sprintf "%d|%d" n m
            let exist, value = cache.TryGetValue key
            match exist with
            | true -> value
            | _ ->
                let result = (countRoutes (n - 1L) m) + (countRoutes n (m - 1L))
                cache.Add(key, result)
                result
            
    (countRoutes 20L 20L).ToString()
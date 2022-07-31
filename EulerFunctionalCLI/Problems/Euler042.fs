module Euler042

open IO
open Conversions

let run () =
    
    let scoreWord (w:string) = stringToChars w |> charsArrayToListInt |> List.sum
    let scoreWords (words:string[]) = [for i in 0..(words.Length - 1) do scoreWord words[i]]

    // this uses the quadratic formula to reverse engineer the N value if you 
    // already know a triangle number. In this case, we don't exactly know a 
    // triangle number, but we know the biggest number we want to check for 
    // triangularity. So round up and that's the biggest N value we need for 
    // creating our list of triangles
    let findMaxTriangleN maxScore = (int)(ceil (sqrt ((2.0 * (float)maxScore) - 0.5) - 0.5))

    

    // first score all the words     
    let wordScores = get42Input () |> scoreWords 
    
    // now build triangles up to the max score
    let maxTriangleN = wordScores |> List.max |> findMaxTriangleN
    let triangles = [1..maxTriangleN] |> List.map (fun n -> (int)((0.5 * (float)n) * ((float) n + 1.0)))

    // finally pull the answer
    wordScores
    |> List.filter (fun x -> List.contains x triangles)
    |> List.length
    |> intToString
    

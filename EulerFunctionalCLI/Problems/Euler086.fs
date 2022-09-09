module Euler086
open Conversions

let run () = 
    (*
    Step one for me was envisioning the room like a cardboard box unfolding.

            (E)----------------(F)
             |     back wall    |
             |                  |
    (E)-----(H)-------(a)------(G)-----(F)
     | left  |                  | right | 
     |       |       floor      |       |
     |  wall |                  | wall  |
    (P)-----(S)----------------(R)-----(Q)
             |     front wall   |
             |                  |
            (P)-------(b)------(Q)
             |                  |
             |      ceiling     |
             |                  |
            (E)----------------(F)

    Path 1: S-a-F is the hypotenuse of triangle S-E-F, where the spider first 
    travels across the floor, then up the back wall.

    Path 2: S-b-F is the hypotenuse of triangle S-R-F, where the spider first
    travels up the front wall, then across the ceiling.

    Paths 1 and 2 are really the same triangle, just "rotated" differently.
    
                               (E)----------------(F)
                                |     back wall    |
                                |                  |
    (F)----------------(E)-----(H)-------(A)------(G)-----(F)
     |                  | left  |                  | right |
     |      ceiling    (c)      |       floor     (d)      |
     |                  |  wall |                  | wall  |
    (Q)----------------(P)-----(S)----------------(R)-----(Q)
                                |     front wall   |
                                |                  |
                               (P)----------------(Q)
             

    Path 3: S-c-F is the hypotenuse of triangle S-H-F, where the spider first 
    travels up the left wall, then across the ceiling.
    
    Path 4: S-d-F is the hypotenuse of triangle S-Q-F, where the spider first 
    travels across the floor, then up the right wall.

    Paths 3 and 4 are really the same triangle, just "rotated" differently.

                                (H)-------(a)------(G)
                                 |                  | 
                                 |       floor      |
                                 |                  |
    (G)----------------(H)------(S)----------------(R)------(G)
     |     back wall   (j) left  |     front wall   |  right |
     |                  |  wall  |                 (i)  wall |
    (F)----------------(E)------(P)----------------(Q)------(F)
                                 |                  |
                                 |      ceiling     |
                                 |                  |
                                (E)----------------(F)
 
    Path 5: S-i-F is the hypotenuse of triangle S-G-F, where the spider first 
    travels up the left wall, then across the back wall.

    Path 6: S-j-F is the hypotenuse of triangle S-P-F, where the spider first 
    travels across the front wall, then up the right wall.

    Paths 5 and 6 are really the same triangle, just "rotated" differently.
    
        #   #   #

    With the "unfolding" of the room it was really easy to see that the 
    "shortest" paths are always hypotenuses (side Z) formed from right 
    triangles with side X being one of the room dimensions, and side Y being 
    the addition of the other two. 
    
    For example, in the illustration on the problem page, you have a room of 
    width 6 (S to R), height 3 (G to F), and depth 5 (R to G). That same 
    illustration shows the spider traveling my path 1 above, which is the 
    hypotenuse of triangle S-E-F. Or, a right triangle with X as the room's 
    width and Y as its height plus depth.

    So now, in our code below, we have m, which is our maximum of the three
    dimensions. The number of integer solutions in a room with its largest 
    dimension of m is the number of integer solutions with X = m plus the 
    number with X = m - 1, plus the number of X = m - 2... and so on until X is
    equal to 1.

    To do that, we take our X value and create all the triangles where the 
    other two dimensions are less than or equal to X and we add them together
    to create Y. Only we can take a short cut. We don't actually have to test
    all the possible width values, cross joined to all possible height, and 
    again cross joined to all possible depth values. That would take a lot of 
    time. Instead, we can first test all possible X values cross joined with
    all integers between 2 and 2X. 
    
    Basically, it doesn't matter which numbers represent width, height, and 
    depth. The problem specifically says "ignoring rotations". So a room of 
    width 6, height 3, and depth 5 is the same room as one of width 3, height 
    5, depth 6 (according to this problem). So that allows us to take our X 
    value and use it in conjunction with just one other number representing the 
    sum of the other two dimensions. Doesn't matter which two dimensions. Just 
    take X and pair it with all the other numbers from 2 to 2X and quickly test 
    that for whether its hypotenuse is a whole number.

    Once you have that, you know that a room with one dimension as X and the 
    other 2 dimensions sum up to Y will yield a whole number "shortest path". 
    But now you need to know all the ways that 2 whole numbers can sum to Y to
    give you your actual total. 

    In the illustrated example on the problem page, you have an X of 6 and a Y
    of 8 (height 3 + depth 5). But that just tells you that X of 6 and Y of 8
    yields an integer hypotenuse. 3 and 5 aren't the only way to do this. A 
    room of width 6, height 4, and depth 4 would have given you the same X = 6,
    Y = 8.

    So that gives you the following:

        1, 7
        2, 6
        3, 5
        4, 4

    But we can't really use 1, 7 because the 7 is larger than our X value of 6.
    And, since we have to ignore rotations, we want to keep our X as the 
    largest dimension to avoid duplications. So that means our total 
    combinations of Y = 8 are the number of ways to choose other dimension 1
    and other dimension 2 satisfying all of these conditions:

        other dimension 1 + other dimension 2 = Y
        other dimension 1 >= 1
        other dimension 2 >= other dimension 1
        other dimension 2 <= X

    The "easy" way to determine those combinations is with this code that I 
    stole from user Javamannen in his post on Dec 16, 2004:

        if y > x + 1 then (x + x + 2 - y) / 2
        else y / 2
 
    *)

    let isInt f = abs (f % 1.0) <= (System.Double.Epsilon * 100.0)
    let isShortestRouteAnInt x y = 
        sqrt ((float)x**2.0 + (float)y**2.0)
        |> isInt
    let rec howManyAtM m target currentChain =
        if Array.sum currentChain >= target then m - 1
        else
            let x = m
            let y_min = 2
            let y_max = x + x
            [|y_min..y_max|]
            |> Array.map (fun y -> (x, y, isShortestRouteAnInt x y))
            |> Array.filter (fun (x, y, isInt) -> isInt = true)
            |> Array.map (fun (x, y, isInt) -> 
                if y > x + 1 then (x + x + 2 - y) / 2
                else y / 2
                )
            |> (fun newAddition -> Array.concat [|currentChain; newAddition|])
            |> howManyAtM (m + 1) target

    let target = (int)1e6
    let initialM = 1
    let initialChain = [||]
    howManyAtM initialM target initialChain
    |> intToString

module Euler031

let run () = 

    (*
    This is a nice and compact way of putting this business logic into code. 
    Basically, set up a recursive function that "spends" each of the 
    individual coins and then counts all the ways to spend the remainder. The
    tricky part is in getting rid of duplicates. 

    For example, if I have 7 left, I could spend a 5-piece coin, get a 
    remainder of 2, and then spend a 2-piece coin. But I could also start with
    7 and spend a 2-piece coin and have a remainder of 5, which I fill by 
    spending the 5-piece coin. Either way, I've spent a 5-piece coin and a 
    2-piece coin, but the algorithm will count that twice. 

    That's why my first List.map also includes a List.filter to only include
    coins that are less than or equal to the one we're first spending. This
    logic would eliminate the 2-then-5 duplicate when we've already spent 
    5-then-2.
    *)

    let target = 200
    let allCoins = [200; 100; 50; 20; 10; 5; 2; 1]

    let rec howManyWays targetSum coins = 
        if targetSum = 0 then 1
        else
            coins 
            |> List.map (fun x -> (targetSum - x, coins |> List.filter (fun y -> y <= x))) 
            |> List.filter (fun (remainder, coinsLeft) -> remainder >= 0) 
            |> List.map (fun (remainder, coinsLeft) -> howManyWays remainder coinsLeft)
            |> List.sum
            

    let answer = howManyWays target allCoins
    answer.ToString()
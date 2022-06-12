module Test

let run () =
    let squareRoot n = sqrt (float n)

    let square n = n * n

    let floorSqrtSquared = squareRoot >> floor >> square

    let isPerfectSquare n =
        let x = floorSqrtSquared n
        if x = n then true else false
    

    let burp = isPerfectSquare 100
    printfn "%b" burp
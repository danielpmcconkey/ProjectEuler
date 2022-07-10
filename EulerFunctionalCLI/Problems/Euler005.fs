module Euler005


let run () =

    
    let denominators = { 11 .. 19 } |> Seq.rev
    let numerators = { 20 .. 20 .. 300000000 }

    let canMod n m = 
        if n % m = 0 then true else false

    let isASolution n =         
        denominators
        |> Seq.forall (canMod n)
        

    let minSolution = 
        numerators
        |> Seq.find isASolution
        
    minSolution.ToString()
    
  

module Euler025

let run () = 

    (*
    see https://www.mathblog.dk/project-euler-25-fibonacci-sequence-1000-digits/
    so, the fibonacci sequence trends toward F(n) = ((phi ^ n)/squareRootOf5) 
    where phi is the golden ratio ((1 + sqrt 5) / 2). 
   
    so, assuming the first Fibonacci number with 1000 digits is large enough to
    converge, then we need an inequality set up as such:
    
                   (phi ^ n)
                 -------------         >        (10 ^ 999)
                 squareRootOf5 
         
    solve the inequality and you'll get n > ~answer. find the first whole number 
    that is greater than your approximate answer and you win the nerd games.
    
    so how do we solve that? we have to remember high school, or google high
    school, and apply the fact that logs cancel out exponents. Take the log
    on both sides.
    
           /               \                   /              \
           |   (phi ^ n)    |                  |              |
    log of | -------------  |     >     log of |  (10 ^ 999)  |
           | squareRootOf5  |                  |              |
           \               /                   \              /
    
    Apply the logarithms rule that states log_b(a^c) = c * log_b(a)^1
    
    n * log(phi) - ((1/2) * log(5))      >       999 * log(10)
    
    
    now we want to isolate n start by adding ((1/2) * log(5)) to both sides
    
    n * log(phi)      >       999 * log(10) + ((1/2) * log(5))
    
    now divide by log(phi)
    
                            999 * log(10) + ((1/2) * log(5))
             n       >     ---------------------------------- 
                                        log(phi)
    *)
    let floatToInt f = (int)f 
    let toString n = n.ToString()
    let add1 f = f + 1F
    let div2 f = f / 2F
    
    let phi = sqrt 5F |> add1 |> div2
    let logOf10 = 10F |> log10
    let logOf5 = 5F |> log10
    let logOfPhi = log10 phi

    ((logOf10 * 999F) + (logOf5 * 0.5F)) / logOfPhi
    |> ceil 
    |> floatToInt
    |> toString


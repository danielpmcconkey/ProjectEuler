module Algorithms

//let prefix prefixStr baseStr = 
//    prefixStr + ", " + baseStr

//let prefixWithHello = prefix "Hello"

//let exclaim s =
//    s + "!"

//let bigHello = prefixWithHello << exclaim

//let lineBreak s =
//    s + System.Environment.NewLine

let primesUpToN n = 

    // this uses the sieve of Sandaram
    // https://kalkicode.com/sieve-of-sundaram

    if(n <= 1)
        then Array.create        
    else
        //Calculate the number of  prime of given n
        let limit = ((n - 2) / 2) + 1
        let sieve = Array.zeroCreate(limit)
        Array.create // just put this here so the solution coud compile while I worked on this function
        
        //    for (i = 1; i < limit; ++i)
        //    {
        //        for (j = i;
        //            (i + j + 2 * i * j) < limit; ++j)
        //        {
        //            //(i + j + 2ij) are unset
        //            sieve[(i + j + 2 * i * j)] = 1;
        //        }
        //    }
        //    // 2 needs to be added manually
        //    if (n >= 2)
        //    {
        //        primes.Add(2);
        //    }
        //    //Display prime element
        //    for (i = 1; i < limit; ++i)
        //    {
        //        if (sieve[i] == 0)
        //        {
        //            if (((i * 2) + 1) > int.MaxValue)
        //            {
        //                throw new OverflowException("too big to downscale to an int");
        //            }
        //            primes.Add((int)((i * 2) + 1));
        //        }
        //    }
        //    return primes.ToArray();
        //}
    
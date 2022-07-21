module Euler019
open System


// create a special type of timespan that has a static + and static zero member to use in the skip of a range
type Span = Span of TimeSpan with
  static member (+) (d:DateTime, Span wrapper) = d + wrapper
  static member Zero = Span(new TimeSpan(0L))

let run () =

    let isSundayTheFirst (date:DateTime) =
        if date.Day = 1 && date.DayOfWeek = DayOfWeek.Sunday then true else false

    let toString n = n.ToString()

    let skip = TimeSpan.FromDays(1.0)
    let startDate = DateTime.Parse("Jan 1, 1901");  // inclusive
    let endDate = DateTime.Parse("Jan 1, 2001");    // exclusive
    
    [startDate .. Span(skip) .. endDate]
    |> Seq.filter(fun x -> isSundayTheFirst x)
    |> Seq.length
    |> toString




module CFSharp.F.Misc

let mutable counter = 0

let IncCounter() = 
    counter <- counter + 1
    printfn "%d" counter

open NUnit.Framework
open Shouldly

[<Test>]
let ``Look ma! Side-effects!``() =
    IncCounter()
    IncCounter()
    (* prints:
        1
        2       *)





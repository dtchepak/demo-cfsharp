module CFSharp.F.Misc

let mutable counter = 0

let IncCounter() = counter <- counter + 1

open NUnit.Framework
open Shouldly

[<Test>]
let ``Look ma! Side-effects!``() =
    printfn "%d" counter
    IncCounter()
    IncCounter()
    printfn "%d" counter

    (* prints:
        0
        2
     *)





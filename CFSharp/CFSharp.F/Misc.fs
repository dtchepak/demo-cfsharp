module CFSharp.F.Misc

open NUnit.Framework
open FsUnit

let mutable counter = 0

let IncCounter() = 
    counter <- counter + 1
    printfn "%d" counter

[<Test>]
let ``Look ma! Side-effects!``() =
    IncCounter()
    IncCounter()
    (* prints:
        1
        2       *)


let add a b = a + b

[<Test>]
let testAdd() = 
    add 1 2 |> should equal 3

let add2(a, b) = a + b

[<Test>]
let testAdd2() = 
    add2(1, 2) |> should equal 3


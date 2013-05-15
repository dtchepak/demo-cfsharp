﻿module CFSharp.F.Misc

open NUnit.Framework
open FsUnit
open System.Linq

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

[<Test>]
let useLambda() =
    let list' = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
    let list = [1..10]

    let result = list.Where(fun x -> x % 2 = 0)

    printfn "%A" result

let describe language =
    let description = 
        if language = "C#" then 
            "nice"
        else if language = "F#" then 
            "awesome"
        else if language = "Haskell" then 
            "begone, zealot!"
        else "I don't know that language"
    language + ": " + description

[<Test>]
let testDescribe() =
    describe "Haskell" |> should equal "Haskell: begone, zealot!"

[<Test>]
let loops() =
    let array = [|1; 2; 3; |]
    for x in array do
        // ...
        printfn "%A" x
    for x = 0 to array.Length-1 do
        // ...
        printfn "%d" array.[x]
    let mutable counter = 0
    while counter < 3 do
        // ...
        printfn "stuff!"
        counter <- counter+1
        

(* conditionals 
   loops
   assignments *)

(* sensible defaults:
        immutable
        no nulls
        *)

(* extra features:
        light-weight type alternatives
        pattern-matching
        ignored return (via ignore)
        *)

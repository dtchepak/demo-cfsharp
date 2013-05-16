module CFSharp.F.Misc

open NUnit.Framework
open FsUnit
open System.Linq
open FSharpx.Strings

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

let topFsharpComments comments = 
    comments 
        |> Seq.filter (contains "F#") 
        |> Seq.truncate 5
        |> String.concat "\n"

[<Test>]
let testTopComments() =
    let result = topFsharpComments ([1..10] |> Seq.map (sprintf "blah blah F# %d"))
    printfn "%s" result

open System.IO

let foo fileName = 
    try
        use f = File.CreateText fileName
        f.WriteLine "hi!"
    with
        ex -> printfn "something went wrong: %A" ex

let commentsFor lang = [| "blah blah " + lang + " blah"; "something " + lang; "foo blah " + lang; |]
let commentsForLanguages languages =
    seq {
        for lang in languages do
        for comments in commentsFor lang do
        if contains "blah" comments then
            yield lang + ": " + comments
    }

[<Test>]
let testCommentsForLanguages() =
    let langs = [| "C#"; "F#" |]
    let result = String.concat "\n" (commentsForLanguages langs)
    printfn "%s" result
        
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

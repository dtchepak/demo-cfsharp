module CFSharp.F.LightWeightTypes

open NUnit.Framework
open System
open FsUnit

type TrafficLight =
    | Red
    | Yellow
    | Green

let trafficInstruction light =
    match light with
        | Red -> "STOP!"
        | Yellow -> "can we sneak through?"
        //  warning FS0025: Incomplete pattern matches on this expression. 
        //  For example, the value 'Green' may indicate a case not covered by the pattern(s).

type ParseResult<'a> = 
    | Parsed of 'a 
    | Failed of string
    member this.parsedOk =
        match this with 
        | Parsed _ -> true
        | _        -> false

let parseInt' s =
    let (parsed, result) = Int32.TryParse s
    if parsed then Some result else None

let parseInt s =
    match Int32.TryParse s with
        | (false, _)     -> Failed ("could not parse " + s)
        | (true, result) -> Parsed result

let parsedOk p =
    match p with
    | Parsed _ -> true
    | _        -> false

let shouldFailWith s p =
    match p with
    | Failed s' -> s' |> should equal s
    | _ -> Assert.Fail("expected Failed but got Parsed")

[<Test>]
let testParseInt() =
    parseInt "123" |> should equal (Parsed 123)
    parseInt "bob" |> shouldFailWith "could not parse bob"
    Assert.AreEqual(Failed "could not parse bob", parseInt "bob")

[<Test>]
let testParsedOk() =
    (parseInt "123").parsedOk |> should equal true
    (parseInt "bob").parsedOk |> should equal false


type String with
    member this.parseInt() =
        parseInt this

[<Test>]
let testParseOnString() =
    "123".parseInt() |> should equal (Parsed 123)

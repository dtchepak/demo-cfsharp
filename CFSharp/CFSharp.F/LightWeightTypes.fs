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
    member this.parsed =
        match this with 
            | Parsed _ -> true
            | _        -> false

let parseInt' s =
    let (parsed, result) = Int32.TryParse s
    if parsed then Some result else None

let parseInt s =
    match Int32.TryParse s with
        | (false, _)     -> Failed ("could not parse int from " + s)
        | (true, result) -> Parsed result

[<Test>]
let testParse() =
    parseInt "123" |> should equal (Parsed 123)
    (parseInt "bob").parsed |> should equal false

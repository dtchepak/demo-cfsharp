﻿module CFSharp.F.MonadPolice2Tests

open NUnit.Framework
open FsUnit
open TestHelper
open MonadPolice2

let ramblings =
    [| "tea?"
    ; "couldn't we use a monad for that?"
    ; "anyone for tea?"
    ; "blah blah blah haskell blah blah monad blah blah"
    ; "a cup of tea is a lot like the Maybe monad..." 
    |]

[<Test>]
let ``create tip off for each outburst of zealotry`` () =
    surveil ramblings 
        |> Seq.length 
        |> should equal 3

[<Test>]
let ``all tip offs should go to xerx`` () =
    surveil ramblings
        |> Seq.map (fun x -> x.sendTo)
        |> Seq.forall (fun x -> x = "xerxesb")
        |> should equal true

let private pickRamblings = Seq.map (fun i -> ramblings.[i]) >> Seq.toArray

[<Test>]
let ``message of each tip of should contain original outburst`` () =
    surveil ramblings
        |> Seq.map (fun x -> x.msg)
        |> Seq.toArray
        |> shouldBe (pickRamblings [1; 3; 4;])

// ---------------------------------------
open FsCheck
open FSharpx.Strings

let propSendToXerx (ramblings:string list) =
    printf "%s" (String.concat "," ramblings)
    let tipoffs = surveil ramblings
    tipoffs |> Seq.forall (fun x -> x.sendTo = "1xerxesb")

[<Test>]
[<Ignore>]
let checkProps() = Check.Quick propSendToXerx


// ---------------------------------------

open MonadPolice

[<Test>]
let ``test sample`` () =
    let ramblings =
        [ "tea?"
        ; "couldn't we use a monad for that?"
        ; "anyone for tea?"
        ; "blah blah blah haskell blah blah monad blah blah"
        ; "a cup of tea is a lot like the Maybe monad..." 
        ]
    let dave = { new ImDave with
                    member x.RecentRamblings() = Seq.ofList ramblings }
    let emailer = { new IEmailGateway with 
                            member x.Send _ _ subj msg = printfn "%s: %s" subj msg }

    sample dave emailer
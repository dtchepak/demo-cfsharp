module CFSharp.F.TestHelper

open System
open NSubstitute
open NSubstitute.Experimental
open Shouldly

let Returns<'a> (result:'a) (call:'a) =
    NSubstitute.SubstituteExtensions.Returns<'a>(call, result) |> ignore

let Received<'a when 'a : not struct> (sub:'a) =
    NSubstitute.SubstituteExtensions.Received(sub)

let wasReceived (call:unit ->unit) : unit =
    let action f = Action(f)
    NSubstitute.Experimental.Received.InOrder (action call)

let Any<'a>() = Arg.Any<'a>()

let ReceivedWithAnyArgs<'a when 'a : not struct> times (sub:'a) =
    NSubstitute.SubstituteExtensions.ReceivedWithAnyArgs(sub, times) 

let inline shouldBe expected actual = ShouldBeTestExtensions.ShouldBe(actual, expected)

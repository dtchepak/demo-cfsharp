module CFSharp.F.MonadPoliceTests

open NUnit.Framework
open NSubstitute
open FsUnit
open MonadPolice
open TestHelper

type MonadPoliceTests() =
    [<DefaultValue>] val mutable _emailer : IEmailGateway
    [<DefaultValue>] val mutable _dave : ImDave
    [<DefaultValue>] val mutable _subject : MonadPolice

    [<SetUp>]
    member this.SetUp() =
        this._emailer <- Substitute.For<IEmailGateway>()
        this._dave <- Substitute.For<ImDave>()
        this._subject <- MonadPolice(this._dave, this._emailer)

    [<Test>]
    member this.ShouldNotifyXerxWithEveryMBomb() =
        let ramblings =
            [ "tea?"
            ; "couldn't we use a monad for that?"
            ; "anyone for tea?"
            ; "blah blah blah haskell blah blah monad blah blah"
            ; "a cup of tea is a lot like the Maybe monad..." 
            ]
        this._dave.RecentRamblings() |> Returns (Seq.ofList ramblings)

        this._subject.Surveil()

        (this._emailer |> Received).Send "xerxesb" (Any<string>()) (Any<string>()) ramblings.[1]
        (this._emailer |> Received).Send "xerxesb" (Any<string>()) (Any<string>()) ramblings.[3]
        (this._emailer |> Received).Send "xerxesb" (Any<string>()) (Any<string>()) ramblings.[4]
        (this._emailer |> ReceivedWithAnyArgs 3).Send (Any<string>()) (Any<string>()) (Any<string>()) (Any<string>())

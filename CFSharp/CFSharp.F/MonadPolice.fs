module CFSharp.F.MonadPolice

open System.Linq
open FSharpx.Strings

type IEmailGateway =
    abstract Send : string -> string -> string -> string -> unit

type ImDave =
    abstract RecentRamblings : unit -> string seq // or Seq<string>
    
type MonadPolice(dave : ImDave, email : IEmailGateway) = 
    //let _dave = dave
    //let _email = email
    member this.Surveil() = 
        let overheard = dave.RecentRamblings()
        let zealotTalk = 
          overheard.Where(contains "monad")
        for outburstOfZealotry in zealotTalk do
            email.Send "xerxesb" "the monad police" 
                        "Lack of pragmatism detected" outburstOfZealotry

type MonadPolice_MoreFSharpish(dave : ImDave, email : IEmailGateway) = 
    let sendOutburst = email.Send "xerxesb" "the monad police" "Lack of pragmatism detected"
    member this.Surveil() = 
        dave.RecentRamblings()
            |> Seq.filter (contains "monad")
            |> Seq.iter sendOutburst
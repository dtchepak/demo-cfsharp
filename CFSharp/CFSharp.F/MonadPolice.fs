module CFSharp.F.MonadPolice

open System.Linq
open FSharpx.Strings

type IEmailGateway =
    abstract Send : string -> string -> string -> string -> unit

type ImDave =
    abstract RecentRamblings : unit -> string seq // or Seq<string>
    
type MonadPolice(dave : ImDave, email : IEmailGateway) = 
    let _dave = dave
    let _email = email
    member this.Surveil() = 
        let overheard = _dave.RecentRamblings()
        let zealotTalk = overheard.Where(fun (x:string) -> x.Contains("monad"))
        for outburstOfZealotry in zealotTalk do
            _email.Send "xerxesb" "the monad police" "Lack of pragmatism detected" outburstOfZealotry

type MonadPolice_MoreFSharpish(dave : ImDave, email : IEmailGateway) = 
    let _dave = dave
    let _email = email
    let sendOutburst = _email.Send "xerxesb" "the monad police" "Lack of pragmatism detected"
    member this.Surveil() = 
        _dave.RecentRamblings()
            |> Seq.filter (contains "monad")
            |> Seq.iter sendOutburst
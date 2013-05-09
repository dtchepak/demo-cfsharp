﻿module CFSharp.F.MonadPolice

open System.Linq

type IEmailGateway =
    abstract member Send : string -> string -> string -> string -> unit

type ImDave =
    abstract member RecentRamblings : unit -> string seq // or Seq<string>
    
type MonadPolice(dave : ImDave, email : IEmailGateway) = 
    let _dave = dave
    let _email = email
    member this.Surveil() = 
        let overheard = _dave.RecentRamblings()
        let zealotTalk = overheard.Where(fun (x:string) -> x.Contains("monad"))
        for outburstOfZealotry in zealotTalk do
            _email.Send "xerxesb" "the monad police" "Lack of pragmatism detected" outburstOfZealotry
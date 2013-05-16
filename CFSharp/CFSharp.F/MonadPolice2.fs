module CFSharp.F.MonadPolice2

open FSharpx.Strings
open MonadPolice

type TipOff =
    { sendTo : string
    ; from : string
    ; subject : string
    ; msg : string
    }

let surveil (talk : string seq) =
    let isZealotry = contains "monad"
    let standardTipOff = { sendTo="xerxesb"; from="the monad police"; 
                           subject="Lack of pragmatism detected"; msg="" }
    talk
        |> Seq.filter isZealotry
        |> Seq.map (fun s -> { standardTipOff with msg=s })

let sample (dave : ImDave) (email : IEmailGateway) =
    dave.RecentRamblings() 
        |> surveil
        |> Seq.iter (fun x -> email.Send x.sendTo x.from x.subject x.msg)
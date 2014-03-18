namespace BingoEngine

module BallCaller =
    open Util
    open Bingo

    let callBalls() =
        Shuffle [|1..75|] 
        |> List.ofArray 
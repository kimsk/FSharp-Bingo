#load "Util.fs"
#load "Bingo.fs"
#load "BingoCard.fs"
#load "BallCaller.fs"
#load "PatternMatcher.fs"

open BingoEngine

let card = BingoCard.createNewCard()

BingoCard.toStr card |> printfn "%s"

let balls = BallCaller.callBalls() |> Seq.take 50

balls
|> Seq.fold PatternMatcher.markBall card
|> BingoCard.toStr
|> (printfn "%s")


let card' = balls |> Seq.fold PatternMatcher.markBall card

(BingoCard.toStr card') |> printfn "%s" 

let cards = 
    [
        BingoPatterns.horizontalLines0
        BingoPatterns.horizontalLines1
        BingoPatterns.horizontalLines2
        BingoPatterns.horizontalLines3
        BingoPatterns.horizontalLines4
    ] 
    |> List.map (PatternMatcher.matchPattern card')
    |> List.iter (fun card -> 
                    match card with
                    | Some c -> BingoCard.toStr c |> printfn "%s"
                    | None -> ()
                )
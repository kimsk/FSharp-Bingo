#load "Util.fs"
#load "Bingo.fs"
#load "BingoCard.fs"
#load "BallCaller.fs"
#load "BingoPatterns.fs"
#load "PatternMatcher.fs"

open BingoEngine

let card = BingoCard.createNewCard()

BingoCard.toStr card |> printfn "%s"

let balls = BallCaller.callBalls() |> Seq.take 50

let card' = balls |> Seq.fold PatternMatcher.markBall card

BingoCard.toStr card' |> printfn "%s" 

let cards = 
    BingoPatterns.Patterns
    |> List.map (fun p -> p.Name, p.Pattern)
    |> List.map (fun (name, pattern) -> name, (PatternMatcher.matchPattern card' pattern))
    |> List.iter (fun (name, card) -> 
                    match card with
                    | Some c -> 
                        printfn "%s" name
                        BingoCard.toStr c |> printfn "%s"
                    | None -> ()
                )

BingoPatterns.Patterns
|> List.map BingoPatterns.toStr
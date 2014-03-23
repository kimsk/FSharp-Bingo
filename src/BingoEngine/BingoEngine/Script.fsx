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

let card' = balls |> Seq.fold (fun card b -> PatternMatcher.markBall b card) card

BingoCard.toStr card' |> printfn "%s" 

// pattern matching
BingoPatterns.Patterns
    |> List.map (fun p -> p.Name, p.Pattern)
    |> List.map (fun (name, pattern) -> name, (PatternMatcher.matchPattern pattern card' ))
    |> List.iter (fun (name, card) ->
                    match card with
                    | BingoCard.Matched _ ->  
                        printfn "%s" name
                        BingoCard.toStr card |> printfn "%s"                    
                    | _ -> ()
                )

// print all bingo patterns
BingoPatterns.Patterns
|> List.map BingoPatterns.toStr

// Use function composition (>>)
let markFirstBall = PatternMatcher.markBall 1
let matchFirstPattern = PatternMatcher.matchPattern (BingoPatterns.Patterns.Head.Pattern)
let markCard' = markFirstBall >> matchFirstPattern

markCard' card
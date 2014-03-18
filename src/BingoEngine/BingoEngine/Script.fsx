#load "Util.fs"
#load "Bingo.fs"
#load "BingoCard.fs"
#load "BallCaller.fs"
#load "PatternMatcher.fs"

open BingoEngine

let card = BingoCard.createNewCard()

let cardStr = BingoCard.printNewCard card

printfn "%s" cardStr

let balls = BallCaller.callBalls() |> Seq.take 20

balls
|> Seq.fold PatternMatcher.markBall card
|> BingoCard.printNewCard
|> (printfn "%s")
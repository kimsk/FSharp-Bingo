#load "Util.fs"
#load "Bingo.fs"
#load "BingoCard.fs"
#load "BallCaller.fs"
#load "BingoPatterns.fs"
#load "PatternMatcher.fs"

open BingoEngine
open System.Linq

let patterns = 
    [
        let elgiblePatterns = 
            [
                "horizontal-1"
                "horizontal-2"
                "horizontal-3"
                "horizontal-4"
                "horizontal-5"
                "vertical-1" 
                "vertical-2" 
                "vertical-3" 
                "vertical-4"
                "vertical-5"
                "Diagonal-1"
                "Diagonal-2"
            ] |> Seq.ofList

        for p in BingoPatterns.Patterns do
            if elgiblePatterns.Any(fun ep -> ep = p.Name) then
                yield p
    ] 

let rec callBall (cards:BingoCard.Card list) (balls:int list) =    
    match balls with
    | [] -> "No Winner"
    | ball::tl ->
        printfn "Call Ball : %d" ball
        let cards' = cards |> List.map (fun c -> PatternMatcher.markBall c ball)
        let cards'' = cards' 
                    |> List.collect (fun c -> 
                                        [
                                            for p in patterns do
                                                let inPatternCard = (PatternMatcher.matchPattern c p.Pattern)
                                                if inPatternCard.IsSome then
                                                    yield (p.Name, inPatternCard.Value)
                                        ]
                                )        
        
        match cards'' with
        | [] -> callBall cards' tl
        | list ->
            list |> List.iter (fun c -> printfn "%s" (fst(c)))
         
            list             
            |> List.fold (fun acc c -> acc + "\r\n" + fst(c) + "\r\n" + BingoCard.toStr(snd(c))+"\r\n") ""

let cards = [1..10] |> List.map (fun _ -> BingoCard.createNewCard())

for c in cards do
    c |> BingoCard.toStr |> printfn "%s"
               
let balls = BallCaller.callBalls() |> Seq.take 30 |> List.ofSeq
let winner = callBall cards balls           

        

    

             
        
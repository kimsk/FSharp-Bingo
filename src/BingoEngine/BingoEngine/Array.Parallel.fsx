#r @"bin\debug\BingoEngine.dll"

open BingoEngine

let patterns = 
    [
        let eligiblePatterns = 
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
            ]

        for p in BingoPatterns.Patterns do
            if eligiblePatterns |> Seq.exists ((=)(p.Name)) then
                yield p
    ] 



// Change list to array to utilize Array.Parallel

#time
let cards = [|1..100000|] |> Array.map (fun _ -> BingoCard.createNewCard())
#time

let balls = BallCaller.callBalls() |> Seq.take 30 |> List.ofSeq

#time
cards |> Array.map (fun c -> PatternMatcher.markBall 1 c)     
#time

let rec callBall (cards:BingoCard.Card[]) (balls:int list) (isParallel) =   
    let arrMap = if isParallel then Array.Parallel.map else Array.map
    let arrCollect =  if isParallel then Array.Parallel.collect else Array.collect
    let arrIter = if isParallel then Array.Parallel.iter else Array.iter
 
    match balls with
    | [] -> printfn "No Winner"
    | ball::tl ->
        printfn "Call Ball : %d" ball
        let cards' = cards |> arrMap (fun c -> PatternMatcher.markBall ball c)
        let cards'' = cards' 
                    |> arrCollect (fun c -> 
                                        [|
                                            for p in patterns do
                                                let c = (PatternMatcher.matchPattern p.Pattern c)
                                                let ismatchedCard = 
                                                    match c with
                                                    | BingoCard.Matched _ -> true
                                                    | _ -> false

                                                if ismatchedCard then
                                                    yield (p.Name, c)
                                        |]
                                )        
        
        match cards'' with
        | [||] -> callBall cards' tl isParallel
        | arr ->
            arr |> arrIter (fun c -> printfn "%s" (fst(c)))

#time
false |> callBall cards balls |> ignore
#time

#time
true |> callBall cards balls |> ignore
#time
#load "Util.fs"
#load "Bingo.fs"
#load "BingoCard.fs"
#load "BallCaller.fs"
#load "BingoPatterns.fs"
#load "PatternMatcher.fs"

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

let rec callBall (cards:BingoCard.Card list) (balls:int list) =    
    match balls with
    | [] -> "No Winner"
    | ball::tl ->
        printfn "Call Ball : %d" ball
        let cards' = cards |> List.map (fun c -> PatternMatcher.markBall ball c)
        let cards'' = cards' 
                    |> List.collect (fun c -> 
                                        [
                                            for p in patterns do
                                                let c = (PatternMatcher.matchPattern p.Pattern c)
                                                let ismatchedCard = 
                                                    match c with
                                                    | BingoCard.Matched _ -> true
                                                    | _ -> false

                                                if ismatchedCard then
                                                    yield (p.Name, c)
                                        ]
                                )        
        
        match cards'' with
        | [] -> callBall cards' tl
        | list ->
            list |> List.iter (fun c -> printfn "%s" (fst(c)))
         
            list             
            |> List.fold (fun acc c -> acc + "\r\n" + fst(c) + "\r\n" + BingoCard.toStr(snd(c))+"\r\n") ""

let markBallAsync card ball =
    async {
        return PatternMatcher.markBall ball card
    }

let matchPatternAsync card pattern =
    async {
        return PatternMatcher.matchPattern pattern card
    }

let rec callBallParallel (cards:BingoCard.Card list) (balls:int list) =    
    match balls with
    | [] -> "No Winner"
    | ball::tl ->
        printfn "Call Ball : %d" ball
        let cards' = [ for c in cards -> markBallAsync c ball] 
                        |> Async.Parallel 
                        |> Async.RunSynchronously 
                        |> List.ofArray
        let cards'' = 
            let list = 
                [
                    for c in cards' ->
                        [ for p in patterns -> matchPatternAsync c p.Pattern]
                        |> Async.Parallel
                        |> Async.RunSynchronously
                        |> Array.filter BingoCard.isMatchedCard                        
                        |> List.ofArray
                ]
            list |> List.reduce (fun acc l -> l@acc) 
        
        match cards'' with
        | [] -> callBall cards' tl
        | list ->            
            list             
            |> List.fold (fun acc c -> acc + "\r\n" + BingoCard.toStr(c)+"\r\n") ""


let cards = [1..1000000] |> List.map (fun _ -> BingoCard.createNewCard())

for c in cards do
    c |> BingoCard.toStr |> printfn "%s"
               
let balls = BallCaller.callBalls() |> Seq.take 30 |> List.ofSeq

#time
let winner = callBall cards balls           
#time

#time
let winner' = callBallParallel cards balls           
#time

// sequential
#time
cards |> List.map (fun c -> PatternMatcher.markBall 1 c) 
    //|> ignore
#time

// parallel 1
// too many async, this can be worse than sequential
#time
[ for c in cards -> markBallAsync c 1] 
    |> Async.Parallel 
    |> Async.RunSynchronously 
    |> ignore
#time 

// parallel 2
// Small number of async gives best result
let groupCards cards groupNum =
    let numPerGroup = (cards |> Seq.length)/groupNum
    printfn "%i %i %i" (cards |> Seq.length) groupNum numPerGroup    
    [0..groupNum-1] 
        |> List.fold (fun acc gr -> (cards |> Seq.skip (gr*numPerGroup) |> Seq.take numPerGroup |> List.ofSeq)::acc) []
let markBallOnCardsAsync cards ball =
    async {
        return (cards |> List.map (fun c -> PatternMatcher.markBall ball c)) 
    }

#time
let groups = groupCards cards 8
#time
#time
[ for cards in groups -> markBallOnCardsAsync cards 1 ] 
    |> Async.Parallel 
    |> Async.RunSynchronously 
    //|> ignore   
#time

// www.voyce.com/index.php/2011/05/27/fsharp-async-plays-well-with-others/
let work i =
    async {
        do! Async.Sleep(500)
        return i + 1
    }

let run _ =
    [1..30000] |> List.map work
    |> Async.Parallel
    |> Async.RunSynchronously

#time
run 1
#time

// http://fsharpforfunandprofit.com/posts/concurrency-async-and-parallel/
let childTask() = 
    // chew up some CPU. 
    for i in [1..1000] do 
        for i in [1..1000] do 
            do "Hello".Contains("H") |> ignore 
            // we don't care about the answer!

// Test the child task on its own.
// Adjust the upper bounds as needed
// to make this run in about 0.2 sec
#time
childTask()
#time

let parentTask = 
    childTask
    |> List.replicate 20
    |> List.reduce (>>)

//test
#time
parentTask()
#time

let asyncChildTask = async { return childTask() }

let asyncParentTask = 
    asyncChildTask
    |> List.replicate 20
    |> Async.Parallel

//test
#time
asyncParentTask 
|> Async.RunSynchronously
#time
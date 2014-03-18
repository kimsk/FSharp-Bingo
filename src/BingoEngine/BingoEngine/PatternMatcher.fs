namespace BingoEngine

module BingoPatterns =
    let horizontalLines = 
        [(0,0);(0,1);(0,1);(0,3);(0,4)]

module PatternMatcher =
    open BingoCard
    
    let markBall (card:Cell[][]) ball =
        let markNumber cell =
            match cell with
            | NotCalled num ->
                if ball = num then BingoCard.Called num
                else BingoCard.NotCalled num
            | _ -> cell
        
        [|
            card.[0] |> Array.map markNumber
            card.[1] |> Array.map markNumber
            card.[2] |> Array.map markNumber
            card.[3] |> Array.map markNumber
            card.[4] |> Array.map markNumber
        |]
     

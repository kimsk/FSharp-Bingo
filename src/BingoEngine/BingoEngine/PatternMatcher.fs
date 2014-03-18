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
        
        let i = Bingo.getCol ball

        match i with
        | Some i -> 
            [|
                for row in 0..4 ->
                [|
                    for col in 0..4 ->
                        if col = i then
                            card.[row].[i] |> markNumber
                        else card.[row].[col]                                         
                |]
            |]
        | None -> card        
     

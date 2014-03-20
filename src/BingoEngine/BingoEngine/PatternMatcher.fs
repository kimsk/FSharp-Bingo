namespace BingoEngine

module BingoPatterns =
    let horizontalLines0 = [(0,0);(0,1);(0,2);(0,3);(0,4)] |> Set.ofList
    let horizontalLines1 = [(1,0);(1,1);(1,2);(1,3);(1,4)] |> Set.ofList
    let horizontalLines2 = [(2,0);(2,1);(2,2);(2,3);(2,4)] |> Set.ofList
    let horizontalLines3 = [(3,0);(3,1);(3,2);(3,3);(3,4)] |> Set.ofList
    let horizontalLines4 = [(4,0);(4,1);(4,2);(4,3);(4,4)] |> Set.ofList

module PatternMatcher =
    open BingoCard
    
    let markBall card ball =
        let markNumber cell =
            match cell with
            | NotCalled num ->
                if ball = num then BingoCard.Called num
                else BingoCard.NotCalled num
            | _ -> cell
        
        match card with
        | NewCard cells 
        | MarkedCard cells -> 
            let i = Bingo.getCol ball        
            match i with
            | Some i -> 
                let f = (fun row col -> 
                    if col = i then
                        cells.[row].[i] |> markNumber                        
                    else cells.[row].[col]      
                    )                 
                        
                Marked(createCells f)
            | None -> Marked cells
        | _ -> card        
     
    let matchPattern card pattern =
        let matchCell =
            function
            | Center | Called _ -> true
            | _ -> false

        match card with
        | MarkedCard cells ->            
            let matchedCells = 
                [
                    for row in 0..4 do
                        for col in 0..4 do                                
                            if matchCell cells.[row].[col] then
                                yield (row,col)                
                ] |> Set.ofList

            let inPatternCell =
                function
                | Called i -> InPattern i
                | cell -> cell

            if Set.isSubset pattern matchedCells then
                let f = (fun row col -> 
                    if pattern |> Set.exists ((=)(row,col)) then
                        inPatternCell cells.[row].[col] 
                    else
                        cells.[row].[col]
                )
                Some 
                    (Matched(createCells f)
//                        [|
//                            for row in 0..4 ->
//                            [|
//                                for col in 0..4 ->
//                                    if pattern |> Set.exists ((=)(row,col)) then
//                                        inPatternCell cells.[row].[col] 
//                                    else
//                                        cells.[row].[col]
//                            |]
//                        |]
                        )
            else None
        | _ -> None
             
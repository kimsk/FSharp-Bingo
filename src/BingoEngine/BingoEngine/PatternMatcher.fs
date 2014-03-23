namespace BingoEngine

module PatternMatcher =
    open BingoCard
    
    let markBall ball card =
        let markNumber cell =
            match cell with
            | NotCalled num ->
                if ball = num then BingoCard.Called num
                else BingoCard.NotCalled num
            | _ -> cell
        
        match card with
        | New cells 
        | Marked cells -> 
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
     
    let matchPattern pattern card =
        let matchCell =
            function
            | Center | Called _ -> true
            | _ -> false

        match card with
        | Marked cells ->            
            let matchedCells = 
                [
                    for row in 0..4 do
                        for col in 0..4 do                                
                            if matchCell cells.[row].[col] then
                                yield (row,col)                
                ] |> Set.ofList

            if Set.isSubset pattern matchedCells then
                let f = (fun row col -> 
                    if pattern |> Set.exists ((=)(row,col)) then
                        match cells.[row].[col] with
                        | Called i -> InPattern i
                        | cell -> cell
                    else
                        cells.[row].[col]
                )
                Matched(createCells f)
            else card
        | _ -> card             

    let markBallAndCheckPattern card ball pattern = card |> markBall ball |> matchPattern pattern
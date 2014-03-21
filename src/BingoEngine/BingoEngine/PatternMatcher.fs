namespace BingoEngine

module BingoPatterns =
    type Pattern = {Name:string; Pattern:Set<int*int>}

    let Patterns =
        [
            {Name = "horizontal-1"; Pattern = [(0,0);(0,1);(0,2);(0,3);(0,4)] |> Set.ofList}
            {Name = "horizontal-2"; Pattern = [(1,0);(1,1);(1,2);(1,3);(1,4)] |> Set.ofList}
            {Name = "horizontal-3"; Pattern = [(2,0);(2,1);(2,2);(2,3);(2,4)] |> Set.ofList}
            {Name = "horizontal-4"; Pattern = [(3,0);(3,1);(3,2);(3,3);(3,4)] |> Set.ofList}
            {Name = "horizontal-5"; Pattern = [(4,0);(4,1);(4,2);(4,3);(4,4)] |> Set.ofList}
            {Name = "vertical-1"; Pattern = [(0,0);(1,0);(2,0);(3,0);(4,0)] |> Set.ofList}
            {Name = "vertical-2"; Pattern = [(0,1);(1,1);(2,1);(3,1);(4,1)] |> Set.ofList}
            {Name = "vertical-3"; Pattern = [(0,2);(1,2);(2,2);(3,2);(4,2)] |> Set.ofList}
            {Name = "vertical-4"; Pattern = [(0,3);(1,3);(2,3);(3,3);(4,3)] |> Set.ofList}
            {Name = "vertical-5"; Pattern = [(0,4);(1,4);(2,4);(3,4);(4,4)] |> Set.ofList}
            { Name = "2 Stamps-1"; Pattern = [(0,0);(0,1);(1,0);(1,1);(3,0);(4,0);(4,1);(3,1)] |> Set.ofList};
            { Name = "2 Stamps-2"; Pattern = [(0,0);(1,0);(1,1);(0,1);(0,3);(1,3);(1,4);(0,4)] |> Set.ofList};
            { Name = "2 Stamps-3"; Pattern = [(0,0);(1,0);(1,1);(0,1);(3,3);(4,3);(4,4);(3,4)] |> Set.ofList};
            { Name = "2 Stamps-4"; Pattern = [(3,0);(4,0);(4,1);(3,1);(0,3);(1,3);(1,4);(0,4)] |> Set.ofList};
            { Name = "2 Stamps-5"; Pattern = [(3,0);(4,0);(4,1);(3,1);(3,3);(4,3);(4,4);(3,4)] |> Set.ofList};
            { Name = "2 Stamps-6"; Pattern = [(0,3);(1,3);(1,4);(0,4);(3,3);(4,3);(4,4);(3,4)] |> Set.ofList};
            { Name = "3 Stamps-1"; Pattern = [(0,0);(1,0);(1,1);(0,1);(3,0);(4,0);(4,1);(3,1);(0,3);(1,3);(1,4);(0,4)] |> Set.ofList};
            { Name = "3 Stamps-2"; Pattern = [(0,0);(1,0);(1,1);(0,1);(0,3);(1,3);(1,4);(0,4);(3,3);(4,3);(4,4);(3,4)] |> Set.ofList};
            { Name = "3 Stamps-3"; Pattern = [(0,0);(1,0);(1,1);(0,1);(3,0);(4,0);(4,1);(3,1);(4,3);(4,4);(3,4);(3,3)] |> Set.ofList};
            { Name = "3 Stamps-4"; Pattern = [(3,0);(4,0);(4,1);(3,1);(3,3);(4,3);(4,4);(3,4);(1,3);(0,3);(0,4);(1,4)] |> Set.ofList};
            { Name = "4 Stamps";   Pattern = [(0,0);(1,0);(1,1);(0,1);(0,3);(1,3);(1,4);(0,4);(3,0);(4,0);(4,1);(3,1);(3,3);(4,3);(4,4);(3,4)] |> Set.ofList};
            
        ]

    let toStr pattern =
        let line = "---------------------\r\n"

        let cells = BingoCard.createCells (fun _ _ -> "   ")
        
        for row, col in pattern.Pattern do
            cells.[row].[col] <- " # "

        let getRowStr row =
            row 
            |> Array.fold (fun acc i -> acc + i + "|") "|"

        pattern.Name + "\r\n" +
        line +
        (cells |> Array.fold (fun acc r -> acc + (getRowStr r) + "\r\n") "") +
        line

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

            if Set.isSubset pattern matchedCells then
                let f = (fun row col -> 
                    if pattern |> Set.exists ((=)(row,col)) then
                        match cells.[row].[col] with
                        | Called i -> InPattern i
                        | cell -> cell
                    else
                        cells.[row].[col]
                )
                Some(Matched(createCells f))
            else None
        | _ -> None
             
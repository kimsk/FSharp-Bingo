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
            { Name = "Diagonal-1"; Pattern = [(0,0);(1,1);(2,2);(3,3);(4,4);] |> Set.ofList};
            { Name = "Diagonal-2"; Pattern = [(4,0);(3,1);(2,2);(1,3);(0,4)] |> Set.ofList};
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
            { Name = "Lucky 7"; Pattern = [(0,0);(0,1);(0,2);(0,3);(0,4);(1,3);(2,2);(3,1);(4,0)]  |> Set.ofList};
            { Name = "Diamond"; Pattern = [(2,0);(1,1);(0,2);(1,3);(2,4);(3,3);(4,2);(3,1)]  |> Set.ofList};
            { Name = "Large Kite-1"; Pattern = [(0,0);(1,0);(2,0);(2,1);(2,2);(1,2);(0,2);(0,1);(1,1);(3,3);(4,4)] |> Set.ofList};
            { Name = "Large Kite-2"; Pattern = [(0,2);(1,2);(2,2);(2,3);(2,4);(1,4);(1,3);(0,3);(0,4);(4,0);(3,1)] |> Set.ofList};
            { Name = "Large Kite-3"; Pattern = [(2,0);(3,0);(4,0);(4,1);(4,2);(3,2);(2,2);(2,1);(3,1);(1,3);(0,4)] |> Set.ofList};
            { Name = "Large Kite-4"; Pattern = [(2,2);(3,2);(4,2);(4,4);(4,3);(3,3);(3,4);(2,4);(2,3);(1,1);(0,0)] |> Set.ofList};
            { Name = "Bar"; Pattern = [(0,0);(1,0);(2,0);(3,0);(4,0);(4,2);(3,2);(2,2);(1,2);(0,2);(0,4);(1,4);(2,4);(3,4);(4,4)] |> Set.ofList};
            { Name = "Small Frame"; Pattern = [(1,1);(2,1);(3,1);(3,2);(3,3);(2,3);(1,3);(1,2)] |> Set.ofList};
            { Name = "Large Frame"; Pattern = [(0,0);(1,0);(2,0);(3,0);(4,0);(4,1);(4,2);(4,3);(4,4);(3,4);(2,4);(1,4);(0,4);(0,3);(0,2);(0,1)] |> Set.ofList};
            { Name = "Goal Post"; Pattern = [(0,0);(1,0);(2,0);(2,1);(2,2);(2,3);(2,4);(1,4);(0,4);(3,2);(4,2)] |> Set.ofList};
            { Name = "X"; Pattern = [(0,0);(1,1);(2,2);(3,3);(4,4);(4,0);(3,1);(1,3);(0,4)] |> Set.ofList};
            { Name = "Y"; Pattern = [(0,0);(1,1);(2,2);(3,2);(4,2);(1,3);(0,4)] |> Set.ofList};
            { Name = "Z"; Pattern = [(0,0);(0,1);(0,2);(0,3);(0,4);(1,3);(2,2);(3,1);(4,0);(4,1);(4,2);(4,3);(4,4)] |> Set.ofList};
            { Name = "7-11"; Pattern = [(0,0);(0,1);(1,1);(2,1);(3,1);(4,1);(0,3);(2,3);(1,3);(3,3);(4,3);(4,4);(3,4);(2,4);(1,4);(0,4)] |> Set.ofList};
            { Name = "13"; Pattern = [(0,0);(1,0);(2,0);(3,0);(4,0);(4,2);(4,3);(4,4);(3,4);(2,4);(2,3);(1,4);(0,4);(0,3);(0,2)] |> Set.ofList};
            { Name = "Full Card"; Pattern = [(0,0);(0,1);(0,2);(0,3);(0,4);(1,4);(1,3);(1,2);(1,1);(1,0);(4,0);(3,0);(2,0);(2,1);(3,1);(4,1);(4,3);(3,2);(4,2);(3,3);(2,2);(2,3);(2,4);(3,4);(4,4)] |> Set.ofList};
            { Name = "Tiny I"; Pattern = [(1,1);(1,2);(1,3);(2,2);(3,2);(3,1);(3,3)] |> Set.ofList};
            { Name = "Small I"; Pattern = [(0,1);(0,2);(0,3);(1,2);(2,2);(3,2);(4,2);(4,1);(4,3)] |> Set.ofList};
            { Name = "Large I"; Pattern = [(0,0);(0,1);(0,2);(0,3);(0,4);(1,2);(2,2);(3,2);(4,0);(4,1);(4,2);(4,3);(4,4)] |> Set.ofList};
            { Name = "Poodle"; Pattern = [(0,0);(0,1);(1,1);(2,1);(3,1);(4,1);(2,2);(2,3);(2,4);(3,3);(4,3);] |> Set.ofList};
            { Name = "Pyramid"; Pattern = [(4,0);(4,1);(3,1);(4,2);(4,3);(4,4);(3,3);(3,2);(2,2);] |> Set.ofList};
            { Name = "Air Plane"; Pattern = [(2,0);(2,1);(1,1);(0,1);(3,1);(4,1);(2,2);(2,3);(2,4);(1,4);(3,4);] |> Set.ofList};
            { Name = "Chair"; Pattern = [(4,1);(3,1);(2,1);(2,2);(2,3);(1,4);(0,4);(2,4);(3,4);(4,4);] |> Set.ofList};
            { Name = "Corners"; Pattern = [(0,0);(4,0);(4,4);(0,4);] |> Set.ofList};
            { Name = "Dog Bone"; Pattern = [(1,0);(2,0);(3,0);(2,1);(2,2);(2,3);(2,4);(1,4);(3,4);] |> Set.ofList};
            { Name = "Arrow Head"; Pattern = [(4,2);(2,2);(1,2);(3,2);(0,2);(1,1);(2,0);(1,3);(2,4);] |> Set.ofList};
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

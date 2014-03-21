namespace BingoEngine

module BingoCard =    
    open Util
    open Bingo

    type Cell =
        | Center
        | NotCalled of int        
        | Called of int
        | InPattern of int

    type Card = 
        | New of Cell[][]
        | Marked of Cell[][]
        | Matched of Cell[][]

    let (|NewCard|_|) card =
        match card with
        | New cells -> Some cells
        | _ -> None

    let (|MarkedCard|_|) card =
        match card with
        | Marked cells -> Some cells
        | _ -> None

    let createCells f =
        [|
            for row in 0..4 ->                
                [|
                    for col in 0..4 ->
                        f row col
                |]
        |]

    let createNewCard () =
        let tmp = 
            [|
                (Shuffle B).[..4] |> Array.map NotCalled
                (Shuffle I).[..4] |> Array.map NotCalled
                (Shuffle N).[..4] |> Array.map NotCalled
                (Shuffle G).[..4] |> Array.map NotCalled
                (Shuffle O).[..4] |> Array.map NotCalled
            |]

        tmp.[2].[2] <- Center
        New(createCells (fun row col -> tmp.[col].[row]))

    let toStr card = 
        let cells, line = 
            match card with
            | New cells -> cells, "---------------------\r\n"
            | Marked cells -> cells, "+---+---+---+---+---+\r\n"
            | Matched cells -> cells, "=====================\r\n"

        //let line = "+---+---+---+---+---+\r\n" 
        let text = "| B | I | N | G | O |\r\n"
        let printCell cell =
            match cell with
            | Center -> " X "
            | NotCalled num -> 
                sprintf "%3i" num
            | Called num -> 
                sprintf "*%2i" num
            | InPattern num ->
                sprintf "#%2i" num

        let getRowStr row =
            row 
            |> Array.fold (fun acc i -> acc + (printCell i) + "|") "|"

        let cardStr = 
            cells |> Array.fold (fun acc r -> acc + (getRowStr r) + "\r\n") ""
            
        line + text + line + cardStr + line         
                

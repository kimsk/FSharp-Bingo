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

    let createNewCard () =
        let tmp = [|
                    (Shuffle B).[..4] |> Array.map NotCalled
                    (Shuffle I).[..4] |> Array.map NotCalled
                    (Shuffle N).[..4] |> Array.map NotCalled
                    (Shuffle G).[..4] |> Array.map NotCalled
                    (Shuffle O).[..4] |> Array.map NotCalled
                |]

        tmp.[2].[2] <- Center

        New [|
                [|for i in 0..4 -> tmp.[i].[0]|]
                [|for i in 0..4 -> tmp.[i].[1]|]
                [|for i in 0..4 -> tmp.[i].[2]|]
                [|for i in 0..4 -> tmp.[i].[3]|]
                [|for i in 0..4 -> tmp.[i].[4]|]
            |]

    let toStr card = 
        let cells = 
            match card with
            | New cells -> cells
            | Marked cells -> cells
            | Matched cells -> cells

        let line = "+---+---+---+---+---+\r\n" 
        let text = "| B | I | N | G | O |\r\n"
        let printCell cell =
            match cell with
            | Center -> " X "
            | NotCalled num -> 
                if num < 10 then sprintf "  %d" num
                else sprintf " %d" num
            | Called num -> 
                if num < 10 then sprintf " *%d" num
                else sprintf "*%d" num
            | InPattern num ->
                if num < 10 then sprintf " #%d" num
                else sprintf "#%d" num

        let getRowStr row =
            row 
            |> Array.fold (fun acc i -> acc + (printCell i) + "|") "|"

        let cardStr = 
            cells |> Array.fold (fun acc r -> acc + (getRowStr r) + "\r\n") ""
            
        line + text + line + cardStr + line         
                

namespace BingoEngine

module Bingo =     
    let B, I, N, G, O = [|1..15|], [|16..30|], [|31..45|], [|46..60|], [|61..75|]

    let getCol num =
        if num >= B.[0] && num <= B.[14] then Some 0
        elif num >= I.[0] && num <= I.[14] then Some 1
        elif num >= N.[0] && num <= N.[14] then Some 2
        elif num >= G.[0] && num <= G.[14] then Some 3
        elif num >= O.[0] && num <= O.[14] then Some 4
        else None
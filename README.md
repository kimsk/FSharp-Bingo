Bingo game with F#
-----------------

Bingo game engine [implemented](https://github.com/kimsk/FSharp-Bingo/tree/master/src/BingoEngine/BingoEngine) and [simulated](https://github.com/kimsk/FSharp-Bingo/blob/master/src/BingoEngine/BingoEngine/BingoGameSim.fsx) in F#. 

#### [Bingo Card module](https://github.com/kimsk/FSharp-Bingo/blob/master/src/BingoEngine/BingoEngine/BingoCard.fs)
```fsharp
type Cell =
    | Center
    | NotCalled of int   
    | Called of int
    | InPattern of int

type Card = 
    | New of Cell[][]
    | Marked of Cell[][]
    | Matched of Cell[][]

```

#### New Card
```fsharp
BingoCard.createNewCard()
```
| B | I | N | G | O |
|---|---|---|---|---|
| 13| 20| 44| 52| 62|
|  6| 26| 45| 54| 68|
|  7| 19| X | 51| 66|
|  9| 25| 36| 53| 74|
|  8| 24| 34| 47| 69|

#### Mark or Daub Bingo Card
```fsharp
PatternMatcher.markBall card 13
```
| B | I | N | G | O |
|---|---|---|---|---|
|*13| 20| 44| 52| 62|
|  6| 26| 45| 54| 68|
|  7| 19| X | 51| 66|
|  9| 25| 36| 53| 74|
|  8| 24| 34| 47| 69|


#### [Bingo Pattern](https://github.com/kimsk/FSharp-Bingo/blob/master/src/BingoEngine/BingoEngine/BingoPatterns.fs)
```fsharp
type Pattern = {Name:string; Pattern:Set<int*int>}
let vertical5 = {Name = "vertical-5"; Pattern = [(0,4);(1,4);(2,4);(3,4);(4,4)] |> Set.ofList}
```

#### Match Bingo Card with Pattern using [PatternMatcher](https://github.com/kimsk/FSharp-Bingo/blob/master/src/BingoEngine/BingoEngine/PatternMatcher.fs)
```fsharp
PatternMatcher.matchPattern card vertical5.Pattern
```

vertical-5

| B | I | N | G | O |
|---|---|---|---|---|
|  4|*25| 32|*49|#75|
|* 5| 18| 41| 53|#65|
|  8| 24| X | 48|#72|
|  1|*23| 43| 57|#64|
|  9| 29| 36| 59|#70|


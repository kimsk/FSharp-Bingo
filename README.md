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
```
---------------------
| B | I | N | G | O |
---------------------
|  3| 25| 32| 57| 75|
|  1| 29| 31| 48| 66|
| 15| 18| X | 54| 68|
|  8| 27| 33| 56| 63|
|  4| 28| 37| 55| 64|
---------------------
```

#### Mark or Daub Bingo Card
```fsharp
PatternMatcher.markBall card 8
```
```
+---+---+---+---+---+
| B | I | N | G | O |
+---+---+---+---+---+
|  3| 25| 32| 57| 75|
|  1| 29| 31| 48| 66|
| 15| 18| X | 54| 68|
|* 8| 27| 33| 56| 63|
|  4| 28| 37| 55| 64|
+---+---+---+---+---+
```


#### [Bingo Pattern](https://github.com/kimsk/FSharp-Bingo/blob/master/src/BingoEngine/BingoEngine/BingoPatterns.fs)
```fsharp
type Pattern = {Name:string; Pattern:Set<int*int>}
let diagonal2 = { Name = "Diagonal-2"; Pattern = [(4,0);(3,1);(2,2);(1,3);(0,4)] |> Set.ofList};
```

#### Match Bingo Card with Pattern using [PatternMatcher](https://github.com/kimsk/FSharp-Bingo/blob/master/src/BingoEngine/BingoEngine/PatternMatcher.fs)
```fsharp
PatternMatcher.matchPattern diagonal2.Pattern card
```

Diagonal-2
```
=====================
| B | I | N | G | O |
=====================
|  3| 25| 32| 57|#75|
|  1| 29| 31|#48| 66|
| 15| 18| X | 54| 68|
|* 8|#27| 33| 56| 63|
|# 4| 28| 37| 55| 64|
=====================
```


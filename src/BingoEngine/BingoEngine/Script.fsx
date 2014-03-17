#load "Util.fs"
#load "Bingo.fs"
#load "BingoCard.fs"

open BingoEngine

let card = BingoCard.createNewCard()

let cardStr = BingoCard.printNewCard card

printfn "%s" cardStr

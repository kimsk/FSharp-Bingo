namespace BingoEngine

module Util =
    open System

    let rng = new Random()

    // http://lorgonblog.wordpress.com/2008/03/05/play-ball-in-f/
    let Shuffle (org:int []) =
        // clone original array
        let arr = Array.copy org

        let n = arr.Length
        for x in 1..n do
            let i = n-x
            let j = rng.Next(i+1)
            let tmp = arr.[i]
            arr.[i] <- arr.[j]
            arr.[j] <- tmp
        arr
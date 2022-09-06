module Euler084

open DomainTypes

let run () = 

    let VERBOSEOUTPUT = false //true

    // set up game actions to be attached to squares and cards
    let getSquareAtPosition game = game.board[game.currentPos]
    let moveTo newPos game = 
        let newGame = {
            board = game.board
            currentPos = newPos
            communityChestDeck = game.communityChestDeck
            chanceDeck = game.chanceDeck
        }
        let newSquare = getSquareAtPosition newGame
        if VERBOSEOUTPUT then printfn "Moved to: %s" newSquare.name else ()
        let endTurnfunc = newSquare.endTurnFunc
        endTurnfunc newGame
    let goToJail game = moveTo 10 game
    let advanceToGo game = moveTo 0 game
    let goToC1 game = moveTo 11 game
    let goToE3 game = moveTo 24 game
    let goToH2 game = moveTo 39 game
    let goToR1 game = moveTo 05 game
    let goToNextR game = 
        let newPos =
            match game.currentPos with
            | x when x < 5 || x > 35 -> 5
            | x when x > 5 && x < 15 -> 15
            | x when x > 15 && x < 25 -> 25
            | x when x > 25 && x < 35 -> 35
        moveTo newPos game
    let goToNextU game = 
        let newPos =
            match game.currentPos with
            | x when x < 12 || x > 28 -> 12
            | x when x > 12 && x < 28 -> 28
        moveTo newPos game
    let goBack3 game = moveTo (game.currentPos - 3) game
    let incrementBoardSquare game =
        let currentSquare = getSquareAtPosition game
        let newSquare = {
            position = currentSquare.position
            name = currentSquare.name
            endTurnFunc = currentSquare.endTurnFunc
            numTurnEnds = currentSquare.numTurnEnds + 1
            }
        let newBoard = 
            game.board
            |> List.mapi (fun i s -> if i = game.currentPos then newSquare else s)
        {
            board = newBoard
            currentPos = game.currentPos
            chanceDeck = game.chanceDeck
            communityChestDeck = game.communityChestDeck
        }
    let doNothing game = 
        // turn is over. update the board square you landed on and return the game
        incrementBoardSquare game
    let drawCommunityChest game = 
        let card = game.communityChestDeck[0]
        if VERBOSEOUTPUT then printfn "Draw card: %s" card.name else ()
        let newDeck = 
            game.communityChestDeck 
            |> List.mapi (fun i card -> 
                if i = (List.length game.communityChestDeck) - 1 
                then game.communityChestDeck[0]
                else game.communityChestDeck[i + 1]
                )
        let newGame = {
            board = game.board
            currentPos = game.currentPos
            communityChestDeck = newDeck
            chanceDeck = game.chanceDeck
        }
        card.cardFunc newGame
    let drawChance game = 
        let card = game.chanceDeck[0]
        if VERBOSEOUTPUT then printfn "Draw card: %s" card.name else ()
        let newDeck = 
            game.chanceDeck 
            |> List.mapi (fun i card -> 
                if i = (List.length game.chanceDeck) - 1 
                then game.chanceDeck[0]
                else game.chanceDeck[i + 1]
                )
        let newGame = {
            board = game.board
            currentPos = game.currentPos
            communityChestDeck = game.communityChestDeck  
            chanceDeck = newDeck
        }
        card.cardFunc newGame
    let rollDice diceSides =
        let random1 = System.Random()
        let random2 = System.Random()
        random1.Next(1, diceSides + 1), random2.Next(1, diceSides + 1)
    let rec takeTurn maxTurns diceSides game numTurnsSoFar doublesInARow = 
        if numTurnsSoFar > maxTurns then game
        else
            if VERBOSEOUTPUT 
                then 
                    let square = getSquareAtPosition game
                    printfn "New turn. Current square: %s" square.name
                else ()
            let d1, d2 = rollDice diceSides
            if VERBOSEOUTPUT then printfn "Dice rolled: %d, %d" d1 d2 else ()
            if d1 = d2 && doublesInARow = 2 then
                if VERBOSEOUTPUT then printfn "Too many doubles. Go to Jail" else ()
                let newGameState = goToJail game
                takeTurn maxTurns diceSides newGameState (numTurnsSoFar + 1) 0
            else
                let nextDoubles = if d1 = d2 then doublesInARow + 1 else 0
                let nextPos = 
                    let raw = game.currentPos + d1 + d2
                    if raw >= game.board.Length then raw - game.board.Length
                    else raw
                let newGameState = moveTo nextPos game
                takeTurn maxTurns diceSides newGameState (numTurnsSoFar + 1) nextDoubles
    let rec shuffle (deck:List<MonopolyCard>) numSwapsSoFar numSwapsTotal =
        if numSwapsSoFar > numSwapsTotal then deck
        else
            let random1 = System.Random()
            let random2 = System.Random()
            let card1 = random1.Next(1, deck.Length)
            let card2 = random2.Next(1, deck.Length)
            if card1 = card2 then shuffle deck numSwapsSoFar numSwapsTotal // that one didn't count
            else
                deck
                |> List.mapi (fun i card -> 
                    if i = card1 then deck[card2]
                    elif i = card2 then deck[card1]
                    else card
                    )
    let rec playGame maxTurns diceSides numberOfShuffleSwaps newGame gamesPlayedSoFar totalGames =
        if gamesPlayedSoFar > totalGames then newGame
        else
            let endOfGame = takeTurn maxTurns diceSides newGame 0 0
            let nextGame = {
                board = endOfGame.board
                currentPos = 0
                chanceDeck = shuffle newGame.chanceDeck numberOfShuffleSwaps 0
                communityChestDeck = shuffle newGame.communityChestDeck numberOfShuffleSwaps 0
            }
            playGame maxTurns diceSides numberOfShuffleSwaps nextGame (gamesPlayedSoFar + 1) totalGames

    // set up the game
    let board = [
        { position = "00"; name = "Go"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "01"; name = "Mediterranean Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "02"; name = "Community Chest 1"; endTurnFunc = drawCommunityChest; numTurnEnds = 0 };
        { position = "03"; name = "Baltic Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "04"; name = "Income Tax"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "05"; name = "Reading Railroad"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "06"; name = "Oriental Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "07"; name = "Chance 1"; endTurnFunc = drawChance; numTurnEnds = 0 };
        { position = "08"; name = "Vermont Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "09"; name = "Connecticut Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "10"; name = "Jail"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "11"; name = "St. Charles Pl"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "12"; name = "Electric Co"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "13"; name = "States Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "14"; name = "Virginia Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "15"; name = "Pennsylvania Railroad"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "16"; name = "St. James Pl"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "17"; name = "Community Chest 2"; endTurnFunc = drawCommunityChest; numTurnEnds = 0 };
        { position = "18"; name = "Tennessee Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "19"; name = "New York Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "20"; name = "Free Parking"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "21"; name = "Kentucky Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "22"; name = "Chance 2"; endTurnFunc = drawChance; numTurnEnds = 0 };
        { position = "23"; name = "Indiana Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "24"; name = "Illinois Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "25"; name = "B&O Railroad"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "26"; name = "Atlantic Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "27"; name = "Ventnor Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "28"; name = "Water Works"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "29"; name = "Marvin Gardens"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "30"; name = "Go to Jail"; endTurnFunc = goToJail; numTurnEnds = 0 };
        { position = "31"; name = "Pacific Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "32"; name = "North Carolina Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "33"; name = "Community Chest 3"; endTurnFunc = drawCommunityChest; numTurnEnds = 0 };
        { position = "34"; name = "Pennsylvania Ave"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "35"; name = "Short Line"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "36"; name = "Chance 3"; endTurnFunc = drawChance; numTurnEnds = 0 };
        { position = "37"; name = "Park Place"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "38"; name = "Luxury Tax"; endTurnFunc = doNothing; numTurnEnds = 0 };
        { position = "39"; name = "Boardwalk"; endTurnFunc = doNothing; numTurnEnds = 0 };
    ]
    let chanceDeck = [
        { name = "Advance to Go"; cardFunc = advanceToGo }
        { name = "Go to Jail"; cardFunc = goToJail }
        { name = "Go to C1"; cardFunc = goToC1 }
        { name = "Go to E3"; cardFunc = goToE3 }
        { name = "Go to H2"; cardFunc = goToH2 }
        { name = "Go to R1"; cardFunc = goToR1 }
        { name = "Go to next R"; cardFunc = goToNextR }
        { name = "Go to next R"; cardFunc = goToNextR }
        { name = "Go to next U"; cardFunc = goToNextU }
        { name = "Go back 3"; cardFunc = goBack3 }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
    ]
    let communityChestDeck = [
        { name = "Advance to Go"; cardFunc = advanceToGo }
        { name = "Go to Jail"; cardFunc = goToJail }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }
        { name = "Do nothing"; cardFunc = doNothing }

    ]

    let numGames = 1000
    let maxTurns = 1000
    let diceSides = 4
    let numberOfShuffleSwaps = 12
    let newGame = {
        board = board
        currentPos = 0
        chanceDeck = chanceDeck
        communityChestDeck = communityChestDeck
    }

    let endState = playGame maxTurns diceSides numberOfShuffleSwaps newGame 0 numGames 
    endState.board
    |> List.sortByDescending (fun s -> s.numTurnEnds)
    |> (fun b -> sprintf "%s%s%s" b[0].position b[1].position b[2].position)
    


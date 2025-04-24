
VAR met_crank = false
   
===Crank===
    
    The Old Gob is rocking in his chair with his eyes closed; Peacful, as if he were in an eternal slumber...
    
    +"Umm... Mister?"
        ->Off_my_porch
    +"Yo Grandpa!" 
        ->Off_my_porch
    +Leave and come back later
        ->DONE
    
    
    ===Off_my_porch===
    ~met_crank = true
    
    "Get off my porch! I said get off, get off! Get your goddamn floppers off my porch!"
    
    +"I just wanted to ask[?"]if you-"
        "I said get the hell off my porch! Get off, get off!"
        ++"Oh my god but[?"] I just-" 
            "Off! Off my porch! I said off!"
            +++"Okay, Okay!" <>
        ++"Okay, Okay!" <>
    +"Okay, Okay!" <>
        
    - Well that didn't work. Guess I got to try something else.
        ->DONE  
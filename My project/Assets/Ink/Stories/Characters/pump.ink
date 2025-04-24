VAR met_pump = false


===Pump===
    
    The beaten up gob sat sun bathing on his bench.  He seems to be knocked out like a light. 
    
    +"Umm... escuse me sir?"
        ->Off_my_bench
    +"Yo you up!" 
        ->Off_my_bench
    +Leave and come back later
        ->DONE
    
    
    ===Off_my_bench===
    ~met_pump = true
    
    "Ahh, ahh get out of here! I don't know nothin' bout' nothin', now stop bothering me!" - Pumplscroob.. 
    
    +"I just wanted to ask[?"]if you-"
        "I told you I don't know nothin'. Now get! I said get!" 
        ++"Look[!"] I just-" 
            "Get! I said get! I don't know nothin'! Get! Get!"
            +++"Okay, Okay!" <>
        ++"Okay, Okay!" <>
    +"Okay, Okay!" <>
        
   - Alright... So that won't work. 
        ->DONE
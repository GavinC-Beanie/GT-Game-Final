


===back_to_crank===

The Old Gob is rocking in his chair with his eyes closed; Peacful, as if he were in an eternal slumber...
    
    +"Umm... Mister?"
        ->Woah_Woah
    +"Yo Grandpa!" 
        ->Woah_Woah
    +Start smoking
        ->Hes_up
    
   
    
    
    ===Woah_Woah===
    
    "Get off my porch! I said get off, get off! Get your goddamn floppers off my porch!"
    
    +["Let's smoke Grandpa!"]"I'm just trying to smoke with you Grandpa!"
        ->Hes_up
    +"Not this again["], I'm out!"
        ->DONE
        
        
===Hes_up===

"Ahhhh... well I can't deny such a kind, young man in need." 

+"Wow..."
    "What!"
    ++"Oh, nothing..."
        ->Want_something
    ++"Well[..."], Bill siad that he smoked with you and-"
        "DOOOOOOOOn't care." 
        +++"Okay, damn."
            ->Want_something
        +++"Really!" 
            ->Want_something
+"I was wondering..."
    "Damn! Let a man relax first!"
    ++"Okay["], my bad, my bad."
        ->Want_something
    ++"Whatever."
        "You got a problem kid!"
        +++"No sir."
        ->Want_something
        +++"Yeah[!"], you!"
            "Then you can get the hell off my porch then!"
            ->DONE
            
===Want_something===

"Well I can only assume I'm getting this tasty treat becasue you want something."

+"Well[..."], I was wondering if I could ask you a few questions?"
    ->Questions
+"Yeah,[ it's about Bill."] I'm helping out Bill and wanted to ask a few questions?
    ->Questions
    
===Questions===

{"Yeah, sure." | "Ask away." | "Sure." }

+"Do you play any golf?"
    ->Golf_Question
    
+"Are you good friends with Bill?"
    ->Bill_Question
    
+"Have you seen anything[?"] suspitions recently?"
    ->Sus_Question

- "Well that's all I got for now."
    ->DONE

===Golf_Question

"Does it look like I play golf?!"
    +"Okay[."], point taken."
        "I hope it was!"
        ++"Could I ask another?"
            ->Questions
        ++"Okay, done here!"
            "Wonderful! Now get off my porch!"
            ->DONE

===Bill_Question

"He's a nice guy. Smoke with him here and there. Fun guy to chat with. Other than that though, I don't know much."
    +"Alright[...], alright..."
        "Yeah it better be alright."
        ++"Could I ask another?"
            ->Questions
        ++"Welp, i'm all good!"
            "Perfect! Now get off my porch!"
            ->DONE
        
===Sus_Question
~ met_2ndCrank = true
~ OnVariableChanged("met_2ndCrank", "Grandpa")
"Well, I did see the gob who boats people taking some kid over to the corse. Was at a weird time too. Kid seemed to be fine so I wasn't worried."
    +"Very intersting!"
        "Is it now!"
        ++"Could I ask another?"
            ->Questions
        ++"Awesome, i'm all good!"
            "Glad to hear. Now get off my porch!"
            ->DONE
        
















->DONE

















->DONE













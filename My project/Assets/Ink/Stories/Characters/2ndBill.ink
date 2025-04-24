
===Back_to_Bill===

"I see you're back, how's it going?"

+ "You smoke[?"] with the cranky do over there?"
    "Ha, yeah! Where'd you find that out?"
    ++ "The pie lady."
        ->I_see
+["Not great"]"No one knows anything!"
    "Damn!"
    ++"Right[!"], how can no one know anything!"
        "It's okay, I know what to do."
        +++"What?"
        ->I_see
        +++"Alright, bet!"
        ->I_see
        
===I_see===

"So, sometimes... I just got to relax a little. To my suprise, the old bastard next door does too. Great guy that one! Here, let's have a puff."

+"Oh hell yeah![] And you do this with the cranky guy?!"
    "Yep, only takes a little and he's got stories for days." 
    ++"Wow!"
        ->Try_Yourself
    ++"No way!"
        "Im serious. He's quite and intriging guy really."
        +++"Intresting["]..."
        ->Try_Yourself
+"Nah[...], I'm alright."
    "You sure, it might even help with talkin' to gobs round here."
    ++"You think so?"
        "Yeah, like the Crank, you'd be able to talk to him for sure!"
        +++"Okay, okay..."
            ->Try_Yourself
        +++"Hmm..."
            ->Try_Yourself



->DONE


===Try_Yourself===

"Yep, try smoking with him yourself. Maybe he'll be willing to chat!"

+"Aright[!"], I'll give it a shot!"
    "Awesome, glad I could help!"
    ->DONE
+"Well...[] I don't know but thanks anyways."
    "Of course, just trying to help!"
    ->DONE
    
    
    
    
    

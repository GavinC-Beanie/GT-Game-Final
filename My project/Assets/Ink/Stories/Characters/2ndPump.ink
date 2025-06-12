


===back_to_pump===

"I told you I do't know nothin'!"

+"I know you're lying!"
    ->Not_lying
+"The cranky thing say's otherwise!"
    ->Not_lying
    

===Not_lying

"Ugh, no, no! You ain't got proof of nothin'"

+"Well...['The crank said!'"]the cranky ole gob says he saw you boatin' some kid!"
    ->Boats

+"Well...['How bout that boat!'] I hear that you've been giving late night boat rides!"
    ->Boats

===Boats

"Ah, ah okay, okay! I'm sorry. He's just so weird!"

+"I'm sorry, what[!"] are you talking about!"
    ->The_kid

+"He's [weird!"]the weird one!"
    Rude... But...<>
    ->The_kid



===The_kid

<> "Well...this kid, he's a freak. Nice kid but weird. Makes me take him out there. Just can't say no. Hell if I kown a lic more!"

+"[Where is he?]Well where could I find this freakazoid?"
    ->Where_freak

+"[Why's he so weird?]What makes this gob such a freakazoid?"
    ->Why_freak
    


===Why_freak
~ met_2ndPump = true
~ OnVariableChanged("met_2ndPump", "Tent_Dweller")
"Ugh! He likes to piss everywhere. Also got pie on his face. Plays with weird shit! Rotten flower I tell you!"

+"Weird...[where is he?"]Where could I find this pissing weirdo!"
    ->Where_freak
+"Intriguing...What else[!"] can you tell me about this weird rotting thing?"
    "Ugh! Nothing! Well... He loves pie. I know that."
    ++"Intresting...[thanks!"]Thanks for the help tent dweller!"
        ->DONE
    ++"Intresting...[boat rides?"]Could you take me on a boat trip?"
        "No... uhhh... I'm busy right now. Maybe another time..."
        ->DONE
    
    
===Where_freak

"He's normally at Nancy's. That's his Gran. He also eats up all those damn pies..."
    
+"Intresting...[thanks!"]Thanks for the help tent dweller!"
    ->DONE
+"Intresting...[boat rides?"]Could you take me on a boat trip?"
    "No... uhhh... I'm busy right now. Maybe another time..."
    ->DONE
    




->DONE









  




->DONE










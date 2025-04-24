 ===Gram===
    //Grandma Gob is old and after a while she actually very sweet, but if you haven't baught a pie from her yet, all she will care about is seeling that first pie. After that, she'll talk about anything with anyone. But before that, she's so damn... ugh you just wanna unplug her life support you know. 
   
    "Hey there honey! Come get yourself a yummy ole pie!" 
    
    +{not Buys_Pie}"Hey Grandma[!"], have you met Bill across the way?"
        ->Shorty
    +{not Buys_Pie}"Hi Ma'am[?"], would you happen to know anything about the golf course across the way?"
        ->Shorty
    +{not Buys_Pie}"Pies!"
        "<>
        ->Pies
    
    
    
    
    ===Shorty===
    //Kinda sarcastic and sassy grandma type shit 
    "Well shorty, I got pies, more pies, a few more pies, and only a few more years. Now what are ya thinkin'?" 
    
    +"That's just not what I asked at all." 
        "Well I asked about pies, didn't I?" //didn't I should be snarky af
        ->ALRIGHT
    +"Umm, well...["] I wanted to ask you about the golf corse over there
        "And I asked you if you wanted any pie, ya?" 
        ->ALRIGHT
    +"Pies... Huh!"
        "<>
        ->Pies
    +"Shorty!"
        "There a problem Honey" 
        ++"Yes!"
        ->Problem_with_gram
        ++"Ugh[!"], no there's no problem"
        ->No_Problem_with_gram
            
            
    

    
    ===ALRIGHT===
    +"Okay...[wow!"]. So... is the only thing you care about... Pie?"
        "Maybe! Would ya like a pie honey?"  //The maybe is quick and light hearted but quikly skiped over onto the question
        ->Ugh_pie
    +"Damn[!"], I'll buy a pie but can I just ask you some questions?"
        "Can you just buy a pie first... honey?"  //Oh the honey should make the player want to punch their screen
        ->Ugh_pie
    +"Alright...["], I'll take a pie
        "Wonderful! <> // 
        ->Pies
    
        
    
    ===Ugh_pie===
    +"Ugh...Sure"//very annoyed
        "Wonderful! <> // 
        ->Pies
    +"Like...[?] why are you so fixated on this pie!" //so confused
        "Honey why don't you just buy a pie, ya!" //omg lady like shut up type shit
        ++"Oh my god! Fine!"
            "Wonderful! <> // - Grandm Gob 
            ->Pies
        ++"Holy shit! I'm Out!
            "Oh... How sad."  //Condesending af 
            ->Didnt_Buy_Pie
    
    
    
    
    ===Problem_with_gram===
    "Well what's the problem honey?"  //sweet and unsepecting
    
    +"Why[!"] did you call me shorty just now!"
        "I can't fathom what you could be talking about"  //sweet and unsepecting
        ++"Oh my god you can't be serious!" 
            "What ever could you mean?"  // strait up sarcastic 
            +++"I'm done"
                "Oh... How sad."  //Condesending af 
                ->Didnt_Buy_Pie
            +++"Ugh, just give me a pie." // annoyed
                "Wonderful! <> // 
                ->Pies
        +"Okay...["] well then..." 
            "How bout ya just buy a pie honey!" 
            ++"Oh my god! Fine!"
                "Wonderful! <> // - Grandm Gob
                ->Pies
            ++"I'm done"
                "Oh... How sad."  //Condesending af 
                ->Didnt_Buy_Pie
        
        
        
    
    ===No_Problem_with_gram===
    "Well that's good... How bout you buy a pie!" 
    
    +"Oh my god[!"], can you take a chill pill!"
        "A pitch kill!?" 
        ++"Holy fuck, I can't!" 
            "Oh... How sad."  //Condesending af 
            ->Didnt_Buy_Pie
        ++"Okay, just give me a pie!"
            "Wonderful! <> // - Grandma Pies
            ->Pies
    +"Yes, yes, I'll take a pie!" 
        "Wonderful! <> // 
        ->Pies
        
      
        
    ===Pies===
    <>Unfortunately, I only have grape jelly jam filled right now! Will the do?" 
    
    
    +"Oh yeah, for sure!" 
        ->Buys_Pie
    +"Yeah, sure." 
        ->Buys_Pie
    +"Eww![] Thats all you have?
        "Unfortunately, yes." // sweet and unsespecting
        ++"Ugh, sure." 
        ->Buys_Pie
        ++"Oh no, I can't do grape!
            "Oh no, that's so unfortunate."  //so condisending
            ->Didnt_Buy_Pie
            

            
    ===Buys_Pie===
    "Wonderful! Here ya go!" 
    ->After_First_Pie
    
 
    
    
    ===After_First_Pie===
    +"Awesome[!"], thanks!"
        "Well thank you. Anything else I can do for ya honey?"  // Acutally kind
        ++"Acutally, yes!" 
            ->Talking_with_Gram
        ++"Nah...["] I think I'm all good!" 
            ->DONE
    +"So...["] can I ask you a few questions now?
        No problem at all. <> 
            ->Talking_with_Gram
    +"There[!"], you happy!"
        "Yes, thank you so much honey. Anything else I can do for ya!" //First part snarky, second part nicer
        ++"Acutally, yes!" 
            ->Talking_with_Gram
        ++"Nah...["] I think I'm all good!"
            ->DONE
   
    
    ===Talking_with_Gram===
    "So, {what's up?" | anything else?" | anything else?" | anything else?"}
    
        
    +"Do you know [about the golf corse?"]anything about the golf course over yander?"
        "Oh Bill's. He's a sweet boy. I don't know about his golf course but he's a special one you see." 
        ++"Oh-kay..."
            ->Talking_with_Gram
        ++"That's it?"
            "Nope!" 
            +++"Oh-kay..."
                ->Talking_with_Gram
            +++"Cool... I think I'm good!"
                ->DONE
                
    +{not met_crank}"Hmm... the old man?["] The one on the porch over there?" 
        "Oh he's just a cranky ole man. Loves that porch. Sometimes too much..."
        ++Okay...
            ->Talking_with_Gram
        ++"Cool... I think I'm good!"
            ->DONE
            
            
    +{met_crank}"The crazy gezzer maybe?["] He loves his porch a little too much!" 
        "Oh yeah, he can be a little much. Only person i've seen calm him down is Bill actual. I try to tell him that smoking that stuff can't be good for him, but he says it does the trick." 
        ++Okay...
            ->Talking_with_Gram
        ++"Cool... I think I'm good!"
            ->DONE
        
    +{not met_pump}"Uhh... the tent Gob?["] What's up with him?"
        "Not really. He's just a crazy one honestly. Damn gobs always boating somewhere." 
        ++Okay...
            ->Talking_with_Gram
        ++"Cool... I think I'm good!"
            ->DONE
        
    +{met_pump}"What's with the tent dweller?["] He was acting crazy when I tried talking to him!"
        "Oh yeah, damn weirdos always asking for free food too. Ugh, pisses me off I tell you!"
        ++Okay...
            ->Talking_with_Gram
        ++"Cool... I think I'm good!"
            ->DONE
        
    +{not met_skate} "Err...Who's the kid[?"] The crazy one on the ramp over there." 
        "Oh he's just some kid. Popped up one day and he's been messing around in that pool ever since." 
        ++Okay...
            ->Talking_with_Gram
        ++"Cool... I think I'm good!"
            ->DONE
        
    +{met_skate} "Did that kid just die...["] Is he okay?!" 
        "Oh I'm sure he's fine. One time he threw himslef off a roof top... both his legs looked like pretzels." 
        ++"Oh... O-kay..."
            ->Talking_with_Gram
        ++"Cool... I think I'm good!"
            ->DONE
        ++"Is that normal?!"
            "As noraml as it comes sweet pea! I mean we are goblins after all."
            +++Realy?!
                ->Talking_with_Gram
            
            +++"Cool... I think I'm good!"
                ->DONE
         
    
    ->DONE
    
    "Awesome, I hope I was helpful."
            ->DONE
    
    ===Didnt_Buy_Pie
    ->DONE
    
    ->Home
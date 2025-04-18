 ===Gram===
    //Grandma Gob is old and after a while she actually very sweet, but if you haven't baught a pie from her yet, all she will care about is seeling that first pie. After that, she'll talk about anything with anyone. But before that, she's so damn... ugh you just wanna unplug her life support you know. 
    
    Grandma Gob is standing behind her portable pie cart. It's not the prettiest but it's something. There's still the remnice of a painted hot dog on the side. She couldn't have done much to fresshen it up. 
    
    "Hey there honey! Come get yourself a yummy ole pie!" - Grandma Gob
    
    *{not Buys_Pie}"Hey Grandma[!"], have you met Bill across the way?"
        ->Shorty
    *{not Buys_Pie}"Hi Ma'am[?"], would you happen to know anything about the golf course across the way?"
        ->Shorty
    *{not Buys_Pie}"Pies!"
        "<>
        ->Pies
    
    
    
    
    ===Shorty===
    //Kinda sarcastic and sassy grandma type shit 
    "Well shorty, I got pies, more pies, a few more pies, and only a few more year. Now what are ya thinkin'?" - Grandma Gob
    
    *"That's just not what I asked at all." 
        "Well I asked about pies, didn't I?" - Grandma Gob//didn't I should be snarky af
        ->ALRIGHT
    *"Umm, well...["] I wanted to ask you about the golf corse over there
        "And I asked you if you wanted any pie, ya?" - Grandma Gob
        ->ALRIGHT
    *"Pies... Huh!"
        "<>
        ->Pies
    *"Shorty!"
        "There a problem Honey" - Grandma Gob
        **"Yes!"
        ->Problem_with_gram
        **"Ugh[!"], no there's no problem"
        ->No_Problem_with_gram
            
            
    

    
    ===ALRIGHT===
    *"Okay...[wow!"]. So... is the only thing you care about... Pie?"
        "Maybe! Would ya like a pie honey?" - Grandma Gob //The maybe is quick and light hearted but quikly skiped over onto the question
        ->Ugh_pie
    *"Damn[!"], I'll buy a pie but can I just ask you some questions?"
        "Can you just buy a pie first... honey?" - Grandma Gob //Oh the honey should make the player want to punch their screen
        ->Ugh_pie
    *"Alright...["], I'll take a pie
        "Wonderful! <> // - Grandma Gob
        ->Pies
    
        
    
    ===Ugh_pie===
    *"Ugh...Sure"//very annoyed
        "Wonderful! <> // - Grandma Gob
        ->Pies
    *"Like...[?] why are you so fixated on this pie!" //so confused
        "Honey why don't you just buy a pie, ya!" //omg lady like shut up type shit
        **"Oh my god! Fine!"
            "Wonderful! <> // - Grandm Gob 
            ->Pies
        **"Holy shit! I'm Out!
            "Oh... How sad." - Grandma Gob //Condesending af 
            ->Didnt_Buy_Pie
    
    
    
    
    ===Problem_with_gram===
    "Well what's the problem honey?" - Grandma Gob //sweet and unsepecting
    
    *"Why[!"] did you call me shorty just now!"
        "I can't fathom what you could be talking about" - Grandma Gob //sweet and unsepecting
        **"Oh my god you can't be serious!" 
            "What ever could you mean?" - Grandma Gob // strait up sarcastic 
            ***"I'm done"
                "Oh... How sad." - Grandma Gob //Condesending af 
                ->Didnt_Buy_Pie
            ***"Ugh, just give me a pie." // annoyed
                "Wonderful! <> // - Grandma Gob
                ->Pies
        *"Okay...["] well then..." 
            "How bout ya just buy a pie honey!" 
            **"Oh my god! Fine!"
                "Wonderful! <> // - Grandm Gob
                ->Pies
            **"I'm done"
                "Oh... How sad." - Grandma Gob //Condesending af 
                ->Didnt_Buy_Pie
        
        
        
    
    ===No_Problem_with_gram===
    "Well that's good... How bout you buy a pie!" 
    
    *"Oh my god[!"], can you take a chill pill!"
        "A pitch kill!?" 
        **"Holy fuck, I can't!" 
            "Oh... How sad." - Grandma Gob //Condesending af 
            ->Didnt_Buy_Pie
        **"Okay, just give me a pie!"
            "Wonderful! <> // - Grandma Pies
            ->Pies
    *"Yes, yes, I'll take a pie!" 
        "Wonderful! <> // - Grandma Gob
        ->Pies
        
      
        
    ===Pies===
    <>Unfortunately, I only have grape jelly jam filled right now! Will the do?" -Grandma Gob
    
    *"Oh yeah, for sure!" 
        ->Buys_Pie
    *"Yeah, sure." 
        ->Buys_Pie
    *"Eww![] Thats all you have?
        "Unfortunately, yes." // sweet and unsespecting
        **"Ugh, sure." 
        ->Buys_Pie
        **"Oh no, I can't do grape!
            "Oh no, that's so unfortunate." - Grandma Gob //so condisending
            ->Didnt_Buy_Pie
            

            
    ===Buys_Pie===
    "Wonderful! Here ya go!" 
    ->After_First_Pie
    
 
    
    
    ===After_First_Pie===
    *"Awesome[!"], thanks!"
        "Well thank you. Anything else I can do for ya honey?" - Grandma Gob // Acutally kind
        **"Acutally, yes!" 
        ->Talking_with_Gram
        **"Nah...["] I think I'm all good!" 
        ->DONE
    *"So...["] can I ask you a few questions now?
        No problem at all. <> 
        ->Talking_with_Gram
    *"There[!"], you happy!"
        "Yes, thank you so much honey. Anything else I can do for ya!" //First part snarky, second part nicer
        **"Acutally, yes!" 
        ->Talking_with_Gram
        **"Nah...["] I think I'm all good!"
        ->DONE
   
    
    
    ===Talking_with_Gram===
    
    
    *"Would you happend to know [about the golf corse?"]anything about the golf course over yander?"
        "Oh Bill's. He's a sweet boy. I don't know about his golf course but he's a special one you see." - Gram
        **"Oh-kay..."
            ->Talking_with_Gram
        **"And there's nothing else?"
            "Nope!" - Gram
            ***"Oh-kay..."
                ->Talking_with_Gram
                
    *{not Crank}"Could you tell me anything about the old man[?"] on the porch?" 
        "Oh he's just a cranky ole man. Loves that porch. Sometimes too much..." -Gram
        "Intresting" 
        ->Talking_with_Gram
        
    *{Crank}"What's up with the crazy old man?["] He loves his porch a little too much!" 
        "Oh yeah, he can be a little much. Only person i've seen calm him down is Bill actual. I try to tell him that smoking that stuff can't be good for him, but he says it does the trick." - Gram
        "Okay... very intresting!" 
        ->Talking_with_Gram
        
    *{not Pump}"Do you know anything about the gob next to the tent?"
        "Not really. He's just a crazy one honestly. Damn gobs always boating somewhere." - Gram
        "Okay..." 
        ->Talking_with_Gram
        
    *{Pump}"What's wrong with the tent dweller?["] He was acting crazy when I tried talking to him!"
        "Oh yeah, damn weirdos always asking for free food too. Ugh, pisses me off I tell you!"
        "Al-right..." 
        ->Talking_with_Gram
        
    *{not Crazy_Skater} "You know anything about the kid[?"] over by the pool?" 
        "Oh he's just some kid. Popped up one day and he's been messing around in that pool ever since." - Gram
        "Huh, okay." 
        ->Talking_with_Gram
        
    *{Crazy_Skater} "I swear I just watched that kid die...["] Is he okay?!" 
        "Oh I'm sure he's fine. One time he threw himslef off a roof top... both his legs looked like pretzels." - Gram
        **"Oh... O-kay..."
            ->Talking_with_Gram
        **"Is that normal?!"
            "As noraml as it comes sweet pea! I mean we are goblins after all." - Gram
            "What the... well... alrighty." 
            ->Talking_with_Gram
            
    +"Okay... I think I'm good!" 
        "Awesome, I hope I was helpful." - Gram
    
    ->DONE
    
    
    
    ===Didnt_Buy_Pie
    ->DONE
===Bill=== 

//Bill Speaking//
"Ah you must be that new gob they sent to help me out! The name's Bill, Dwaggin', Bill the Dwaggin'... Dwaggin' the Bill? What's your's?" - Bill The Dwaggin' 
    * "Uhh...["], I guess I don't have one." 
        "Huh? You're an intresting one arn't you?" - Bill the Dwaggin' 
        -> Intresting_One
    * "It's umm...["]{pc_name}! Yeah, that's my name!" 
        "Awesome, always great to meet a new gob!
        -> Context_to_Situation
        



===Intresting_One===
    *"No, you're an intresting one!"
        "Wow, chill out dude. Look, look,  
        ->Context_to_Situation
    *"I wish..."
        "Damn! That was oddly depressing... Well,
        ->Context_to_Situation
    *"Aren't I here for a reason?"
        "Oh yeah, I almost forgot!
        ->Context_to_Situation
    
    
===Context_to_Situation
    <> I called the commisioner becasue someone or something keeps stealing my balls! How are gobs supossed to play a fun game if all their balls keep getting stolen." -Bill the Dwaggin' 
    *[WHAT?!] "What are you talking about!?"
        "Golf balls. What did you think I was talking about?" - Bill the Dwaggin' 
        "Umm.. nevermind."
        -> The_Need
        
    *[Balls!] "Awe dude thats discusting"!
        "What's so discusting about it. How'd you like it if I called your dying hobby discusting becasue something keeps stealing you're balls! Huh!" - Bill the Dwaggin' 
        
        **"What are you talking about?!"
            "My golf balls. What else would I be talking about?" -Bill the Dwaggin' 
            -> The_Need
        **"Dude you're weird." 
            "What's so weird about being mad about people stealing my golf balls. I think it's pretty fucked up personally."  
                ***"Dude golf balls![] Are we serious right now!"
                    ->The_Need
                ***"Ooohhhhh...[] That makes more sence!"
                "What did you think I meant?" - Bill the Dwaggin
                "Umm... nothing..."
                    ->The_Need
        
    *[Okay...] so what exactly is getting stollen? 
        "My golf balls! It's been pissing me off! 
        ->Pissing_me_off
        
        
    === Pissing_me_off ===
    <> People aren't even showing up too play anymore. It sucks!" 
        -> The_Need
    
    
    === The_Need === 
    
    *"Okay, intresting["], how can I help!"
        "I need my course to be thriving again. 
        -> Hole_17
    *"So... [Do you want them back?]Do you want them back?"
        "No, I really don't care about the lost ones anymore. It's just...
        -> Hole_17
    *"So... [What do you need me for?] what exactly do you need me for?" 
        "Well...
        -> Hole_17
        
    ===Hole_17===
    <> Everyone keeps saying that their balls are disappering in hole 17. How can you get all the way through 17 holes and not finsish the course. People aren't even showing up anymore! Can you help me figure out how to fix this?" - Bill the Dwaggin'
    *"I mean...["] I guess I can try." 
    ->Off_to_fix_it
    *"Hell yeah man! ["] Shouldn't be a problem at all!" 
    -> Off_to_fix_it
    *"Nah[], this seems like a lost cause." 
    -> Sad_Bill
    *{Pissing_me_off} "[You said that already] bro. Like I get it but like...
    ->Sad_Bill
    
    ===Off_to_fix_it
    "Awesome, I really need to figure out what keeps happening to all my balls!" - Bill the Dwaggin'
    
    *"Yeah...["] I got you. Well... I'll be off now!" 
    ->DONE
    *"You know how weird that sounds["]... Right?"
    "Huh? What do you mean?" - Bill the Dwaggin'
        **"Well...["] your balls and what not. It's just a little weird, ya know?"
        "What's so weird about my balls?" - Bill the Dwaggin'
        
            ***["Nevermind"]"You know what, nevermind"
            Well alrighty then mate! Go find my balls, I'll see you around! - Bill the Dwaggin'
            ->DONE
            
            ***"Well it's not your balls["], but they way you're saying it." 
            "I'm still confused." - Bill the Dwaggin'
            
                ****Nope[!], I'm done! I'll see you around Dwaggin' the Bill!
                "See you around 
                {not Intresting_One: 
                    <> {pc_name}! Thanks for you're help" - Bill the Dwaggin'
                -else:
                    <> dude. Thanks for you're help" - Bill the Dwaggin'
                }
                ->DONE
                
        **"Like...[Nevermind"] Acutally nevermind. Imma go now. 
            Well alrighty then mate! Go find my balls, I'll see you around! - Bill the Dwaggin'
            ->DONE
   
   
    ===Sad_Bill===
    Look, I know my balls aren't very important to others. But they mean everything to mean. I can't keep watching the course die out. PLease, can ya help me out just this once. - Bill the Dwaggin'
    
    *[Yeah!] "You know what, no problem Bill, I got you!
        Awe thank you so much gob! Now go find my balls, I'll see you around! - Bill the Dwaggin'
        ->DONE
        
    *"I mean...["] yeah I could help out." 
        Awe thank you so much gob! Now go find my balls, I'll see you around! - Bill the Dwaggin'
        ->DONE
        
    *"Im sorry["], I just can't help out with your balls right now."
        No, I understand. I'm disapointed you can't help me out with my balls but lifes gonna shove a driver up your ass sometimes and what can you do about it.
            **Sure???
            ->Author_Note_1
    
    
    
    
    ===Author_Note_1===
    
    Hey, it's the creator here. Normally, as a player, you could totally decied not to help out Bill. I don't know why you wouldn't, but I guess some people are just monsters. Yeah, I'm looking at you. However, for the purpose of this demo, this is the only currnet playable situation. Becasue of that, I'm going to send you back a little. Please enjoy, and just make friends with Bill. He's a pretty awesome guy once you get to know him. 
    By Now!
    *[Click Here]
    ->Sad_Bill
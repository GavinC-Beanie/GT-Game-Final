-> Start


////////////////////////////////////
////////////////////////////////////
//////////////////// Start of Intro

=== Start ===

A flower wiggles, then turns, and then wiggles again and before it knows, it's no longer a flower, it's a walking, breathing, talking goblin. The flower is you! 
//Comissioner Speaking//

//Go back in here and give this more description about whats going on so play testers can invision your vision ((((VERY IMPORTANT))))


"Oh! You're finally awake huh? Your lazy bumb was in the ground for weeks ya know! Something's going on over at Bill's, hurry up and go check it out! Any questions" -Commisioner

    * "Nope[!"], I'm all good!"
        "Ya, well you betta be after I watered you this much. Now get moving!" - Commisioner
        
        You traverse over to Bill's place, a rock? Maybe a giant Dome? You're not sure but Bill's waiting out front to greet you. 
        -> Bill.First_Meet
    * "Yeah...["] What's going--" 
        "Ya well that's too bad. Get movin'!" - Commisioner
       
        You traverse over to Bill's place, a rock? Maybe a giant Dome? You're not sure but Bill's waiting out front to greet you.
        ->Bill.First_Meet


===Bill=== 

=First_Meet
VAR pc_name = ""

~ pc_name = "{~Goober|Tooby|Ranchy|Poopermint|Tifbloob|RopRop}"

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
    ->After_Meeting_Bill
    *"You know how weird that sounds["]... Right?"
    "Huh? What do you mean?" - Bill the Dwaggin'
        **"Well...["] your balls and what not. It's just a little weird, ya know?"
        "What's so weird about my balls?" - Bill the Dwaggin'
        
            ***["Nevermind"]"You know what, nevermind"
            Well alrighty then mate! Go find my balls, I'll see you around! - Bill the Dwaggin'
            ->After_Meeting_Bill
            
            ***"Well it's not your balls["], but they way you're saying it." 
            "I'm still confused." - Bill the Dwaggin'
            
                ****Nope[!], I'm done! I'll see you around Dwaggin' the Bill!
                "See you around 
                {not Intresting_One: 
                    <> {pc_name}! Thanks for you're help" - Bill the Dwaggin'
                -else:
                    <> dude. Thanks for you're help" - Bill the Dwaggin'
                }
                ->After_Meeting_Bill
                
        **"Like...[Nevermind"] Acutally nevermind. Imma go now. 
            Well alrighty then mate! Go find my balls, I'll see you around! - Bill the Dwaggin'
            ->After_Meeting_Bill
   
   
    ===Sad_Bill===
    Look, I know my balls aren't very important to others. But they mean everything to mean. I can't keep watching the course die out. PLease, can ya help me out just this once. - Bill the Dwaggin'
    
    *[Yeah!] "You know what, no problem Bill, I got you!
        Awe thank you so much gob! Now go find my balls, I'll see you around! - Bill the Dwaggin'
        ->After_Meeting_Bill
        
    *"I mean...["] yeah I could help out." 
        Awe thank you so much gob! Now go find my balls, I'll see you around! - Bill the Dwaggin'
        ->After_Meeting_Bill
        
    *"Im sorry["], I just can't help out with your balls right now."
        No, I understand. I'm disapointed you can't help me out with my balls but lifes gonna shove a driver up your ass sometimes and what can you do about it.
            **Sure???
            ->Author_Note_1
    
    
    
    
    ===Author_Note_1===
    
    Hey, it's the creator here. Normally, as a player, you could totally decied not to help out Bill. I don't know why you wouldn't, but I guess some people are just monsters. Yeah, I'm looking at you. However, for the purpose of this demo, this is the only currnet playable situation. Becasue of that, I'm going to send you back a little. Please enjoy, and just make friends with Bill. He's a pretty awesome guy once you get to know him. 
    By Now!
    *[Click Here]
    ->Sad_Bill
    
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// End of Intro
    
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// Start of Movement Blocks
    ===After_Meeting_Bill===
    Bill walks away behind his... Dome? You're still not sure. It's time to start your detective work. There's a whole town to talk to though, just where will you start?
    
    *[The old dude sitting in his rocking chair]
        ->Crank
    *[The grandma selling pies out a street cart]
        ->Gram
    *[The gob seemingly slumped on the bench out side his tent]
        ->Pump
    *[The crazy looking gob over by the empty pool]
        ->Crazy_Skater
    
    
    ===Locations_after_Bill===    
    +[The old dude sitting in his rocking chair]
        ->TBD
    +[The grandma selling pies out a street cart]
        ->Gram
    +[The gob seemingly slumped on the bench out side his tent]
        ->TBD
    +[The crazy looking gob over by the empty pool]
        ->TBD
    
    
    ===Locations_after_Gram===
    +[The old dude sitting in his rocking chair]
        ->Crank
    +[The gob seemingly slumped on the bench out side his tent]
        ->Pump
    +[The crazy looking gob over by the empty pool]
        ->Crazy_Skater
    +{Didnt_Buy_Pie} [Could Bill be at his place] //This needes to be changed to didnt talk about Bill and Crank (not made yet)
        ->TBD //Bill shouldn't be there
    
    //Add one for Bill being there once you have a knot that explaines that Bill smokes with Crank
    
    ===Locations_after_Pump===    
    +[The old dude sitting in his rocking chair]
        ->TBD
    +[The grandma selling pies out a street cart]
        ->Gram
    +[The crazy looking gob over by the empty pool]
        ->TBD
    +[Could Bill be at his place] //This should have a check to see if PC has talked to gram about Bill and Crank
        ->TBD //Bill shouldn't be there
    
    //Add one for Bill if PC has talked to gram about BIll and Crank
    
     ===Locations_after_Crank===    
    +[The grandma selling pies out a street cart]
        ->Gram
    +[The gob seemingly slumped on the bench out side his tent]
        ->TBD
    +[The crazy looking gob over by the empty pool]
        ->TBD
    +[Could Bill be at his place] //This should have a check to see if PC has talked to gram about Bill and Crank
        ->TBD //Bill shouldn't be there
    
    //Add one for Bill if PC has talked to gram about BIll and Crank
    
    ===Locations_after_Skater===    
    +[The old dude sitting in his rocking chair]
        ->TBD
    +[The grandma selling pies out a street cart]
        ->Gram
    +[The gob seemingly slumped on the bench out side his tent]
        ->TBD
    +[Could Bill be at his place] //This should have a check to see if PC has talked to gram about Bill and Crank
        ->TBD //Bill shouldn't be there
    
    //Add one for Bill if PC has talked to gram about BIll and Crank
    
    
    
    //Blocks need to be added for Locations after Unicorn and Gomblar
    
    
    ===TBD===
    //Everything after meeting bill for the first time
    
    //Go back and add stiches to Bill's Knot
    :)
    
    ->Ending
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// End of Movement Blocks
    
    
    
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// Start of Gram
    
    
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
        ->Locations_after_Gram
    *"So...["] can I ask you a few questions now?
        No problem at all. <> 
        ->Talking_with_Gram
    *"There[!"], you happy!"
        "Yes, thank you so much honey. Anything else I can do for ya!" //First part snarky, second part nicer
        **"Acutally, yes!" 
        ->Talking_with_Gram
        **"Nah...["] I think I'm all good!"
        ->Locations_after_Gram
   
    
    
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
        
    
    
    //Do this after interactions with pumple and crank and scater
    
    //Shouldn't say much about pumple
    
    //Explain Bill and Crank smoking if prompted
    
    //Scater is just some kid
    
    ->Locations_after_Gram
    
    
    
    ===Didnt_Buy_Pie
    ->Locations_after_Gram
    
    
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// End of Gram  
    
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// Start of Crank
    
    ===Crank===
    
    The Old Gob is rocking in his chair with his eyes closed. His sagging skin slups over the edges of his mouth, his forehead wrickile putting small waves to shame. Wlist mid rocking, the rest of his body never moves. Almost as if, he was in an eternal slumber...
    
    *"Umm... Mister?"
        ->Off_my_porch
    *"Yo Grandpa!" 
        ->Off_my_porch
    *Leave and come back later
        ->Locations_after_Crank
    
    
    ===Off_my_porch===
    
    "Get off my porch! I said get off, get off! Get your goddamn floppers off my porch!" - Crank
    
    *"I just wanted to ask[?"]if you-"
        "I said get the hell off my porch! Get off, get off!" - Crank
        **"Oh my god but[?"] I just-" 
            "Off! Off my porch! I said off!" - Crank
            ***"Okay, Okay!" <>
        **"Okay, Okay!" <>
    *"Okay, Okay!" <>
        
    - Well that didn't work. Guess I got to try something else.
        ->Locations_after_Crank
    
    
    ///////////////////////////////////
    ////////////////////////////////////
    //////////////////// End of Crank
    
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// Start of Pump
    
    
    ===Pump===
    
    The beaten up gob sat sun bathing on his bench. Two milk crates held up his wooden two by four with a small carton spilled in front of him. He seemed to be out like a light. 
    
    *"Umm... escuse me sir?"
        ->Off_my_bench
    *"Yo you up!" 
        ->Off_my_bench
    *Leave and come back later
        ->Locations_after_Pump
    
    
    ===Off_my_bench===
    
    "Ahh, ahh get out of here! I don't know nothin' bout' nothin', now stop bothering me!" - Pumplscroob.. 
    
    *"I just wanted to ask[?"]if you-"
        "I told you I don't know nothin'. Now get! I said get!" 
        **"Look[!"] I just-" 
            "Get! I said get! I don't know nothin'! Get! Get!"
            ***"Okay, Okay!" <>
        **"Okay, Okay!" <>
    *"Okay, Okay!" <>
        
   - Alright... So that won't work. 
        ->Locations_after_Pump
    
    
    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// End of Pump  
    

    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// Start of Skater
    
    ===Crazy_Skater===
    
    A beaten up and partially dismembered gob sits at the top of a ramp convenetly placed at the edge of an empty pool. He has no helment, and might be missing a brain too. 
    
    "Hell yeahhh... let's rip this shit!" 
    
    Before you can even say anything, the gob drops down the ramp, comes up the other side of the pool getting huge air, but then comes down landind on the edge of the pool, his head geeting slit from his body turning into a splat from the contact. 
    
    *"Ummm..."
        ...
        **"Ohhh-kay"
        ->Skater_Dies
        **"What the fuck!" 
        ->Skater_Dies
        
    *"Excuse me?"
        ...
        **Probably could've expected that...
        ->Skater_Dies
        **"What the fuck!" 
        ->Skater_Dies
        
    *"What [the..."] in the fuck just happend!" 
        ...
        **"I mean[..."] I guess you can't answer..." 
        ->Skater_Dies
        **"What the fuck!"
        ->Skater_Dies
    
    
    ===Skater_Dies===
    
    "Well then... guess I got to come back another time. I mean... if there is another time. Did I just watch him die. Surely not right?!?! Ahh... I have better things to do." 
    ->Locations_after_Skater


    ////////////////////////////////////
    ////////////////////////////////////
    //////////////////// End of Skater






===Ending===
    
-> END


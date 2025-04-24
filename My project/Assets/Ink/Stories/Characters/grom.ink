
===Gromblar===

"Woah, woah! Stranger danger! Stranger danger!"

+"Woah, [chill out!"]calm down you freakazoid!"
    ->Bubble
+[Start Peeing]Ppssssssssssssss... (would be emote)
    ->Chiller
+"Nope[!"], can't do it!"
    ->DONE
    
    
===Bubble

"Ahhhhh! You must remove yourself from my personal bubble! Stranger danger! Stranger danger!

+[Start Peeing]Ppssssssssssssss... (would be emote)
    ->Chiller
+"Nope[!"], can't do it!"
    ->DONE
    
===Chiller

"Oh... You must be one cool chiller to pee yourself like that..."

+"You're a freak!"
    ->freak
+...
    ->what
+"Can...Can I just ask[..."]you some questions?"
    ->Questions_grom
    
===what

"What?"

+"You're a freak!"
    ->freak
+"Can...Can I just ask[..."]you some questions?"
    ->Questions_grom
    
    
===freak

"Woah, I thought you were a chiller!"

+"I am[, I am!"]... Promise, super chill! Just wanna ask a few questions."
    ->Questions_grom
    
+"Nevermind[..."], I'm chill or whatever. Just wanna ask a few questions.
    ->Questions_grom
    
    
 ===Questions_grom   
"Okay... sure. What's up!"

+[The golf course?]"What have you been up to on the Bill's golf course!?"
    ->grom_ex
+[The boat trips?]"Why have you been taking boat rides to Bill's golf course!?"
    ->grom_ex


===grom_ex

"Oh, theres this weird slim over there. When I touch it, my hand disapears! Isn't that so cool!"

+"Sure[..."], now continue!"
    ->grom_ex_cont
+"Awesome[..."], now keep going!"
    ->grom_ex_cont
    
    
    
===grom_ex_cont

"Well... I go over there to touch it. It's like a portal or something. It's pretty and pink too. That's all really..."

+"That's it[!"], that's the only reason!"
    "Yep!"
    ++"Okay[..."], whatever, thanks I guess."
    "No problem stranger!"
        ->DONE
+"Where is it?"
    "Oh it's in this little cup. Couldn't really tell you where though. I would have to show you. 
    ++"Nope[!"], I think I'm all good.
    "Wonderful! Bye Stranger!"
    ->DONE
    ++"Can't[!"], I already asked and the tent dweller seems to be busy."
    "Damn. Well bye then Stranger!"
    ->DONE


    

->DONE


VAR seen_derp = false
===Derpy===


"Ah! There we go!"

*{not seen_derp}"Woah!["] Are you a real unicorn!"
    ->Intro_derp

*{not seen_derp}"Okay...["] So now there's a unicorn... That checks."
    ->Intro_derp
 
*{seen_derp}"Okay... I know!["] I'm back."
    ->Whats_up_derp
    
===Intro_derp

"Yep, yep, the real thing butthead. Sorry I had to suck you in and all. Just a lot easier to talk in here than it is out there ya feel!"

*"So what is... here?"
    ->Here

*"Why is it so [pink?"]damn pink in here?!"
    ->Pink


===Here

"Oh here! This is my acid trip. Just made my own freak world... Pretty dope right! I just live here, bum out, have fun, eat pizza!"

*Okay...
    ->Whats_up_derp

*Alright, bet!
    ->Whats_up_derp


===Pink

"Cause why not bro! Let you're freak out a little. This is my acid word, welcome. I just live here, bum out, have fun, eat pizza!"

*Okay...
    ->Whats_up_derp

*Alright, bet!
    ->Whats_up_derp


===Whats_up_derp
~ seen_derp = true

"Well! What's up then?"

*"Right! [The questions!"]Can I ask you a few questions?"
    ->Derpy_Questions
    
*"Actually, I'm all good!["] How do I get out of here!"
    ->leave_derps




===Derpy_Questions

"Sure."

*"You know [about Bill's"]anything about Bill's golf course!"
    ->derpy_course
*["Freak?!"]"What is this 'freak' you speak of?"
    ->derpy_freak
*"What are those balls[?"] over there. Is there something in them?"
    ->derpy_balls
*"What's up with [the trash?]all the damn trash?"
    ->derpy_trash



===derpy_course
~ met_derpy = true
~ OnVariableChanged("met_derpy", "Derpy_Unicorn_Buttponey")
"Ha! I take shits in some of the holes everynow then. Great right! Can you belive that?"

*{met_grom}["Maybe?"] "Yeah... actually, I might..."
    "Woah! You're one detective arn't you!"
    **"Could I ask another?"
    ->Derpy_Questions
    **"Sure... I think im good here."
    ->leave_derps
*"Sure...Could I ask another?"
    ->Derpy_Questions
*"Sure... I think im good here."
    ->leave_derps
    
    
===derpy_freak

"Freak! Oh that's the grove! The rythm! That looseness of life... That shit that makes you feel like you're living!"

*"Oh hell yeah!"
    "Hell yeah!"
    **"Could I ask another?"
    ->Derpy_Questions
    **"I think im good here."
    ->leave_derps
*"Okay...Could I ask another?"
    ->Derpy_Questions
*"Huh... I think im good here."
    ->leave_derps



===derpy_balls
~ met_derpy = true
~ OnVariableChanged("met_derpy", "Derpy_Unicorn_Buttponey")
"Oh, those are Grumpl eggs. You'll see more of those if this game gets funding. Some of them haven't been hatching though... That's been weird."

*"Intresting...Could I ask another?"
    ->Derpy_Questions
*"Okay... I think im good here."
    ->leave_derps


===derpy_trash

"Ha! Sometimes I freak a little...  too much."

*"Huh...Okay...Could I ask another?"
    ->Derpy_Questions

*"Wha-..No. I think im good here."
    ->leave_derps



===leave_derps

"No problem, I'll just zap you out!"

->DONE

















ONE


















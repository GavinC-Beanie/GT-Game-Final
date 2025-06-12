

=== Com ===

"Oh! You're finally awake huh? Your lazy bumb was in the ground for weeks ya know! Something's going on over at Bill's, hurry up and go check it out! Any questions" -Commisioner

    * "Nope[!"], I'm all good!"
        "Ya, well you betta be after I watered you this much. Now get moving!"
        ~met_com = true
        ~ OnVariableChanged("met_com", "The_Commisioner")
        [END_DIALOGUE]
        ->DONE
        
    * "Yeah...["] What's going--" 
        "Ya well that's too bad. Get movin'!"
       ~met_com = true
       ~ OnVariableChanged("met_com", "The_Commisioner")
       [END_DIALOGUE]
        ->DONE
    
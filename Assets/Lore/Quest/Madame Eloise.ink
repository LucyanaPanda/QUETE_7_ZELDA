INCLUDE ../globals.ink

-> main

=== main ===
- Hi sweetie !
- Have you been doing well since the beginning of your journey ?
	+ [Yes]
		-- Glad to hear sweetie.
		-- { eloiseQuestCompleted == false : -> Quest | -> FinishQuest}
    + [No]
        -- Oh i’m sorry. Must be because you’re not feeling like you belong to the world.
        -- You’ll get your memories back soon enough.
        -- Go rest in my house for a little if you wish.
        -- -> END
        
=== Quest ===
I have a favor to ask you..
In the cave behind my house, there is a treasure my late grandfather left me.
This treasure is a necklace from my mother, the very last thing my grandfather held dear too.
Could you get it for me please ?
-> END

=== FinishQuest ===
Thank you for bringing it back to me. 
There is a rumor going around near the village.
Apparently, if you give an offrande to the divine status of the ancient temple, you'll be able to regain your old past life.
Maybe that could help you remember what happened to you.
Take care sweetie.
-> END
INCLUDE ../../globals.ink

-> LilBro

=== LilBro ===
Hey sis!
{ tommyQuestCompleted == true: -> FinishQuest | -> Quest }

=== Quest ===
Have you seen my toy somewhere around ?
I can't find it anymore.
Could you help me out please ?
+ [Yes, don't worry Tommy, we'll find it]
    -- -> END
+ [I got things to do, but i'll come back and help you out ]
    -- -> END

=== FinishQuest === 
Thank you sis !
This is the necklace I've been looking for.
-> END

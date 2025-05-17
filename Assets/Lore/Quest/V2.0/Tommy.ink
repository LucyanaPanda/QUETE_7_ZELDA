INCLUDE ../../globals.ink

-> LilBro

=== LilBro ===
Hey sis!
{ tommyQuestCompleted == 1: -> FinishQuest | -> Quest }

=== Quest ===
Have you seen my toy somewhere around ?
I can't find it anymore.
Could you help me out please ?
+ [Yes, don't worry Tommy, we'll find it]
    -- Thanks sis!
    -- -> END
+ [Right now, i can't ]
    -- Don't worry, it's okay.
    -- -> END

=== FinishQuest === 
Thank you sis !
This is the necklace I've been looking for.
-> END

INCLUDE ../../globals.ink

{ manQuestCompleted == 1: -> FinishQuest | -> Quest }
-> Quest

=== Quest ===
Gosh, I feel anxious whenever I'm around the "Bakery".
I can't understand why. I love pastries.
I wish I knew what's happening to me.
Do you have any ideas ? I am going crazy ?
+ No, you're not.
    -- Gosh, that reassures me.
    -- You can help me then.
    -- Get this weird forbidden feeling to go away.
    -- -> END
+ Yes, you are.
    -- Oh... My... God...
    -- Exorcise me please. It must be a demon's doing !
-> END

=== FinishQuest ===
Did you exorcise it ?
Thank you so much. 
Bless you. Amen.
-> END

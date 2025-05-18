INCLUDE ../../globals.ink

{ sourLadyQuestCompleted == 1: -> FinishQuest | -> Main }
-> Main

=== Main ===
Don't bother me. I need to be alone.
...
You can leave now.
* [What's wrong ?]
    -- I said don't bother me, i don't need you to coMfort me
    -- -> END
* [I won't leave until you feel better]
    -- Why so persisting ?
    -- Fine, do whatever you want.
    -- -> Quest

=== Quest ===
Fine, I've been in a bad mood for the last few days and I don't know why!
And I can't find the reason for it nor can I calm down.
It pisses me off more than I need.
-> END

=== FinishQuest ===
I can't explain how but i feel better after telling you all my problems.
Thank you... for insisting.
-> END

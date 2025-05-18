INCLUDE ../../globals.ink

-> Mother

//First Quest
=== Mother ===
Hey Honey!
Are you doing fine ?
 + [I'm doing fine mom, looking for little jobs]
    -- Oh great ! you'll earn experience and some pockets money.
    -- That's a good initiative.
    -- { motherQuestCompleted == 1: -> FinishQuest | -> QuestMother }
+ [It's okay, normal day] 
    -- Okay, seems like your day is going tooooo smooth.
    -- {motherQuestCompleted == 1: -> FinishQuest | -> QuestMother }
    
=== QuestMother === 
Look, i need some help, can you collect all the carrots in the garden ?
Miss Patiro wants to buy some but I'm a little overwhelmed from work so can you please help me ?
+ [Yes, absolutely mom]
    -- Good, thank you honey
    -- -> END

=== FinishQuest ===
Thanks again Honey.
You were a great help.
Go enjoy your day.
-> END

INCLUDE ../../globals.ink

-> Mother

//First Quest
=== Mother ===
From Mother:
Hey Honey, how are ya ?
 + [I'm doing fine mom, looking for little jobs]
    -- Oh great ! you'll earn experience and some pockets money.
    -- That's a good initiative.
    -- { motherQuestCompleted == true: -> FinishQuest | -> QuestMother }
    -- -> QuestMother
+ [It's okay, normal day] 
    -- -> Side
        
=== Side ===
Okay, seems like your day is going tooooo smooth.
-> QuestMother
    
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

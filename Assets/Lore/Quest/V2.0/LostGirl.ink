INCLUDE ../../globals.ink
{ lostGirlQuestCompleted == true: -> FinishQuest | -> LostGirl}

=== LostGirl ===
Hello miss!
I'm looking for the bakery but I got lost.
-> Choices

=== Choices ===
Would you mind telling me the way ?
    * [Go to the left and keep straigth]
        -- Are you sure ? 
        -- This way leads to the park, not the bakery
        -> Choices
    * [You're already here]
        -- But...
        -- That's the police station right behind me
        -> Choices
    * [Left then right then left until you find the bakery]
        -- Thank you miss !
        -- ~ lostGirlQuestCompleted = true // Bind it to globals.ink
        -- Here is something for you !
        --> END
    
=== FinishQuest === 
Thank you miss !
-> END

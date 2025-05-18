INCLUDE ../../globals.ink

{ RgrandmaQuestCompleted == 1: -> FinishQuest | -> Quest }

=== Quest ===
Hello ... ?
What's up ?
+ [Your grandma's looking for you at home.]
    -- Really ? Thanks for telling.
    -- I should head back soon.
    // -- ~ RgrandmaQuestCompleted = 1

    -- ->END 
+ [Just looking at you.]
    -- ...
    -- -> Tentative2
    -- ->END


=== Tentative2 ===
Okayy, weirdo
Are there any problems ?
+ [You're grandma is looking for you.]
    // -- ~ RgrandmaQuestCompleted = 1
    -- Ohh ookay.
    -- Well thank you for telling me. By the way, if you want to start a conversation.
    -- Try with words. It works better than just looking at people.
    -- Bye.
    -- -> END
    
=== FinishQuest ===
Thank you.
I will head back soon to my grandma
-> END

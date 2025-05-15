INCLUDE ../../globals.ink

{ RgrandmaQuestCompleted == true: -> FinishQuest | -> Quest }

=== Quest ===
What's up ?
+ Your grandma's looking for you at home.
    -- Really ? Thanks for telling.
    -- I should head back soon.
    -- ->END 
+ Just looking at you.
    -- -> FinishQuest
    -- ->END


=== FinishQuest ===
~ RgrandmaQuestCompleted = true
Okayy, weirdo
Are there any problems ?
+ You're grandma is looking for you.
    -- Ohh ookay.
    -- Well thank you for telling me. By the way, if you want to start a conversation.
    -- Try with words. It works better than just looking at people.
    -- Bye.
    -- -> END

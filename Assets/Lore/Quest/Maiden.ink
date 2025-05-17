INCLUDE ../globals.ink

// { maidenQuestCompleted == false : -> Main | -> FinishQuest }

=== Main ===
Hey you! 
Are you going to the ancient temple ?
I've heard that when you offer something to the temple.
It will grant you a wish.
But there're monsters all the way to the temple.
-> Quest

=== Quest ===
Can you kill them for me ?
* [I accept]
    -- Okay cool.
    -- Come back to me if you've finished.
    -- -> END
* [No, kill them by yourself]
    -- Are you sure ? I'll be a great help if you do. Even more if you planned to go to the temple.
    -- Soooooo....
    -- -> Quest

=== FinishQuest ===
You're back, meaning you've kill them.
Thanks a lot.
I'll tell a secret. You have to solved twos enigmas inside before accessing to the last room where you can ask for a wish.
One of the enigmas needs  to be placed in order.
Remember this, the right order going from the left to right is:
Left,  Top, Left, Right, Left 
-> END
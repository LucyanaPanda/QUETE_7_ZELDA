INCLUDE ../globals.ink

{ TalkOnceToFirendlyMonster == 1: -> WelcomeBack | -> Start }

=== Start ===
Hey ! You yes you! 
You can see me right ?
+ [Yes I can]
    -- -> Continue
+ [...]
    -- -> Silence

=== Continue ===
OMG that's a first, i can talk to somebody !
It's been 500 years since i had a conversation.
You know, back in my days, we never had stores and parcs this modern.
"Blablabla"
+ [Leave]
    -- Wait!
    -- -> END
    
=== Silence ===
Hey! don't hang around like you don't see me.
I know you do.
+ [Leave]
    -- Hey, don't leave me!
    -- -> END
    
=== WelcomeBack ===
Hey, you're back !
Wanna talk ?
+ [With a cup of tea]
    -- Great !
    -- -> WannaTalk
+ [Sorry, later maybe]
    -- Later ther !
    -- -> END
    
=== WannaTalk ===
You know, i've never had a friend since I die.
Everyone either don't see me or wants to torments others.
I don't like to torment.
I tried once on a child. It cried like crazy.
I'm traumatized forever.
-> END
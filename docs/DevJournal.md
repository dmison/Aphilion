
## Week 1

Intending to continue working on Souls-like, but likely rebuilding it.

Leaning heavily on this tutorial series that I found on Monday.  It's a very long series for an Elden Ring implementation so there are a lot of features that I don't need and can skip.  Likewise it is a multi-player implementation so I need to work around that as well.  Interesting learning a little bit about Unity NetCode though.

A key reason I am referring to this tutorial instead of just extending the original Terminus code is having a better implementation of the character controller, especially in regards to animation & using root animation to drive some movement but not all.


I'll be using the Synty Dark Fantasy,  Locomotion animation & Combat Animation asset packs.

They are missing a couple of things like "using a lever", "drink potion", "open door" animations but should be easy to implement.

The "dodge back" animation needs it's root motion adjusted as it isn't much of a dodge, seems like less than a meter and could stand to be double that.  I had a brief look at editing it in Unity and it worked but would slide back to the original unedited point at the end.  Obviously missing something.

Have completed up to the stamina implementation.  Mine varies a little based on what I learned from Terminus.

Another deviation from the tutorial is that I am using UIToolkit instead of Unity UI.  This is mostly because I am very familiar with web development and it is more intuitive to me.

---

### Pitch thoughts

- 3rd person action RPG
- focus on deep combat that is "tough but fair"
    - blocking
    - stance breaking
    - poise
    - attacks vary per weapon

- non-intrusive narrative
    - item descriptions
    - NPC dialog
    - environmental storytelling

- regions are heavily interconnected with unlockable shortcuts

- combined character XP/currency
    - dropped on death

- bonfire/lantern spawn points
    - rest at:
        - refills flasks
        - resets enemies in region

- 3 "play phases"
    - exploring new area
        - expect to die
        - learning area, enemies etc
        - trying to find new bonfires & shortcuts
    - boss attempts
    - area runs
        - practicing
        - trying new weapons/tactics
        - farming XP
        - area leading up to boss (especially runbacks) should be aiming to teach you the things you need for the boss & give opportunity to practice those things

- upgrading
    - character stats
    - weapons
    - items
    - armour

- potion/flask
  - 
    - fixed number refilled at bonfire
    - can be upgraded
        - number of uses
        - amount healed

## Week 2 (Mon Jul 27 - Sun Aug 2)

Okay on a team of one.  this means I have to focus my scope.

Hopefully can get a contractor for animation.

I want to get combat & AI in.  Keep AI simple based on Terminus code.  Stretch for "plan AI".

Level design needs to be established early I think.

Thoughts: I want to make the 3 phases of play obvious to players.  Not sure how, maybe NPC dialog overtime based on progress/actions ?

- movement
- combat
- AI
- environment
- interactables?  ladders, doors, bonfire, elevators

Health & damage is done.  I like the system of "Character Effects" scriptable objects for damage as a means of bundling up damage numbers, visual effects,  audio, animation etc.

Part way into the weapons code

## Week 3 (Mon Aug 3 - Sun Aug 9)

4th: character effects foundation & resizing stat bars
5th: basic death

story/theme ideas:
- it's just capitalism:
    - nothing specific storywise
    - each region is an abstraction of a different problem in capitalism
    - each major boss is representative of a particular stereotype
- queer time / trans grief
    - the world has shattered into different shards of time that do not align with each other anymore
    - the villain has caused this to happen by travelling back in time to change events
    - "firelink shrine/hunters dream" area could be a tiny pocket of stable time of the original timeline
    - you are an individual who has the unique ability to travel safely between the shards
    - travelling through each shard, defeating bosses that control each in order to make it to the final boss & defeat them & restore the timeline
    - reveal:
        - the villain is you ... well one version of you
        - unable to deal with the sense of loss of the childhood they were denied the villain figures out how to time travel & change it
        - doesn't work properly & messes things up so they keep trying, which makes the world increasingly broken
        - very The Butterfly Effect
        - each of the bosses are very different versions of the same character as well but all from very different timelines
    - maybe the characters who help you in the "stable time pocket" actually know the truth and they are concealing it from you.
## Week 4 (Mon Aug 10 - Sun Aug 16)

Contractors will not be available.

10th: collision improvements
10th: foundations for weapons & items, inventory
12th: weapon action foundations
12th: item action foundations
15th: audio system basics, roll audio, hit audio, impact FX, target lockon, skeleton character

## Week 5 (Mon Aug 17 - Sun Aug 23)

- 19th: lock-on system done.  no ui though.
 - 20th: outline shader for lock target?  https://www.youtube.com/watch?v=Bm6Bmcjd1Mw
outline shader is not great with synty models - would have to do weirdness with normal smoothing, but baking into vertex colours & shenanigans in the shader.  a black outline is really nice though.  Will do the "unsmoothed" version now and look into smoothing later.  I only have to do it to the enemy models so probably only 4 or 5.

Organising tasks for sprints.  I think I'm going to organise into 2 week sprints.  will make it easier for me to progress with smaller units of work

still lots of combat stuff that can be implemented, but I'm going to start the AI work in sprint 3 and get the basics done so the combat loop is complete

 - 23rd: I started on the AI stuff and I've started refactoring some of the movement mechanics, mainly falling, back into base character manager class instead of the player manager.  It's fiddly and maybe I should just duplicate those things in the ai character manager to start with & refactor into the base class as opportunity arises.

# Sprint 3

- AI characters
    - [x] state machine
    - [x] idle
    - [x] pursue
    - [x] combat stance
    - [x] attack action

## Week 6 (Mon Aug 24 - Sun Aug 30)

- [x] fix AI movement [due:: 2026-08-28] [completed:: 2026-08-31]

Concept: when player dies a pool spreads out under them, like a pool of blood but it doesn't have to be blood, which the body then sinks into, and then they climb back out of a corresponding pool of liquid at the spawn point.  Was initially imagining a black liquid like oil, or perhaps a well for the spawn point that they climb out of The Ring style. The oil makes me think of a character who has been corrupted by pollution ala The Toxic Avenger or even Godzilla.

Considering the idea of multiple characters like in Borderlands although I could only implement one in this timeframe, I could demonstrate it with placeholder UI for character selection.  Having each character have their unique spins on different core mechanics based on how the events in the backstory of the game has affected them is interesting.  Maybe different "bonfires"?  Each area of the game having to have different "bonfires" for each character in different places would mean that each would experience that region differently.  That would require a lot of level design work, or at least a strategy to accommodate the idea in design.

In co-op play that would mean each player would have different spawn points.

Going back to the pollution idea: each character gets their powers as a result of different things that have destroyed the environment.

Each character has their own starting area & unique path through the game.  Each character would also travel through the other starting areas, but at different times in their playthough and with different things happening.  That would make the different spawn points more reasonable in terms of design.

The starting area for each would reflect their individual circumstances & effect on them.

What core other mechanics could change?  Different vendors, different XP currency?

Aside from specific character abilities that each would have.

I don't like the idea of having them fulfil different gameplay roles ... they should each be customisable to do whatever you want.  So if you like the tanky role you can play each ever you want not just one.  Ideally also have multiple gender & race options each each as well.

What would be the destruction factors?
1. carbon pollution - oil
2. radioactive waste
3. plastics?
4. mono-culture ecology, species extinction
5. chemical/fertilizer run-off
6.

These would be driving forces for character & environment design

Also damage types / resistances

Or more systemic things / ideologies that can be destructive:
- Capitalism
- Fascism
- Individualism
- Racism
- Sexism

All the isms

## Week 7 (Mon Aug  31 - Sun Sep 6)

- Been spinning my wheels on this
- the AI character in-place rotation animations is annoying as hell so I'm back burnering that
- Reference: Quaternions explained visually in 381 seconds https://www.youtube.com/watch?v=MpyGVvC-13s
- got AI in-place rotation working without animation.  This can be good enough for now.
- continuing AI combat stance & attacks
- started putting more ideas down in diagrams

# Sprint 4

- [ ] AI damage dealing

- sound effects
    - [ ] weapon whoosh
    - [ ] player take damage grunt
    - [ ] skeleton idle/chase/attack sound
- [ ] weapon trail effect
 
- [ ] bonfire/well respawn on death
- [ ] decide on respawn point model / name / concept

- [ ] AI management/spawning
- [ ] clean menu/world/region scenes & management
- [ ] game save/load
- [ ] basic UI for start screen, new game, load game etc

- [ ] input queuing
- [ ] weapon switching & UI
- [ ] blocking
- [ ] enemy healthbars
- [ ] decide on a set of weapons to include
- [ ] two-handed weapons
- [ ] weapon-based animation controllers (animation overrides)
- [ ] interact-able objects
- [ ] poise
- [ ] weapon combos

## Week 8 (Mon Sep  7 - Sun Sep 13)

- finalising initial AI attacks, damage dealing

- planning
    - add an effect of a spreading pool of liquid under the dead player body
    - when respawn is implemented, pool of similarly coloured liquid that you crawl out of.  maybe a well of some sort.



## Week 9 (Mon Sep  14 - Sun Sep 20)


# Sprint 5

- [ ] item pick ups
- [ ] equipment UI
- [ ] stance breaking
- [ ] riposte
- [ ] backstab
- [ ] flasks

- [ ] runes
- [ ] level up
- [ ] revive & reclaim runes

- [ ] dual wielding/power stance
- [ ] breakable objects
- [ ] elevators
- [ ] doors
- [ ] ladders

## Week 10 (Mon Sep  21 - Sun Sep 27)

## Mid Trimester Break (Mon Sep  28 - Sun Oct 4)


# Sprint 6

- [ ] finalizing level designs
- [ ] 

## Week 11 (Mon Oct  5 - Sun Oct 11)

## Week 12 (Mon Oct  12 - Sun Oct 18)

# Cleanup
## Week 13 (Mon Oct  19 - Sun Oct 25)

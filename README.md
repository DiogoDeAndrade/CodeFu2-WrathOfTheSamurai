# CodeFu 2 - Wrath of the Samurai

Diogo de Andrade
ULHT
------------------------------

## Description

This game is the companion for the presentation "Code Fu 2 - Wrath of the Samurai", a live coding session in which I build an extremely simple stealth game in less than one hour.
The goals of the session are:
- Have the ninja moving and animating properly
- Have a dynamic camera that avoids obstacles and follows the player
- Have droids that patrol some waypoints and attack the player if they find him

## Initial State
The first push of this repository is the initial state of the game:
- All packages imported
-- TextMeshPro
-- Cinemachine
-- Lightweigth Render Pipeline
-- Shader Graph
- All assets needed
-- Ninja model from Mixamo
-- Japanese Lamp from Gabriel Guimarães (Asset Store)
-- Droid from Adequate (Asset Store)
-- Textures from John's Junkyard (Asset Store)
- Title screen scene and logic (because it's a good place to put the credits)
- All the scripts that handle the UI
- Prefabs for enemies and entities, without any scripts, colliders, animators, etc... Basically, just the graphics for them
- Level layout and waypoints

## What worked

- Most of the "game" works (even if it took plus 30 minutes than expected)

## What could be added

- There is no collision detection on the enemies' lasers, so they can go through walls
- There is no end-condition (goal does not work)

## Conclusion

- It was an interesting live-coding session. The game took longer than expected, explanations took some valuable time, but they are an integral part of the experience.
- Future Code-Fu's might focus on smaller subsystems maybe...
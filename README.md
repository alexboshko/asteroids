Asteroids
=========
This is a clone of classical Asteroids game. The project was created mainly for demonstration purposes

There are builds for MacOS X and Windows in "Builds" folder.

Project is created in Unity 4.5.4 version.

Third-party libraries & assets I've used:
- NGUI
- DOTween
- Basically, all models and vfx except jet propulsions on player ship. Latter was created by me from scratch using standard texture from Unity
- Sounds

Things created by me:
- All scripts outside "third_party" folder
- All "custom" shaders. They aren't doing anything fancy, however
- For what it worth, few minor tweaks of StarField & Explosions vfx (basically scale, colours, emission intensity, etc)

Disclaimer
==========
I've tried to implement flexible architecture for hypothetical future development of the game. However, some thing are deliberately done in a way you hardly can expect to see in a fully fledged project, such as:

- String constants are hardcoded if they are used only once;
- Sometimes you might see a magic numbers constants, mainly in animations code;
- All game-play tweaking values are available as a public variables in corresponding prefabs in contrast of being in xml config;
- There are few sounds in the game, but there are no dedicated sound management system to handle them;
- Component caching is mostly not implemented (e.g. transform)

These simplifications are made for the sake of code simplicity and for saving development time.

Thanks,
Alex

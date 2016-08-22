![Eazy Sound Manager](http://i67.tinypic.com/sw7llf.png)

**Eazy Sound Manager** is a simple **Unity3D tool** which aims to make sound and music management in games easier. Playing a single audio clip is now as easy as calling one API function. The API can handle multiple music, game and UI sound effects at the same time while still giving you the option to interrupt previous audio clips when needed. Audio clips can be one shot, or looping.

Moreover, Eazy Sound Manager has the option to make music persist through multiple scenes, as well as add fade in/out transitions. Different global settings for music, game sound effects and UI sound effects are also implemented. However, eash audio has its own volume setting which is always relative to its global volume.

## Features
- No setup needed
- Simple API
- Play multiple audio clips
- Play music, game sound effects and UI sound effects
- Play/Stop/Pause/Resume all or individual audio clips
- Loop music
- Fade in and fade out transitions
- Global volume settings
- Music persistance across scenes

## Installation
No setup is needed. Just place the SoundManager.cs script somewhere inside your assets folder, and you are ready to go.

**DO NOT ATTACH THE SCRIPT ON ANY GAMEOBJECT**

## Usage
SoundManager.cs is a singleton class that will handle everything for you. You can play three types of audio: Music, game sound effects and UI sound effects. Each one is played using a different function call. 

####Play Music
Play background music
```c#
SoundManager.PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds)
```
Parameters

| Name| Type| Description
| --- |---| ---
| `clip`| AudioClip| The audio clip to play|
| `volume`| float| The volume the music will have|
| `loop`| bool| Wether the music is looped|
| `persist`| bool| Whether the audio persists in between scene changes|
| `fadeInSeconds`| float| How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)|
| `fadeOutSeconds`| float| How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)|
| `currentMusicfadeOutSeconds`| float| How many seconds it needs for all music audio to fade out. It will override  their own fade out seconds. If -1 is passed, all music will keep their own fade out seconds|

####Play Sound
Play a sound fx
```c#
SoundManager.PlaySound(AudioClip clip, float volume)
```

Parameters

| Name| Type| Description
| --- |---| ---
| `clip`| AudioClip| The audio clip to play|
| `volume`| float| The volume the sound will have|

####Play UI Sound
Play a UI sound fx
```c#
SoundManager.PlayUISound(AudioClip clip, float volume)
```

Parameters

| Name| Type| Description
| --- |---| ---
| `clip`| AudioClip| The audio clip to play|
| `volume`| float| The volume the UI sound will have|

## Authors
- Jack Hadjicosti (https://github.com/JackM36)
- Andreas Andreou (https://github.com/AndAndreou)

## License
MIT License. Copyright 2016 Jack Hadjicosti.

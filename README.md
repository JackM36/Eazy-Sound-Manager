![Eazy Sound Manager](http://i67.tinypic.com/sw7llf.png)

**Eazy Sound Manager** is a simple **Unity3D tool** which aims to make sound and music management in games easier. Playing a single audio clip is now as easy as calling one API function. The API can handle multiple music, game and UI sound effects at the same time while still giving you the option to interrupt previous audio clips when needed. Audio clips can be one shot, or looping.

Moreover, Eazy Sound Manager has the option to make music persist through multiple scenes, as well as add fade in/out transitions. Different global settings for music, game sound effects and UI sound effects are also implemented. However, each audio has its own volume setting which is always relative to its global volume.

## Features
- No setup needed
- Simple API
- Play multiple audio clips
- Play music, game sound effects and UI sound effects
- Play/Stop/Pause/Resume all or individual audio clips
- Loop music
- Fade in and fade out transitions
- Global volume settings
- Music persistence across scenes

## Installation
No setup is needed. Just place the SoundManager.cs script somewhere inside your assets folder, and you are ready to go.

**DO NOT ATTACH THE SCRIPT ON ANY GAMEOBJECT**

## Usage
SoundManager.cs is a singleton class that will handle everything for you. You can play three types of audio: Music, game sound effects and UI sound effects. Each one is played using a different function call. All audio can be retrieved, stopped, paused or resumed.

Read the [wiki](https://github.com/JackM36/Eazy-Sound-Manager/wiki) for more information.

## Authors
- Jack Hadjicosti (https://github.com/JackM36)
- Andreas Andreou (https://github.com/AndAndreou)

## Donate
Eazy Sound Manager is a free asset done in my free time. However, donations helps me improve it, as well as develop and publish more projects like this.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=GGTKA37Z7TBTE)

## License
MIT License. Copyright 2016 Jack Hadjicosti.

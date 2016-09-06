using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EazyTools.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance = null;
        private static float vol = 1f;
        private static float musicVol = 1f;
        private static float soundsVol = 1f;
        private static float UISoundsVol = 1f;

        private static Dictionary<int, Audio> musicAudio;
        private static Dictionary<int, Audio> soundsAudio;
        private static Dictionary<int, Audio> UISoundsAudio;

        private static bool initialized = false;

        private static SoundManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (SoundManager)FindObjectOfType(typeof(SoundManager));
                    if (_instance == null)
                    {
                        _instance = (new GameObject("EazySoundManager")).AddComponent<SoundManager>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Global volume
        /// </summary>
        public static float globalVolume { get; set; }

        /// <summary>
        /// Global music volume
        /// </summary>
        public static float globalMusicVolume { get; set; }

        /// <summary>
        /// Global sounds volume
        /// </summary>
        public static float globalSoundsVolume { get; set; }

        /// <summary>
        /// Global UI sounds volume
        /// </summary>
        public static float globalUISoundsVolume { get; set; }

        void Awake()
        {
            instance.Init();
        }

        void OnLevelWasLoaded(int level)
        {
            List<int> keys;

            // Stop and remove all non-persistent music audio
            keys = new List<int>(musicAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = musicAudio[key];
                if (!audio.persist && audio.activated)
                {
                    Destroy(audio.audioSource);
                    musicAudio.Remove(key);
                }
            }

            // Stop and remove all sound fx
            keys = new List<int>(soundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = soundsAudio[key];
                Destroy(audio.audioSource);
                soundsAudio.Remove(key);
            }

            // Stop and remove all UI sound fx
            keys = new List<int>(UISoundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = UISoundsAudio[key];
                Destroy(audio.audioSource);
                UISoundsAudio.Remove(key);
            }
        }

        void Update()
        {
            List<int> keys;

            // Update music
            keys = new List<int>(musicAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = musicAudio[key];
                audio.Update();

                // Remove all music clips that are not playing
                if (!audio.loop && !audio.playing)
                {
                    Destroy(audio.audioSource);
                    musicAudio.Remove(key);
                }
            }

            // Update sound fx
            keys = new List<int>(soundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = soundsAudio[key];
                audio.Update();

                // Remove all sound fx clips that are not playing
                if (!audio.loop && !audio.playing)
                {
                    Destroy(audio.audioSource);
                    soundsAudio.Remove(key);
                }
            }

            // Update UI sound fx
            keys = new List<int>(UISoundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = UISoundsAudio[key];
                audio.Update();

                // Remove all UI sound fx clips that are not playing
                if (!audio.loop && !audio.playing)
                {
                    Destroy(audio.audioSource);
                    UISoundsAudio.Remove(key);
                }
            }
        }

        void Init()
        {
            if (!initialized)
            {
                musicAudio = new Dictionary<int, Audio>();
                soundsAudio = new Dictionary<int, Audio>();
                UISoundsAudio = new Dictionary<int, Audio>();

                initialized = true;

                DontDestroyOnLoad(this);
            }
        }

        /// <summary>
        /// Retrieves the Audio that has as its id the audioID if one is found, returns null if no such Audio is found
        /// </summary>
        /// <param name="audioID">The id of the Audio to be retrieved</param>
        /// <returns>Audio that has as its id the audioID, null if no such Audio is found</returns>
        public static Audio GetAudio(int audioID)
        {
            Audio audio;

            audio = GetMusicAudio(audioID);
            if (audio != null)
            {
                return audio;
            }

            audio = GetSoundAudio(audioID);
            if (audio != null)
            {
                return audio;
            }

            audio = GetUISoundAudio(audioID);
            if (audio != null)
            {
                return audio;
            }

            return null;
        }

        /// <summary>
        /// Returns the music Audio that has as its id the audioID if one is found, returns null if no such Audio is found
        /// </summary>
        /// <param name="audioID">The id of the music Audio to be returned</param>
        /// <returns>Music Audio that has as its id the audioID if one is found, null if no such Audio is found</returns>
        public static Audio GetMusicAudio(int audioID)
        {
            List<int> keys = new List<int>(musicAudio.Keys);
            foreach (int key in keys)
            {
                if (audioID == key)
                {
                    return musicAudio[key];
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the sound fx Audio that has as its id the audioID if one is found, returns null if no such Audio is found
        /// </summary>
        /// <param name="audioID">The id of the sound fx Audio to be returned</param>
        /// <returns>Sound fx Audio that has as its id the audioID if one is found, null if no such Audio is found</returns>
        public static Audio GetSoundAudio(int audioID)
        {
            List<int> keys = new List<int>(soundsAudio.Keys);
            foreach (int key in keys)
            {
                if (audioID == key)
                {
                    return soundsAudio[key];
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the UI sound fx Audio that has as its id the audioID if one is found, returns null if no such Audio is found
        /// </summary>
        /// <param name="audioID">The id of the UI sound fx Audio to be returned</param>
        /// <returns>UI sound fx Audio that has as its id the audioID if one is found, null if no such Audio is found</returns>
        public static Audio GetUISoundAudio(int audioID)
        {
            List<int> keys = new List<int>(UISoundsAudio.Keys);
            foreach (int key in keys)
            {
                if (audioID == key)
                {
                    return UISoundsAudio[key];
                }
            }

            return null;
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip)
        {
            return PlayMusic(clip, 1f, false, false, 1f, 1f, -1f);
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, float volume)
        {
            return PlayMusic(clip, volume, false, false, 1f, 1f, -1f);
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="loop">Wether the music is looped</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, bool loop, bool persist)
        {
            return PlayMusic(clip, 1f, loop, persist, 1f, 1f, -1f);
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <param name="loop">Wether the music is looped</param>
        /// <param name = "persist" > Whether the audio persists in between scene changes</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist)
        {
            return PlayMusic(clip, volume, loop, persist, 1f, 1f, -1f);
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="loop">Wether the music is looped</param>
        /// <param name="persist"> Whether the audio persists in between scene changes</param>
        /// <param name="fadeInValue">How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)</param>
        /// <param name="fadeOutValue"> How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds)
        {
            return PlayMusic(clip, 1f, loop, persist, fadeInSeconds, fadeOutSeconds, -1f);
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <param name="loop">Wether the music is looped</param>
        /// <param name="persist"> Whether the audio persists in between scene changes</param>
        /// <param name="fadeInValue">How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)</param>
        /// <param name="fadeOutValue"> How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds)
        {
            return PlayMusic(clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f);
        }

        /// <summary>
        /// Play background music
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <param name="loop">Wether the music is looped</param>
        /// <param name="persist"> Whether the audio persists in between scene changes</param>
        /// <param name="fadeInValue">How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)</param>
        /// <param name="fadeOutValue"> How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)</param>
        /// <param name="currentMusicfadeOutSeconds"> How many seconds it needs for all music audio to fade out. It will override  their own fade out seconds. If -1 is passed, all music will keep their own fade out seconds</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds)
        {
            if (clip == null)
            {
                Debug.LogError("Sound Manager: Audio clip is null, cannot play music", clip);
            }

            instance.Init();

            // Stop all current music playing
            StopAllMusic(currentMusicfadeOutSeconds);

            // Create the audioSource
            AudioSource audioSource = instance.gameObject.AddComponent<AudioSource>() as AudioSource;
            Audio audio = new Audio(Audio.AudioType.Music, audioSource, clip, loop, persist, volume, fadeInSeconds, fadeOutSeconds);

            // Add it to music list
            musicAudio.Add(audio.audioID, audio);

            return audio.audioID;
        }

        /// <summary>
        /// Play a sound fx
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlaySound(AudioClip clip)
        {
            return PlaySound(clip, 1f);
        }

        /// <summary>
        /// Play a sound fx
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlaySound(AudioClip clip, float volume)
        {
            if (clip == null)
            {
                Debug.LogError("Sound Manager: Audio clip is null, cannot play music", clip);
            }

            instance.Init();

            // Create the audioSource
            AudioSource audioSource = instance.gameObject.AddComponent<AudioSource>() as AudioSource;
            Audio audio = new Audio(Audio.AudioType.Sound, audioSource, clip, false, false, volume, 0f, 0f);

            // Add it to music list
            soundsAudio.Add(audio.audioID, audio);

            return audio.audioID;
        }

        /// <summary>
        /// Play a UI sound fx
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayUISound(AudioClip clip)
        {
            return PlayUISound(clip, 1f);
        }

        /// <summary>
        /// Play a UI sound fx
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <param name="volume"> The volume the music will have</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayUISound(AudioClip clip, float volume)
        {
            if (clip == null)
            {
                Debug.LogError("Sound Manager: Audio clip is null, cannot play music", clip);
            }

            instance.Init();

            // Create the audioSource
            AudioSource audioSource = instance.gameObject.AddComponent<AudioSource>() as AudioSource;
            Audio audio = new Audio(Audio.AudioType.UISound, audioSource, clip, false, false, volume, 0f, 0f);

            // Add it to music list
            UISoundsAudio.Add(audio.audioID, audio);

            return audio.audioID;
        }

        /// <summary>
        /// Stop all audio playing
        /// </summary>
        public static void StopAll()
        {
            StopAll(-1f);
        }

        /// <summary>
        /// Stop all audio playing
        /// </summary>
        /// <param name="fadeOutSeconds"> How many seconds it needs for all music audio to fade out. It will override  their own fade out seconds. If -1 is passed, all music will keep their own fade out seconds</param>
        public static void StopAll(float fadeOutSeconds)
        {
            StopAllMusic(fadeOutSeconds);
            StopAllSounds();
            StopAllUISounds();
        }

        /// <summary>
        /// Stop all music playing
        /// </summary>
        public static void StopAllMusic()
        {
            StopAllMusic(-1f);
        }

        /// <summary>
        /// Stop all music playing
        /// </summary>
        /// <param name="fadeOutSeconds"> How many seconds it needs for all music audio to fade out. It will override  their own fade out seconds. If -1 is passed, all music will keep their own fade out seconds</param>
        public static void StopAllMusic(float fadeOutSeconds)
        {
            List<int> keys = new List<int>(musicAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = musicAudio[key];
                if (fadeOutSeconds > 0)
                {
                    audio.fadeOutSeconds = fadeOutSeconds;
                }
                audio.Stop();
            }
        }

        /// <summary>
        /// Stop all sound fx playing
        /// </summary>
        public static void StopAllSounds()
        {
            List<int> keys = new List<int>(soundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = soundsAudio[key];
                audio.Stop();
            }
        }

        /// <summary>
        /// Stop all UI sound fx playing
        /// </summary>
        public static void StopAllUISounds()
        {
            List<int> keys = new List<int>(UISoundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = UISoundsAudio[key];
                audio.Stop();
            }
        }

        /// <summary>
        /// Pause all audio playing
        /// </summary>
        public static void PauseAll()
        {
            PauseAllMusic();
            PauseAllSounds();
            PauseAllUISounds();
        }

        /// <summary>
        /// Pause all music playing
        /// </summary>
        public static void PauseAllMusic()
        {
            List<int> keys = new List<int>(musicAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = musicAudio[key];
                audio.Pause();
            }
        }

        /// <summary>
        /// Pause all sound fx playing
        /// </summary>
        public static void PauseAllSounds()
        {
            List<int> keys = new List<int>(soundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = soundsAudio[key];
                audio.Pause();
            }
        }

        /// <summary>
        /// Pause all UI sound fx playing
        /// </summary>
        public static void PauseAllUISounds()
        {
            List<int> keys = new List<int>(UISoundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = UISoundsAudio[key];
                audio.Pause();
            }
        }

        /// <summary>
        /// Resume all audio playing
        /// </summary>
        public static void ResumeAll()
        {
            ResumeAllMusic();
            ResumeAllSounds();
            ResumeAllUISounds();
        }

        /// <summary>
        /// Resume all music playing
        /// </summary>
        public static void ResumeAllMusic()
        {
            List<int> keys = new List<int>(musicAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = musicAudio[key];
                audio.Resume();
            }
        }

        /// <summary>
        /// Resume all sound fx playing
        /// </summary>
        public static void ResumeAllSounds()
        {
            List<int> keys = new List<int>(soundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = soundsAudio[key];
                audio.Resume();
            }
        }

        /// <summary>
        /// Resume all UI sound fx playing
        /// </summary>
        public static void ResumeAllUISounds()
        {
            List<int> keys = new List<int>(UISoundsAudio.Keys);
            foreach (int key in keys)
            {
                Audio audio = UISoundsAudio[key];
                audio.Resume();
            }
        }
    }

    public class Audio
    {
        private static int audioCounter = 0;

        /// <summary>
        /// The ID of the Audio
        /// </summary>
        public int audioID { get; private set; }

        /// <summary>
        /// The audio source that is responsible for this audio
        /// </summary>
        public AudioSource audioSource { get; private set; }

        /// <summary>
        /// Whether the audio will be lopped
        /// </summary>
        public bool loop { get; private set; }

        /// <summary>
        /// Whether the audio persists in between scene changes
        /// </summary>
        public bool persist { get; private set; }

        /// <summary>
        /// How many seconds it needs for the audio to fade in/ reach target volume (if higher than current)
        /// </summary>
        public float fadeInSeconds { get; private set; }

        /// <summary>
        /// How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)
        /// </summary>
        public float fadeOutSeconds { get; set; }

        /// <summary>
        /// Wether the audio is currently playing
        /// </summary>
        public bool playing { get; private set; }

        /// <summary>
        /// Wether the audio is stopping
        /// </summary>
        public bool stopping { get; private set; }

        /// <summary>
        /// Wether the audio is paused
        /// </summary>
        public bool paused { get; private set; }

        /// <summary>
        /// The interpolater for fading in/out
        /// </summary>
        public float fadeInterpolater { get; private set; }

        /// <summary>
        /// Volume of the audio. It is always relative to the global volume.
        /// </summary>
        public float volume { get; set; }

        /// <summary>
        /// The volume to reach
        /// </summary>
        public float targetVolume { get; private set; }

        /// <summary>
        /// The volume the audio has when fade in/out starts
        /// </summary>
        public float onFadeStartVolume { get; private set; }

        /// <summary>
        /// Whether the audio is created and updated at least once. 
        /// </summary>
        public bool activated { get; set; }

        private AudioType audioType;

        public enum AudioType
        {
            Music,
            Sound,
            UISound
        }

        public Audio(AudioType audioType, AudioSource audioSource, AudioClip clip, bool loop, bool persist, float volume, float fadeInValue, float fadeOutValue)
        {
            this.audioID = audioCounter;
            audioCounter++;

            this.audioType = audioType;
            this.audioSource = audioSource;
            this.loop = loop;
            this.persist = persist;
            this.targetVolume = volume;
            this.volume = 0f;
            this.fadeInSeconds = fadeInValue;
            this.fadeOutSeconds = fadeOutValue;

            this.playing = false;
            this.paused = false;
            this.activated = false;

            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.volume = 0f;
            Play();
        }

        /// <summary>
        /// Start playing audio clip
        /// </summary>
        public void Play()
        {
            audioSource.Play();
            playing = true;

            onFadeStartVolume = volume;
        }

        /// <summary>
        /// Stop playing audio clip
        /// </summary>
        public void Stop()
        {
            fadeInterpolater = 0f;
            onFadeStartVolume = volume;
            targetVolume = 0f;

            stopping = true;
        }

        /// <summary>
        /// Pause playing audio clip
        /// </summary>
        public void Pause()
        {
            audioSource.Pause();
            paused = true;
        }

        /// <summary>
        /// Resume playing audio clip
        /// </summary>
        public void Resume()
        {
            audioSource.UnPause();
            paused = false;
        }

        public void Update()
        {
            activated = true;

            if (volume != targetVolume)
            {
                float fadeValue;
                fadeInterpolater += Time.deltaTime;
                if (volume > targetVolume)
                {
                    fadeValue = fadeOutSeconds;
                }
                else
                {
                    fadeValue = fadeInSeconds;
                }

                volume = Mathf.Lerp(onFadeStartVolume, targetVolume, fadeInterpolater / fadeValue);
            }

            switch (audioType)
            {
                case AudioType.Music:
                    {
                        audioSource.volume = volume * SoundManager.globalMusicVolume * SoundManager.globalVolume;
                        break;
                    }
                case AudioType.Sound:
                    {
                        audioSource.volume = volume * SoundManager.globalSoundsVolume * SoundManager.globalVolume;
                        break;
                    }
                case AudioType.UISound:
                    {
                        audioSource.volume = volume * SoundManager.globalUISoundsVolume * SoundManager.globalVolume;
                        break;
                    }
            }


            if (volume == 0f && stopping)
            {
                audioSource.Stop();
                stopping = false;
            }

            // Update playing status
            if (audioSource.isPlaying != playing)
            {
                playing = audioSource.isPlaying;
            }
        }
    }
}

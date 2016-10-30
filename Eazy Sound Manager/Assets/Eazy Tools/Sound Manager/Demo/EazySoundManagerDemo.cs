using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EazyTools.SoundManager;

public class EazySoundManagerDemo : MonoBehaviour
{
    public EazySoundDemoAudioControls[] AudioControls;
    public Slider globalVolSlider;
    public Slider globalMusicVolSlider;
    public Slider globalSoundVolSlider;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Update UI
        for(int i=0; i < AudioControls.Length; i++)
        {
            EazySoundDemoAudioControls audioControl = AudioControls[i];
            if (audioControl.audio != null && audioControl.audio.playing)
            {
                if (audioControl.pauseButton != null)
                {
                    audioControl.playButton.interactable = false;
                    audioControl.pauseButton.interactable = true;
                    audioControl.stopButton.interactable = true;
                    audioControl.pausedStatusTxt.enabled = false;
                }
            }
            else if (audioControl.audio != null && audioControl.audio.paused)
            {
                if (audioControl.pauseButton != null)
                {
                    audioControl.playButton.interactable = true;
                    audioControl.pauseButton.interactable = false;
                    audioControl.stopButton.interactable = false;
                    audioControl.pausedStatusTxt.enabled = true;
                }
            }
            else
            {
                if (audioControl.pauseButton != null)
                {
                    audioControl.playButton.interactable = true;
                    audioControl.pauseButton.interactable = false;
                    audioControl.stopButton.interactable = false;
                    audioControl.pausedStatusTxt.enabled = false;
                }
            }
        }
	}

    public void PlayMusic1()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[0];

        if (audioControl.audio != null && audioControl.audio.paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            int audioID = SoundManager.PlayMusic(audioControl.audioclip, audioControl.volumeSlider.value, true, false);
            AudioControls[0].audio = SoundManager.GetAudio(audioID);
        }
    }

    public void PlayMusic2()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[1];

        if (audioControl.audio != null && audioControl.audio.paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            int audioID = SoundManager.PlayMusic(audioControl.audioclip, audioControl.volumeSlider.value, false, false);
            AudioControls[1].audio = SoundManager.GetAudio(audioID);
        }
    }

    public void PlaySound1()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[2];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, audioControl.volumeSlider.value);

        AudioControls[2].audio = SoundManager.GetAudio(audioID);
    }

    public void PlaySound2()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[3];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, audioControl.volumeSlider.value);

        AudioControls[3].audio = SoundManager.GetAudio(audioID);
    }

    public void Pause(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundDemoAudioControls audioControl = AudioControls[audioControlID];

        audioControl.audio.Pause();
    }

    public void Stop(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundDemoAudioControls audioControl = AudioControls[audioControlID];

        audioControl.audio.Stop();
    }

    public void AudioVolumeChanged(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundDemoAudioControls audioControl = AudioControls[audioControlID];

        if (audioControl.audio != null)
        {
            audioControl.audio.SetVolume(audioControl.volumeSlider.value, 0);
        }
    }

    public void GlobalVolumeChanged()
    {
        SoundManager.globalVolume = globalVolSlider.value;
    }

    public void GlobalMusicVolumeChanged()
    {
        SoundManager.globalMusicVolume = globalMusicVolSlider.value;
    }

    public void GlobalSoundVolumeChanged()
    {
        SoundManager.globalSoundsVolume = globalSoundVolSlider.value;
    }
}

[System.Serializable]
public struct EazySoundDemoAudioControls
{
    public AudioClip audioclip;
    public Audio audio;
    public Button playButton;
    public Button pauseButton;
    public Button stopButton;
    public Slider volumeSlider;
    public Text pausedStatusTxt;
}

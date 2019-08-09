using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;

public class EazySoundManagerDemo : MonoBehaviour
{
    public EazySoundDemoAudioControls[] AudioControls;
    public Slider globalVolSlider;
    public Slider globalMusicVolSlider;
    public Slider globalSoundVolSlider;

    public GameObject test;

    private void Update ()
    {
        // Update UI
        for(int i=0; i < AudioControls.Length; i++)
        {
            EazySoundDemoAudioControls audioControl = AudioControls[i];
            if (audioControl.audio != null && audioControl.audio.IsPlaying)
            {
                if (audioControl.pauseButton != null)
                {
                    audioControl.playButton.interactable = false;
                    audioControl.pauseButton.interactable = true;
                    audioControl.stopButton.interactable = true;
                    audioControl.pausedStatusTxt.enabled = false;
                }
            }
            else if (audioControl.audio != null && audioControl.audio.Paused)
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

        if (audioControl.audio == null)
        {
            int audioID = EazySoundManager.PlayMusic(audioControl.audioclip, audioControl.volumeSlider.value, true, false);
            AudioControls[0].audio = EazySoundManager.GetAudio(audioID);
        }
        else if (audioControl.audio != null && audioControl.audio.Paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            audioControl.audio.Play();
        }
    }

    public void PlayMusic2()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[1];

        if (audioControl.audio == null)
        {
            int audioID = EazySoundManager.PlayMusic(audioControl.audioclip, audioControl.volumeSlider.value, true, false);
            AudioControls[1].audio = EazySoundManager.GetAudio(audioID);
        }
        else if (audioControl.audio != null && audioControl.audio.Paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            audioControl.audio.Play();
        }
    }

    public void PlaySound1()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[2];
        int audioID = EazySoundManager.PlaySound(audioControl.audioclip, audioControl.volumeSlider.value);

        AudioControls[2].audio = EazySoundManager.GetAudio(audioID);
    }

    public void PlaySound2()
    {
        EazySoundDemoAudioControls audioControl = AudioControls[3];
        int audioID = EazySoundManager.PlaySound(audioControl.audioclip, audioControl.volumeSlider.value);

        AudioControls[3].audio = EazySoundManager.GetAudio(audioID);
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
        EazySoundManager.GlobalVolume = globalVolSlider.value;
    }

    public void GlobalMusicVolumeChanged()
    {
        EazySoundManager.GlobalMusicVolume = globalMusicVolSlider.value;
    }

    public void GlobalSoundVolumeChanged()
    {
        EazySoundManager.GlobalSoundsVolume = globalSoundVolSlider.value;
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

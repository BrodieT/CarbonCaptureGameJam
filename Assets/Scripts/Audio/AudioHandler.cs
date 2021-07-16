using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance = default;


    [SerializeField]
    AudioSource musicSource = default;

    [SerializeField]
    AudioSource uiSource = default;

    [SerializeField]
    AudioSource playerSource = default;

    [SerializeField]
    AudioSource freeSource = default;

    [SerializeField]
    AudioBank bank = default;

    List<AudioBank.AudioFile> allAudioFiles = new List<AudioBank.AudioFile>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        allAudioFiles.AddRange(bank.allFiles);
        PlaySound(AudioBank.Audio.MENU_MUSIC);
    }

    public void PlaySound(AudioBank.Audio name)
    {

        foreach(AudioBank.AudioFile f in allAudioFiles)
        {
            if(f.name == name)
            {
                switch(f.source)
                {
                    case AudioBank.AudioSource.PLAYER:
                        playerSource.clip = f.clip;
                        playerSource.Play();
                        break;
                    case AudioBank.AudioSource.MUSIC:
                        musicSource.clip = f.clip;
                        musicSource.Play();
                        break;
                    case AudioBank.AudioSource.UI:
                        uiSource.clip = f.clip;
                        uiSource.Play();
                        break;
                    case AudioBank.AudioSource.ELSE:
                        freeSource.clip = f.clip;
                        freeSource.Play();
                        break;
                    default:
                        break;
                }

                break;
            }
        }
    }
}

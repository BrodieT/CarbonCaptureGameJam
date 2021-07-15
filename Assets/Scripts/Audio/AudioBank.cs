using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Bank", menuName = "New Audio Bank")]
public class AudioBank : ScriptableObject
{
    public enum Audio { MENU_MUSIC, GAME_MUSIC, IMPACT, COLLECT, UI, FOOTSTEPS, BOSS_MUSIC}

    public enum AudioSource { MUSIC, PLAYER, UI, ELSE}
    [System.Serializable]
    public struct AudioFile
    {
        public Audio name;
        public AudioSource source;
        public AudioClip clip;
    }

    [SerializeField]
    public List<AudioFile> allFiles = new List<AudioFile>();
}

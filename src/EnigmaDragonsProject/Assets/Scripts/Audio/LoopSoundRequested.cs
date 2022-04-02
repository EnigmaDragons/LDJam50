
using UnityEngine;

public class LoopSoundRequested
{
    public string SoundName { get; }
    public AudioSource Src { get; }

    public LoopSoundRequested(string soundName, AudioSource src)
    {
        SoundName = soundName;
        Src = src;
    }
}

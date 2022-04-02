
using UnityEngine;

public class StopSoundRequested
{
    public string SoundName { get; }
    public AudioSource Src { get; }

    public StopSoundRequested(string soundName, AudioSource src)
    {
        SoundName = soundName;
        Src = src;
    }
}

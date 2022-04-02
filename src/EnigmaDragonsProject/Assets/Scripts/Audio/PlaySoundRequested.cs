using UnityEngine;

public class PlaySoundRequested
{
    public string SoundName { get; }
    public Vector3 WorldPosition { get; }

    public PlaySoundRequested(string soundName, Vector3 worldPosition)
    {
        SoundName = soundName;
        WorldPosition = worldPosition;
    }
}

using UnityEngine;

public class PlayOneShot
{
    public AudioClipVolume Clip { get; }
    public Vector3 WorldPosition { get; }

    public PlayOneShot(AudioClipVolume clip, Vector3 worldPosition)
    {
        Clip = clip;
        WorldPosition = worldPosition;
    }
}

using UnityEngine;

public class GameSoundGuy : OnMessage<PlaySoundRequested>
{
    [SerializeField] private AudioClipVolume fillWater;
    
    protected override void Execute(PlaySoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FillWater))
            AudioSource.PlayClipAtPoint(fillWater.clip, msg.WorldPosition, fillWater.volume);
    }
}

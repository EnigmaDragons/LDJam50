using UnityEngine;

public class GameSoundGuy : OnMessage<PlaySoundRequested, LoopSoundRequested, StopSoundRequested>
{
    [SerializeField] private AudioClipVolume watering;
    [SerializeField] private AudioClipVolume fillWater;
    
    protected override void Execute(PlaySoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FillWater))
            AudioSource.PlayClipAtPoint(fillWater.clip, msg.WorldPosition, fillWater.volume);
    }

    protected override void Execute(LoopSoundRequested msg)
    {
        msg.Src.clip = watering.clip;
        msg.Src.loop = true;
        msg.Src.volume = watering.volume;
        msg.Src.Play();
    }

    protected override void Execute(StopSoundRequested msg)
    {
        msg.Src.Stop();
    }
}

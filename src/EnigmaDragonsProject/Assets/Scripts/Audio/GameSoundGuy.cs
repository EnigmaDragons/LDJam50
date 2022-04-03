using UnityEngine;

public class GameSoundGuy : OnMessage<PlaySoundRequested, LoopSoundRequested, StopSoundRequested>
{
    [SerializeField] private AudioClipVolume watering;
    [SerializeField] private AudioClipVolume fillWater;
    [SerializeField] private AudioClipVolume treeFire;
    [SerializeField] private AudioClipVolume newPlant;
    [SerializeField] private AudioClipVolume plantFull;
    
    protected override void Execute(PlaySoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FillWater))
            AudioSource.PlayClipAtPoint(fillWater.clip, msg.WorldPosition, fillWater.volume);
        if (msg.SoundName.Equals(GameSounds.NewPlant))
            AudioSource.PlayClipAtPoint(newPlant.clip, msg.WorldPosition, newPlant.volume);
        if (msg.SoundName.Equals(GameSounds.PlantFull))
            AudioSource.PlayClipAtPoint(plantFull.clip, msg.WorldPosition, plantFull.volume);
    }

    protected override void Execute(LoopSoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FillWater))
        {
            msg.Src.clip = watering.clip;
            msg.Src.volume = watering.volume;
        }
        if (msg.SoundName.Equals(GameSounds.TreeFire))
        {
            msg.Src.clip = treeFire.clip;
            msg.Src.volume = treeFire.volume;
        }
        msg.Src.loop = true;
        msg.Src.Play();
    }

    protected override void Execute(StopSoundRequested msg)
    {
        msg.Src.Stop();
    }
}

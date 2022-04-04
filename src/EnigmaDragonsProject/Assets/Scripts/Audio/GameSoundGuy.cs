using UnityEngine;
using UnityEngine.Audio;

public class GameSoundGuy : OnMessage<PlaySoundRequested, LoopSoundRequested, StopSoundRequested>
{
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClipVolume watering;
    [SerializeField] private AudioClipVolume fillWater;
    [SerializeField] private AudioClipVolume treeFire;
    [SerializeField] private AudioClipVolume newPlant;
    [SerializeField] private AudioClipVolume[] plantFull;
    [SerializeField] private AudioClipVolume footStep;
    
    protected override void Execute(PlaySoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FillWater))
            PlayOneShot(fillWater, msg.WorldPosition);
        if ( msg.SoundName.Equals(GameSounds.NewPlant))
            PlayOneShot(newPlant, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.PlantFull))
            PlayOneShot(plantFull.Random(), msg.WorldPosition);
    }

    private void PlayOneShot(AudioClipVolume a, Vector3 position)
    {
        if (a == null)
            Log.Warn("Request Sound is Null");
        else if (a.clip == null)
            Log.Warn("Requested Sound Clip is Null");
        else
            AudioClipUtils.PlayClipAtPoint(a.clip, position, a.volume, mixerGroup);
    }

    protected override void Execute(LoopSoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FootStep))
        {
            msg.Src.clip = footStep.clip;
            msg.Src.volume = footStep.volume;
        }
        if (msg.SoundName.Equals(GameSounds.Watering))
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

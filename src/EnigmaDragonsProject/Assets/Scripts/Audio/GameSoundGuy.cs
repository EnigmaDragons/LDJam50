using UnityEngine;
using UnityEngine.Audio;

public class GameSoundGuy : OnMessage<PlaySoundRequested, LoopSoundRequested, StopSoundRequested, PlayOneShot>
{
    [SerializeField] private UiSfxPlayer uiSounds;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private AudioClipVolume watering;
    [SerializeField] private AudioClipVolume fillWater;
    [SerializeField] private AudioClipVolume treeFire;
    [SerializeField] private AudioClipVolume newPlant;
    [SerializeField] private AudioClipVolume menuToggle;
    [SerializeField] private AudioClipVolume buttonHover;
    [SerializeField] private AudioClipVolume plantDie;
    [SerializeField] private AudioClipVolume gameOver;
    [SerializeField] private AudioClipVolume throwWaterBalloon;
    [SerializeField] private AudioClipVolume waterGun;
    [SerializeField] private AudioClipVolume toolUpgrade;
    [SerializeField] private AudioClipVolume plantWilting;
    [SerializeField] private AudioClipVolume[] plantFull;
    
    protected override void Execute(PlaySoundRequested msg)
    {
        if (msg.SoundName.Equals(GameSounds.FillWater))
            PlayOneShotClip(fillWater, msg.WorldPosition);
        if (gameState.State.PlantSpawningComplete && msg.SoundName.Equals(GameSounds.NewPlant))
            uiSounds.Play(newPlant);
        if (msg.SoundName.Equals(GameSounds.PlantFull))
            PlayOneShotClip(plantFull.Random(), msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.MenuToggle))
            PlayOneShotClip(menuToggle, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.PlantDie))
            PlayOneShotClip(plantDie, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.GameOver))
            PlayOneShotClip(gameOver, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.ThrowWaterBalloon))
            PlayOneShotClip(throwWaterBalloon, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.ButtonHover))
            PlayOneShotClip(buttonHover, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.WaterGun))
            PlayOneShotClip(waterGun, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.ToolUpgrade))
            PlayOneShotClip(toolUpgrade, msg.WorldPosition);
        if (msg.SoundName.Equals(GameSounds.PlantWilting))
            PlayOneShotClip(plantWilting, msg.WorldPosition);
    }

    private void PlayOneShotClip(AudioClipVolume a, Vector3 position)
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

    protected override void Execute(PlayOneShot msg)
    {
       PlayOneShotClip(msg.Clip, msg.WorldPosition);
    }

    protected override void Execute(StopSoundRequested msg)
    {
        msg.Src.Stop();
    }
}

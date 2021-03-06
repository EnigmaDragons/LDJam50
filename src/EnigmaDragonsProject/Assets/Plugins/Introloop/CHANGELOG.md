# Introloop Changelog
Sirawat Pitaksarit / Exceed7 Experiments (Contact : 5argon@exceed7.com)

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

I am keeping the **Unreleased** section at the bottom of this document.

# [4.1.0] - 2019-08-15

## Added

### Seek(elapsedTime)

This new method internally implemented as calling `Play` with custom `startTime`. Except that you do not need to specify which audio because it remembered your previous audio. Also it only works if the state is playing. From the outside, it looks like you are "seeking" the playhead.

Introloop naturally has infinite timeline. The time specified here is instead an elapsed time, Introloop will make the current position like you have played by this amount of time, according to your loop boundaries.

It is added as a part of 2019.1+ hack to fix Unity bug, which requires remembering the audio and play again. But I guess it may be handy, so I decided to make it public.

### Dirty hack to fix 2019.1+ bug added into all `IntroloopPlayer`

There is a bug in 2019.1+ where on game minimize, on pressing pause button in editor, or pausing AudioListener, all `ScheduledEndTime` will be lost. I confirmed it is not a problem in 2018.4 LTS. And it obviously affects Introloop.

There is an  [`OnApplicationPause(bool)`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html) added to `IntroloopPlayer` to fix this while waiting for Unity team to address the bug for real. I have reported since 2019.1 release, as of now that 2019.2 has been released plus the old version is at 2019.1.14f1, it was not fixed yet. I think I couldn't wait for another half a year, so I decided to bake the workaround into the code.

The ideal fix is to call `Pause` just before the game goes to minimize then `Resume` after we comeback to reschedule. However at `OnApplicationPause`, `Pause` does not work, as all audio are already on its way to pausing. (Introloop need to see which one is playing to resume correctly.)

So an another approach is that we will remember the time just before the pause, and the play again after coming back using that time. This hack does that, using the new `Seek` added in this same update, and it is wrapped in `#if UNITY_2019_1_OR_NEWER`. Also, `OnApplicationPause` do not get called in editor when pressing pause button (near the play mode button) so you will still experience this bug in the editor. In real device it should take care of the bug.

Track that bug : https://fogbugz.unity3d.com/default.asp?1151637_4i53coq9v07qctp1

## Changed

### Pause and Stop overloads changed to be able to take action in the same frame

- Previous behaviour : `Pause()` or `Stop()` without any fade time specified will results in a small pop removal fade time added to reduce harsh noise when pausing.
- Previous behaviour : `Pause(-1)` or `Stop(-1)` with negative fade time results in 0 fade time, however it needs at least the next frame to actually pause or stop since it uses the same routine as the one with fade time.
- Previous behaviour : `Pause(0)` or `Stop(0)` with zero fade time will results in a small pop removal fade time added to reduce harsh noise when pausing.
- Previous behaviour : `Pause(555)` or `Stop(555)` with positive fade time will fade to pause or fade to stop as expected.

There is a problem that no matter what you cannot stop an audio in the same frame that you tell it to. This may cause problem in some situation where you want to free up memory and cannot wait any frame.

- New behaviour : `Pause()` or `Stop()` without any fade time specified enters a special routine that instantly pause or stop in the same frame.
- New behaviour : `Pause(-1)` or `Stop(-1)` with negative fade time enters a special routine that instantly pause or stop in the same frame.
- New behaviour : `Pause(0)` or `Stop(0)` with zero fade time will results in a small pop removal fade time added to reduce harsh noise when pausing. (unchanged)
- New behaviour : `Pause(555)` or `Stop(555)` with positive fade time will fade to pause or fade to stop as expected. (unchanged)

To use the old pop removal fade time pause or stop, now the only way is that you have to use the overload with fade time, but instead input 0. It means you wants "virtually no fade" but also you don't want a noise problem. It is now a little different from the overload without any argument which do it instantly.

## Fixed

- Priority of audio sources generated are now 0 (the highest). Since it is very likely that you do not want any other sound effects to cut off the music when virtual audio sources run out.
- Time method used inside `IntroloopTrack.cs` was `Time.time`. It is now `Time.unscaledTime` so it could accommodate games with dynamically scaled time.

# [4.0.0] - 2019-06-15

It's been a year since the last update. Thank you for all your supports so far!
As per SemVer, big version change introduce breaking changes.

**Known issues**

Do not use `AudioListener.pause` to pause, [the documentation](https://docs.unity3d.com/ScriptReference/AudioListener-pause.html) said that it would preserve scheduling status, but what I found is that sometimes it deletes `PlaySchedule` on coming back from pause. I have submitted as a bug report which you could track [here](https://fogbugz.unity3d.com/default.asp?1151637_4i53coq9v07qctp1) if it was resolved or not. If possible, stop completely and resume by playing anew with the new `startTime` parameter.

## Added

### Unity Package Manager & Assembly Definition File

The package has been conformed to the recent Unity practice of putting `packages.json` and properly using `asmdef` file. In the future when Asset Store became integrated with UPM, Introloop is now ready for that.

This also allows you to put the Introloop package outside your Unity project, instead anywhere in your harddisk, then use the local UPM feature to share a single Introloop sources over multiple projects, thanks to `packages.json` file.

This is a breaking change if you are also using `asmdef` file, because you now have to link up your `asmdef` with Introloop's `E7.Introloop`.

The assembly definitions will also prevents scripts in the intro scene from getting into your game as well, since you could link to only `E7.Introloop` and not `E7.Introloop.Demo`. In the future when incremental compiler works better and we could enter play mode without assembly reload, this would help your game even more.

### Script icons added

`IntroloopAudio` icon is provided so you could discern them from other `ScriptableObject` files.

`IntroloopPlayer` icon is also added, which could show up in the scene since it is a `MonoBehaviour`. If you don't like this, you can click on "Gizmos" in your Scene tab, and then **left click on the icon image, not on the little arrow or the checkbox**. It will be greyed out and disappear from the scene view.

An [Affinity Designer](https://affinity.serif.com/en-gb/designer/) project files are even included, so you could modify some part of the icon if you don't like them!

### `startTime` optional parameter added to `IntroloopPlayer.Play`

This is one of the most requested feature. You are now able to specify start time when starting introloop audio.

The time you specify here is called "playhead time". Since `IntroloopAudio` conceptually has infinite length, any number that is over looping boundary will be wrapped over to the intro boundary in the calculation.

In effect, you can never specify start time so that it starts after looping boundary.

### `GetPlayheadTime()` added to `IntroloopPlayer`

You can also ask the currently playing `IntroloopPlayer` the "playhead time". This is not accumulated, the time could decrease when it goes over looping boundary back to intro boundary. So you better visualize it as an actual playhead that jumps back on loop.

This number is usable with the new start time option. Together, you can implement something like remembering field music time and enter battle scene with an another Introloop music completely replaced the field music. When you come back, you can use the remembered "playhead time" to start the field music from the same place. It is a good idea to start from the beginning if the player had passed through some "check point" like a cutscene or entered town, otherwise continue with the playhead time.

## Changed

### `IntroloopSettings` is now the field of `IntroloopPlayer` rather than a separated component

That was a stupid design, but I was afraid to let that go.

It will cause missing script on your prefab template in your `Resources/Introloop/` folder, please go remove the missing component which was `IntroloopSettings` and assign those values in `IntroloopPlayer` instead. Luckily the values are just a mixer and fade time, but sorry for inconvenience.

### A bit of prepare time added to immediate Play() method

Theoretically playing "instantly" is impossible, even though we want it to play "right now" on calling `IntroloopPlayer.Play()`. So this time is the near-instant future that we told the scheduling method to start. With this, it could get better chance that the first loop is accurate at the expense of some waiting time.

(But Introloop was never meant to be a low latency solution, for that please search for [Native Audio](http://exceed7.com/native-audio/).)

Previously, you could randomly get a bit off first loop depending on whether it could fulfill this "right now" or not. You notice that usually all loops except the first one are accurate, that's because it got time far into the future as a schedule, unlike the first play.
        
The default number `0.02f` is choosen to be a bit more that 16ms, the time per 1 frame on 60 FPS. So that it could get through a busy frame.

## Removed

### Default fade length removed from `IntroloopSettings`

I have decided that default fade length should be something coming from your game, rather than the task of Introloop. Along with this...

### All `_Fade` variants of `Play`, `Stop`, `Pause`, `Resume` removed

There is now just 1 version of each. Now the normal version handles fade by using the optional fade parameter. Enter 0 or not using the parameter to get the usual "pop removal fade length". If you use negative number, the action is immediate without pop removal fade length.

If you was using default fade length with `Fade` variants, make your own default fade length variable coming from the game and input it into the fade parameter of each method.

### Dropped support for Unity 5.5.6 and 5.6.5

I used to maintain support far back to those versions. It's time to move on! Even though nothing prevents Introloop from working on those versions, project downgrading is getting more and more difficult.

The new support ranges is as follows : Oldest version in Unity Hub, latest LTS version, latest TECH version, and latest beta version will be tested. As of current, they are : 2017.1.5f1, 2017.4.26f1, 2018.3.14f1, 2019.1.1f1.

## Fixed

### Streaming load type is now much more accurate when used with Introloop

The mentioned prepare time is the final key to use `Streaming` audio load type with Introloop! Because streaming audio size is technically so small that it tries to start instantly, it conflicts with the scheduled start time that is too immediate to fulfill. I observed that with this small delay, `Streaming` audio's first loop became much more reliable. Took me long enough to figure this one out. Now, enjoy your memory-conscious introlooped music.

### Suppressed unassigned variables warnings

I put `pragma warning disable 0649` on several places. No idea how I didn't think of doing this before after all these years. (Sorry)

### Code documentation + website improved

I have rewritten everything, and also some XML tag that links code references together are added.


# [3.0.0] - 2018-05-20

## Added

### Multiple singleton players

With `IntroloopPlayer.Instance.Play`, it refers to the same "Instance" throughout your game. Meaning that you cannot have 2 concurrent Introloop player playing+looping at the same time.

With `MySubClassOfIntroloopPlayer.Get`, it will spawns different set of player. This means you can now have many Introloop playing at the same. It is useful for dividing the players into several parts. Like BGMPlayer, AmbientPlayer, etc. 

Moreover, you can then define your own methods on your subclass to be more suitable for your game. Like `FieldBGMPlayer.Get.PlayDesertTheme()` instead of `IntroloopPlayer.Instance.Play(desertTheme);`.

The template's name was hardcoded as the same as your class name. If your class name is FieldBGMPlayer then you must have FieldBGMPlayer.prefab in the same location as IntroloopPlayer.prefab in Resources folder. (Defined in IntroloopSettings.cs constant fields.)

See the new **IntroloopDemoSubclass** demo scene for how this works.

### IntroloopPlayer.InternalAudioSources property

You can `foreach` on this property to make changes to all 4 `AudioSource` that Introloop uses at once.
You should not use this in `Awake`, as Introloop might be still not yet spawn the `AudioSource`.

### Local Introloop

You can now have non-static IntroloopPlayer anywhere in the scene as many instances as you like.
If you do, you need to keep and access it with normal `IntroloopPlayer` variable. 

How to get one is easy, just attach `IntroloopPlayer` (and optionally with `IntroloopSetting`) to one of your game object.
All of the required `AudioSource` will be spawned directly as a child of this game object.

Or an anothey way, you could `gameObject.AddComponent<IntroloopPlayer>()` anytime. The next frame all of the required audio sources
will be ready for play.

These local Introloop does not automatically get `DontDestroyOnLoad` like `IntroloopPlayer.Instance` or `Subclass.Get` ones, 
thus they will stop playing if you change scene with `LoadSceneMode.Single`, etc. Also, it will be positional by default. (Spatial blend is 1, or full 3D.)

### Positional Introloop

The point of having a local Introloop is that you would like it to be positional. Imagine you have 10 bushes with an individual seamlessly looping leaf sound. And you also want these to get louder as the player approaches it.
You could spawn a local Introloop and position them on each bush. Note that each one will uses 4 `AudioSource`.

The following functions was added : 

`IntroloopPlayer.Set/MatchAudioSourceCurve` - Set audio curve with `AnimationCurve` or copy audio curvesfrom an another `AudioSource`.
`IntroloopPlayer.Set2DSpatialBlend` - `IntroloopPlayer.Instance` and `Subclass.Get` automatically get this. Hear full sound independent of `AudioListener` position.
`IntroloopPlayer.Set3DSpatialBlend` - Local Introloop automatocally get this. Introloop is now dependent on positioning.
`IntroloopPlayer.SetSpatialBlend` - Set spatial blend to any number to make it semi 2D-3D.

See the new **IntroloopDemoLocalPositional** demo scene for how this works.

## Changed

- Introloop now use a `namespace` coding practice like all other plugins. The `namespace` is `using E7.Introloop`.
- Minimum supported version now moved to 2017.1.3f1, the lowest version available on Unity Hub. Technically, it should still mostly works from 5.0 onwards but I won't be checking integrity on those versions anymore.
- When right click creating `IntroloopAudio` file, it was at `Asset > Create IntroloopAudio` but now it is now properly in `Asset > Create > IntroloopAudio`.
- Some class which is not intended for you to use has been changed to `internal`.

# [2.0.0] - 2017-04-14

## Added

### Pitch

You can now specify a pitch in an IntroloopAudio asset file. If you would like to use multiple pitches of the same audio, you can just copy the asset file and have different pitches. It can reference to the same actual audio file. Works fine with pause, resume, automatic memory management.

### Preload

A feature where critical precision of starting an Introloop Audio is needed. Load the audio by calling IntroloopAudio.Instance.Preload(yourIntroloopAudio) beforehand to pre-consume memory, and then call Play as usual afterwards.

### Ogg Streaming as Introloop on iOS/Android

In Unity 5.5.2 they added support for choosing "Streaming" with OGG on iOS/Android. I am happy to inform that this option works with Introloop. Everthing will be the same except it will not cost you as much memory of an entire audio on Play as before in an exchange for some CPU workload.

# [1.0.0] - 2015-12-15

## Added

It's the first version!
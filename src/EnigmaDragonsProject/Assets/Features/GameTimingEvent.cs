using System;
using UnityEngine;

[Serializable]
public class GameTimingEvent
{
    [SerializeField] private PlantController plant;
    [SerializeField] private float secondsToAppearOverride;
    [SerializeField] private string descriptionOverride;

    public PlantController Plant => plant;
    public float SecondsToAppear => secondsToAppearOverride > 0 ? secondsToAppearOverride : 90;
    public string Description => string.IsNullOrWhiteSpace(descriptionOverride) ? plant.Plant.IncomingDescription : descriptionOverride;
}
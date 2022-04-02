using System;

[Serializable]
public class WaterConsumption
{
	[SerializeField] private float threshold;
	[SerializeField] private float waterConsumptionPerSecond;

	public float Threshold => threshold;
	public float WaterConsumptionPerSecond => waterConsumptionPerSecond;
}
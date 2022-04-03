
using System;
using Sirenix.OdinInspector;

[Serializable]
public class PlayerStats
{
    [InfoBox("All numbers are stored as multipliers")]
    
    public float speed = 1f; //done
    public float fillSpeed = 1f; //done
    public float capacity = 1f; //done
    public float wateringSpeed = 1f; //done
    public float waterPerShot = 1f; //not done
    public float spellPower = 1f; //done
    public float spellDuration = 1f;
    public float spellCooldown = 1f; //done
    
    [InfoBox("Except this one, this one is additive")]
    public int bonusCharges = 0; //done

    public static PlayerStats operator +(PlayerStats stats1, PlayerStats stats2)
    {
        stats1.speed += stats2.speed;
        stats1.fillSpeed += stats2.fillSpeed;
        stats1.capacity += stats2.capacity;
        stats1.wateringSpeed += stats2.wateringSpeed;
        stats1.waterPerShot += stats2.waterPerShot;
        stats1.spellPower += stats2.spellPower;
        stats1.spellDuration += stats2.spellDuration;
        stats1.spellCooldown += stats2.spellCooldown;
        stats1.bonusCharges += stats2.bonusCharges;
        return stats1;
    }
    
}
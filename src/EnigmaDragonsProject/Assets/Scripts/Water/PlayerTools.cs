using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "playerTools")]
public class PlayerTools : ScriptableObject
{
    [SerializeField] private WateringTool startMelee;
    [SerializeField] private WateringTool startRanged;
    [SerializeField] private CurrentGameState gameState;

    public void FillTolls()
    {
        gameState.UpdateState(x =>
        {
            if(x.MeleeTool) x.MeleeTool.Fill();
            if(x.RangedTool) x.RangedTool.Fill();
        });
    }

    public WateringTool GetMeleeTool() => gameState.State.MeleeTool;

    public WateringTool GetRangedTool() => gameState.State.RangedTool;

    public void Reset()
    {
        gameState.UpdateState(x =>
        {
            x.MeleeTool = startMelee;
            x.RangedTool = startRanged;
            startMelee.Reset();
            startRanged.Reset();
        });
    }
}
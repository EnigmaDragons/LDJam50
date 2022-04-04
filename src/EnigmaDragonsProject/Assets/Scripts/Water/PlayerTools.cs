using UnityEngine;

[CreateAssetMenu(menuName = "playerTools")]
public class PlayerTools : ScriptableObject
{
    [SerializeField] private WateringTool startMelee;
    [SerializeField] private WateringTool startRanged;
    [SerializeField] private CurrentGameState gameState;

    public void FillTools()
    {
        gameState.UpdateState(x =>
        {
            if(x.MeleeTool) x.MeleeTool.Fill();
            if(x.RangedTool) x.RangedTool.Fill();
        });
    }

    public WateringTool GetMeleeTool()
    {
        var tool = gameState.State.MeleeTool;
        return tool;
    }

    public WateringTool GetRangedTool() => gameState.State.RangedTool;

    public void Reset()
    {
        gameState.UpdateState(x =>
        {
            x.MeleeTool = startMelee;
            x.RangedTool = startRanged;
            if (startMelee != null) 
                startMelee.Reset();
            if (startRanged != null) 
                startRanged.Reset();
        });
    }
}

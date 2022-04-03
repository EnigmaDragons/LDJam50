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
        Log.Info("Init Player Tools");
        gameState.UpdateState(x =>
        {
            x.MeleeTool = startMelee;
            x.RangedTool = startRanged;
            if (startMelee != null)
            {
                startMelee.Reset();
                Log.Info($"Player Melee Tool {gameState.State.MeleeTool.name ?? "null"}");
            }

            if (startRanged != null)
            {
                startRanged.Reset();
                Log.Info($"Player Ranged Tool {gameState.State.RangedTool.name ?? "null"}");
            }
        });
    }
}

using UnityEngine;

public class UponEnableSendShowUpgrades : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    
    private void OnEnable()
    {
        if (gameState == null)
        
            Log.Error("Game State is null", gameObject);
        else
            gameState.UpdateState(x => x.GiveUpgrade = true);
    }
}
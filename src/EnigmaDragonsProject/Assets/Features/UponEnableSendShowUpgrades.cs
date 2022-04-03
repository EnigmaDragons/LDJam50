using UnityEngine;

public class UponEnableSendShowUpgrades : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    
    private void OnEnable()
    {
        gameState.UpdateState(x => x.GiveUpgrade = true);
    }
}
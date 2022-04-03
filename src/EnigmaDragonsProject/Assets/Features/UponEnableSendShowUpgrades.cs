using UnityEngine;

public class UponEnableSendShowUpgrades : MonoBehaviour
{
    private void OnEnable()
    {
        Message.Publish(new ShowUpgrade());
    }
}
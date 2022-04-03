using UnityEngine;
using UnityEngine.InputSystem;

public class SpellsController : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private SpellController waterBalloon;
    [SerializeField] private WaterBalloon waterBalloonPrototype;
    [SerializeField] private GameObject player;
    private const float _waterBalloonCooldown = 10f;
    
    private void Update()
    {
        if (gameState.State.WaterBaloonUnlocked)
            waterBalloon.Init();

        if (Input.GetKeyDown(KeyCode.Keypad1) && waterBalloon.IsAvailable())
        {
            Instantiate(waterBalloonPrototype, player.transform.position + player.transform.forward, Quaternion.identity);
            waterBalloon.Use(_waterBalloonCooldown * gameState.State.playerStats.spellCooldown);
        }
    }
}
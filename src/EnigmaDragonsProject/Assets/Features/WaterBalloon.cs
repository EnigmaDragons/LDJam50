using System.Linq;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject onCollision;
    [SerializeField] private CurrentGameState gameState;
    private const float _water = 10;
    private const float _maxDistance = 6;
    private const float _spread = 4;
    private Vector3 _direction;
    private float _distanceRemaining;
    private bool _collided;
    
    public void Init(Vector3 direction)
    {
        _direction = direction;
        _distanceRemaining = _maxDistance;
    }

    private void Update()
    {
        var travelDistance = _direction * Time.deltaTime * speed;
        transform.position += travelDistance;
        _distanceRemaining -= Time.deltaTime * speed;
        if (_distanceRemaining <= 0)
            OnTriggerEnter(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collided)
            return;
        _collided = true;
        var results = Physics.OverlapSphere(transform.position, _spread * gameState.State.playerStats.spellPower, layerMask: LayerMask.GetMask("Plants"));
        foreach (var hit in results)
        {
            var plant = hit.GetComponent<PlantController>();
            if (plant == null)
                continue;
            plant.AddWater(_water * gameState.State.playerStats.spellPower);
        }
        var collision = Instantiate(onCollision, transform.position, Quaternion.identity);
        collision.transform.localScale = new Vector3(gameState.State.playerStats.spellPower, gameState.State.playerStats.spellPower, gameState.State.playerStats.spellPower);
        gameObject.SetActive(false);
    }
}
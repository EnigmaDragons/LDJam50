using UnityEngine;

public class WaterShot : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject onCollision;
    private Vector3 _direction;
    private float _waterAmount;
    private float _distanceRemaining;
    private bool _collided;
    
    public void Init(float waterAmount, Vector3 direction, float range)
    {
        _waterAmount = waterAmount;
        _direction = direction;
        _distanceRemaining = range;
    }

    private void Update()
    {
        var travelDistance = _direction * Time.deltaTime * speed;
        transform.position += travelDistance;
        _distanceRemaining -= Time.deltaTime * speed;
        if (_distanceRemaining <= 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collided)
            return;
        _collided = true;
        var plantController = other.gameObject.GetComponent<PlantController>();
        if (plantController != null)
            plantController.AddWater(_waterAmount);
        Instantiate(onCollision, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
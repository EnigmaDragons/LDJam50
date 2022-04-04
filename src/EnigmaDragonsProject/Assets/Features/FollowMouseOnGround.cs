using UnityEngine;

public class FollowMouseOnGround : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private GameObject pointFrom; //hacks
    
    private Camera cam;
    
    private void Update()
    {
        if (!gameState.State.WaterBalloonUnlocked && gameState.State.RangedTool == null)
            return;
        if (cam == null)
            cam = Camera.main;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
        {
            var point = hit.point;
            point.y = transform.position.y;
            transform.position = point;
            transform.rotation = Quaternion.LookRotation((point - pointFrom.transform.position).normalized);
        }
        else
            Log.Error("no hit against the raycast quad");
    }
}
using UnityEngine;

public class FollowMouseOnGround : MonoBehaviour
{
    private Camera cam;
    
    private void Update()
    {
        if (cam == null)
            cam = Camera.main;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
        {
            var point = hit.point;
            point.y = transform.position.y;
            transform.position = point;
        }
        else
            Log.Error("no hit against the raycast quad");
    }
}
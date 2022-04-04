using UnityEngine;

public class ExpandingMinimap : MonoBehaviour
{
    [SerializeField] private GameObject miniMap;
    [SerializeField] private GameObject macroMap;

    private bool _expanded;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _expanded = !_expanded;
            miniMap.SetActive(!_expanded);
            macroMap.SetActive(_expanded);
        }
    }
}
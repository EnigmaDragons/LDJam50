using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [SerializeField]
    private GameObject areaTile;
    [SerializeField]
    private float areaSize;

    private Transform[,] areaGrid;
    [Header("Unlocked area")]
    [TableMatrix(SquareCells = true, DrawElementMethod = nameof(DrawColoredEnumElement))]
    [ShowInInspector]
    private bool[,] unlocksGrid;

    private void Awake()
    {
        areaGrid = new Transform[5, 5];
        unlocksGrid = new bool[5, 5];
    }

    [Button]
    private void BuildGrid()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                var inst = Instantiate(areaTile, transform.position + new Vector3(i * areaSize, 0, j * areaSize),
                    Quaternion.identity);
                inst.SetActive(false);
                inst.transform.SetParent(transform);
                areaGrid[i, j] = inst.transform;
                unlocksGrid[i, j] = false;
            }
        }
    }

    [Button]
    private void ClearGrid()
    {
        var count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    [Button(ButtonStyle.FoldoutButton)]
    private void UnlockCell(int x, int z)
    {
        if (unlocksGrid[x, z]) return;
        unlocksGrid[x, z] = true;
        areaGrid[x, z].gameObject.SetActive(true);
    }
    
    
    private static bool DrawColoredEnumElement(Rect rect, bool value)
    {
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value = !value;
            GUI.changed = true;
            Event.current.Use();
        }
        
#if UNITY_EDITOR        
        UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));
#endif

        return value;
    }
}

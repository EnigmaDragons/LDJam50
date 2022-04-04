using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpellObject : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private float baseCooldown;
    [PreviewField]
    [SerializeField] private Sprite sprite;
    
}

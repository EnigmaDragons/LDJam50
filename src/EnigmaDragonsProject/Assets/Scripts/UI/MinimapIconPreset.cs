using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Minimap/Icon Preset")]
    public class MinimapIconPreset : ScriptableObject
    {
        [Serializable]
        private class NamedIcon
        {
            public string name;
            [PreviewField]
            public Sprite icon;
        }

        [SerializeField]
        private List<NamedIcon> icons;

        private Dictionary<string, Sprite> IconDictionary => icons.ToDictionary(item => item.name, item => item.icon);


        public List<String> GetAllIconNames()
        {
            return IconDictionary.Keys.ToList();
        }

        public bool HasIcon(string name)
        {
            if (IconDictionary == null) return false;
            return IconDictionary.ContainsKey(name);
        }

        public bool HasAnyIcon()
        {
            if (IconDictionary == null) return false;
            return IconDictionary.Count > 0;
        }
        
        public Sprite GetIcon(string name)
        {
            return IconDictionary[name];
        }
    }
}

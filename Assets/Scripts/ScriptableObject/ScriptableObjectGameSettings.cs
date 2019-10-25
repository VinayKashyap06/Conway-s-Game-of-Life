using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Commons
{
    [CreateAssetMenu(fileName ="Game Settings",menuName ="Custom Objects/ Game Settings", order=0)]
    public class ScriptableObjectGameSettings : ScriptableObject
    {
        [Header("Type of Visual")]
        public e_VisualType visualType;

        [Header("Visual Data List Items")]
        public List<VisualData> visualData;

        [Header("Global Properties")]
        public int gridSizeX;
        public int gridSizeY;
        public Color aliveColor;
        public Color deadColor;

        public GameObject GetVisualPrefab(e_VisualType visualType)
        {
            for (int i = 0; i < visualData.Count; i++)
            {
                if (visualData[i].visualType==visualType)
                {
                    return visualData[i].prefab;
                }
            }
            return null;
        }
    }
}
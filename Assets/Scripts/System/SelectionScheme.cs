using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingTools
{
    [CreateAssetMenu(menuName = "Monster-Town/Scheme")]
    public class SelectionScheme : ScriptableObject
    {
        public Color lightColor;
        public Material selectionMaterial;
        public Material hologramMaterial;

    }

}

using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    [CreateAssetMenu(fileName = "ArtConfig", menuName = "ScriptableObject/ArtConfig")]
    public class ArtConfig : ScriptableObject
    {
        [SerializeField] private Sprite classicNode;
        [SerializeField] private Sprite gravityNode;

        public Sprite GetNodeImageByType(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.Gravity: return gravityNode;

                default: return classicNode;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class PathType : MonoBehaviour
    {
        public TypesOfPaths thisPath;
        public enum TypesOfPaths
        {
            RockPath,
            BoostPath
        }
    }
}
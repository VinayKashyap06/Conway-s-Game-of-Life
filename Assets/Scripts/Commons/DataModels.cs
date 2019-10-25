using System;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    #region Enums

    //Visual type to select, comes from Scriptable Object
    public enum e_VisualType
    {
        NONE,
        CUBE,
        SPHERE        
    }

    //State type enum
    public enum e_StateType
    {
        NONE=-1,
        DEAD,
        ALIVE
    }
    
    #endregion

    #region Data Structures
    [Serializable]
    public struct VisualData
    {
        public e_VisualType visualType;
        public GameObject prefab;
    }


    public class VisualInfo
    {
        public GameObject visual;
        public e_StateType currentState;
        private Material visualMat;
        
        //in built functions keeps controller code clean
        public void SetColor()
        {
            if (visualMat==null)
            {
                visualMat = visual.GetComponentInChildren<Renderer>().material;
            }
            visualMat.color = currentState==e_StateType.DEAD ? Globals.DeadColor : Globals.AliveColor;
            //Debug.Log(visualColor);
        }
    }

    #endregion
}

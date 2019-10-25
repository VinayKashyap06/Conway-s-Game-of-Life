using System;
using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
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

    public class VisualInfo
    {
        public GameObject visual;
        public e_StateType currentState;
        private Color visualColor;
        
        //in built functions keeps controller code clean
        public void SetColor()
        {
            if (visualColor==null)
            {
                visualColor = visual.GetComponent<Renderer>().material.color;
            }

            if (currentState==e_StateType.DEAD)
            {
                visualColor = Globals.DeadColor;
            }
            else
            {
                visualColor = Globals.AliveColor;
            }
        }
    }
}

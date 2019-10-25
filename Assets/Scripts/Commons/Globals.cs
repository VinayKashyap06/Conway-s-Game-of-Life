using System;
using UnityEngine;


namespace Commons
{
    //demo of how static classes can be used
    public static class Globals
    {       
        private static Color aliveColor;
        private static Color deadColor;
        private const int gridSize=6;

        //Getter and Setter
        public static Color AliveColor
        {
            get
            {
                return aliveColor;
            }
            set
            {
                aliveColor = value;
            }
        }
        public static Color DeadColor
        {
            get
            {
                return deadColor;
            }
            set
            {
                deadColor = value;
            }
        }
        public static int GridSize
        {
            get
            {
                return gridSize;
            }
        }
    }
}

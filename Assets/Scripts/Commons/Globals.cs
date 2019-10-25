using System;
using UnityEngine;


namespace Commons
{
    //demo of how static classes can be used
    public static class Globals
    {
        #region Private static Properties

        private static Color aliveColor;
        private static Color deadColor;
        private static int gridSizeX=6;
        private static int gridSizeY = 6;
        #endregion

        #region Getters and Setters
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
        public static int GridSizeX
        {
            get
            {
                return gridSizeX;
            }
            set
            {
                gridSizeX = value;
            }
        }
        public static int GridSizeY
        {
            get
            {
                return gridSizeY;
            }
            set
            {
                gridSizeY = value;
            }
        }
        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Commons;
using GameSystem.Controllers;
using UnityEngine;

namespace GameSystem
{
    public class ServiceGame : SingletonBase<ServiceGame>
    {
        #region Public Properties

        public ScriptableObjectGameSettings gameSettings;
        #endregion

        #region Private Properties

        private float timeelapsed;
        private float Max_TIME = 2f;
        private ControllerGrid controllerGrid;
        #endregion

        #region Overriden Methods
        protected override void OnInitalize()
        {
            base.OnInitalize();
            SaveSettingsToGlobals();            
            controllerGrid = new ControllerGrid();
        }
        #endregion

        #region Unity_Methods
        private void Start()
        {
            SwitchVisual(gameSettings.visualType);            
        }
        private void FixedUpdate()
        {
            timeelapsed += Time.deltaTime;
            if (timeelapsed>=Max_TIME)
            {
                controllerGrid.Tick();
                timeelapsed = 0;
            }
        }
        #endregion

        #region Public_Methods
        public void SwitchVisual(e_VisualType visualType)
        {
            GameObject newPrefab = gameSettings.GetVisualPrefab(visualType);
            controllerGrid.SwitchPrefab(newPrefab);
        }
        public void StartSimulation()
        {
            controllerGrid.StartSimulation(Globals.GridSizeX, Globals.GridSizeY);
        }
        public void StopSimulation()
        {
            controllerGrid.StopSimulation();
        }
        #endregion

        #region Private_Methods
        private void SaveSettingsToGlobals()
        {
            Globals.AliveColor = gameSettings.aliveColor;
            Globals.DeadColor = gameSettings.deadColor;
            Globals.GridSizeX = gameSettings.gridSizeX;
            Globals.GridSizeY = gameSettings.gridSizeY;
        }
        #endregion
    }
}
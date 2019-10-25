using System;
using System.Collections;
using System.Collections.Generic;
using Commons;
using GameSystem.Controllers;
using TMPro;
using UnityEngine;

namespace GameSystem
{
    public class ServiceGame : SingletonBase<ServiceGame>
    {
        #region Public Properties

        public ScriptableObjectGameSettings gameSettings;
        #endregion

        #region Private Properties

        private float timeElapsed;
        private float MAX_TIME = .5f;
        private ControllerGrid controllerGrid;
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Singleton base method called. 
        /// Saving settings and creating a new controller
        /// </summary>
        protected override void OnInitalize()
        {
            base.OnInitalize();
            SaveSettingsToGlobals();            
            controllerGrid = new ControllerGrid();
        }
        #endregion

        #region Unity_Methods
        /// <summary>
        /// Setting a visual at the start
        /// </summary>
        private void Start()
        {
            SwitchVisual(gameSettings.visualType);            
        }
        /// <summary>
        /// Calls Fixed update for fixed fps
        /// Ticks are sent to controller
        /// </summary>
        private void FixedUpdate()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed>=MAX_TIME)
            {
                controllerGrid.Tick();
                timeElapsed = 0;
            }
        }
        #endregion

        #region Public_Methods
        /// <summary>
        /// Switch visual is called to switch the type of visual
        /// </summary>
        /// <param name="visualType"></param>
        public void SwitchVisual(e_VisualType visualType)
        {
            GameObject newPrefab = gameSettings.GetVisualPrefab(visualType);
            controllerGrid.SwitchPrefab(newPrefab);
        }

        /// <summary>
        /// Starts Simulation
        /// </summary>
        public void StartSimulation()
        {
            controllerGrid.StartSimulation(Globals.GridSizeX, Globals.GridSizeY);
        }

        /// <summary>
        /// Sets Generation text for the controller
        /// </summary>
        /// <param name="generationText"></param>
        public void SetGenText(TextMeshProUGUI generationText)
        {
            controllerGrid.SetGenText(generationText);
        }
        /// <summary>
        /// Sets Time text for the controller
        /// </summary>
        /// <param name="timeText"></param>
        public void SetTimeText(TextMeshProUGUI timeText)
        {
            timeText.text = "Timestep: "+MAX_TIME.ToString();
        }
        /// <summary>
        /// Stops Simulation
        /// </summary>
        public void StopSimulation()
        {
            controllerGrid.StopSimulation();
        }
        #endregion

        #region Private_Methods
        /// <summary>
        /// Save Settings from Scriptable Object to the Globals Static class
        /// </summary>
        private void SaveSettingsToGlobals()
        {
            Globals.AliveColor = gameSettings.aliveColor;
            Globals.DeadColor = gameSettings.deadColor;
            Globals.GridSizeX = gameSettings.gridSizeX;
            Globals.GridSizeY = gameSettings.gridSizeY;
            Globals.MaxTime = gameSettings.timeStep;

            //set limit so that user doesn't crash the game or  wait too long
            if (gameSettings.timeStep < 0.2f)
            {
                Globals.MaxTime = 0.2f;
            }
            else if(gameSettings.timeStep>10f)
            {
                Globals.MaxTime = 10f;
            }

            MAX_TIME = Globals.MaxTime;
        }
        #endregion
    }
}
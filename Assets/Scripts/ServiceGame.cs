using System.Collections;
using System.Collections.Generic;
using Commons;
using GameSystem.Controllers;
using UnityEngine;

namespace GameSystem
{
    public class ServiceGame : SingletonBase<ServiceGame>
    {
        private ControllerGrid controllerGrid;

        protected override void OnInitalize()
        {
            base.OnInitalize();
           // controllerGrid = new ControllerGrid();
        }

        private void Start()
        {
            StartSimulation(Globals.GridSize);
        }

        public void SwitchVisual(e_VisualType visualType)
        {
            //GameObject 
            //controllerGrid.SwitchPrefab();
        }
        public void StartSimulation(int gridSize)
        {
            controllerGrid.StartSimulation(gridSize);
        }
    }
}
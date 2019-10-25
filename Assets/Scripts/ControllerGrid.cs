using System;
using UnityEngine;
using Commons;

namespace GameSystem.Controllers
{    
    public class ControllerGrid
    {
        private GameObject prefabToUse;
        private VisualInfo[,] previousGenGrid;
        private VisualInfo[,] nextGenGrid;
        private int currentGridSize;
        

        public void StartSimulation(int gridSize)
        {
            currentGridSize = gridSize;
            previousGenGrid = new VisualInfo[gridSize, gridSize];
            SetRandomGrid();
            nextGenGrid = previousGenGrid;
        }

        private void SetRandomGrid()
        {
            for (int i = 0; i < currentGridSize; i++)
            {
                for (int j = 0; j < currentGridSize; j++)
                {
                    previousGenGrid[i, j] = new VisualInfo();
                    previousGenGrid[i, j].currentState =(e_StateType)UnityEngine.Random.Range(0,2);                    
                    previousGenGrid[i, j].visual = GameObject.Instantiate(prefabToUse, new Vector3(i,j,1f), Quaternion.identity);
                    previousGenGrid[i, j].visual.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    previousGenGrid[i, j].SetColor();
                }
            }
        }
    }
}

using System;
using UnityEngine;
using Commons;
using TMPro;

namespace GameSystem.Controllers
{    
    public class ControllerGrid : ITickable
    {
        #region Private Properties

        private GameObject prefabToUse;
        private VisualInfo[,] previousGenGrid;
        private int[,] newStates;
        private int currentGridSizeX;
        private int currentGridSizeY;
        private GameObject gridParent;
        private bool simulate=false;
        private int gen = 0;
        private TextMeshProUGUI genText;
        #endregion

        #region Public_Methods
        public void StartSimulation(int gridSizeX, int gridSizeY)
        {
            Camera.main.transform.localPosition = new Vector3(Globals.GridSizeX / 2f, Globals.GridSizeY / 2f, -10f);
            if (gridParent != null)
                GameObject.Destroy(gridParent);
            gridParent = new GameObject("GridParent");
            currentGridSizeX = gridSizeX;
            currentGridSizeY = gridSizeY;

            previousGenGrid = new VisualInfo[gridSizeX, gridSizeY];
            newStates = new int[gridSizeX,gridSizeY];
            SetRandomGrid();
            simulate = true;
        }
        public void Tick()
        {
            if (!simulate)            
                return;            
            CalculateNewGrid();
            gen++;
            genText.text ="Gen: "+gen.ToString();

        }
        public void SwitchPrefab(GameObject newPrefab)
        {
            prefabToUse = newPrefab;
        }
        public void StopSimulation()
        {
            simulate = false;
            gen = 0;
        }
        public void SetGenText(TextMeshProUGUI generationText)
        {
            genText = generationText;
        }

        #endregion
        #region Private_Methods
        private void PrintNextGenElements()
        {
            for (int i = 0; i < currentGridSizeY; i++)
            {
                for (int j = 0; j < currentGridSizeX; j++)
                {
                    Debug.Log("<color=green> " + newStates[i, j] + "</color>");
                }
            }
        }    
        private void PrintOldGenElements()
        {
            for (int i = 0; i < currentGridSizeY; i++)
            {
                for (int j = 0; j < currentGridSizeX; j++)
                {
                    Debug.Log("<color=red> " + previousGenGrid[i, j].currentState + "</color>");
                }
            }
        }
        private void SetRandomGrid()
        {
            for (int i = 0; i < currentGridSizeY; i++)
            {
                for (int j = 0; j < currentGridSizeX; j++)
                {
                    previousGenGrid[i, j] = new VisualInfo();
                    previousGenGrid[i, j].currentState =(e_StateType)UnityEngine.Random.Range(0,2);                    
                    previousGenGrid[i, j].visual = GameObject.Instantiate(prefabToUse, new Vector3(i,j,1f), Quaternion.identity);
                    previousGenGrid[i, j].visual.transform.SetParent(gridParent.transform);
                    previousGenGrid[i, j].visual.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    previousGenGrid[i, j].SetColor();
                }
            }
        }      
        private void CalculateNewGrid()
        {
            for (int i = 0; i < currentGridSizeY; i++)
            {
                for (int j = 0; j < currentGridSizeX; j++)
                {
                    GetNewElementState(previousGenGrid, i,j);
                }
            }
            SetNewStatesToNewGen();
        }
        private void SetNewStatesToNewGen()
        {
            for (int i = 0; i < currentGridSizeY; i++)
            {
                for (int j = 0; j < currentGridSizeX; j++)
                {
                    previousGenGrid[i, j].currentState = (e_StateType) newStates[i,j];
                    previousGenGrid[i, j].SetColor();
                }
            }
        }
        private void GetNewElementState(VisualInfo[,] previousGenGrid, int x, int y)
        {
            int neighborCount = 0;

            if (EdgeCase(x, y))
            {
                newStates[x,y]= (int) previousGenGrid[x, y].currentState;
                return;
            }

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    neighborCount +=(int)previousGenGrid[x+i,y+j].currentState;
                }
            }

            neighborCount -= (int)previousGenGrid[x, y].currentState;

            if (IsDead(neighborCount,(int)previousGenGrid[x,y].currentState))
            {
                newStates[x, y] = 0;        
            }
            else 
            {
                newStates[x, y] = 1;
            }           
        }
        private bool EdgeCase(int x, int y)
        {
            return (x - 1 < 0 || y - 1 < 0 || x + 1 >= currentGridSizeX || y + 1 >= currentGridSizeY); 
        }
        private bool IsDead(int neighborCount, int state)
        {
            if (state==1)
            {
                if (neighborCount < 2 || neighborCount > 3)
                    return true;
                else
                    return false;
            }
            else
            {
                if (neighborCount == 3)
                    return false;
                else
                    return true;
            }
        }
        #endregion      
    }
}

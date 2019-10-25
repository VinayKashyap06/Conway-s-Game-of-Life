using System;
using UnityEngine;
using Commons;

namespace GameSystem.Controllers
{    
    public class ControllerGrid : ITickable
    {
        private GameObject prefabToUse;
        private VisualInfo[,] previousGenGrid;
        private VisualInfo[,] nextGenGrid;
        private int[,] newStates;
        private int currentGridSize;
        private GameObject gridParent;

        public void StartSimulation(int gridSize)
        {
            gridParent = new GameObject("GridParent");
            currentGridSize = gridSize;
            previousGenGrid = new VisualInfo[gridSize, gridSize];
            newStates = new int[gridSize,gridSize];
            SetRandomGrid();
            nextGenGrid = previousGenGrid;
           // Debug.Log(" Old Grid elements :");
            //PrintOldGenElements();
            //CalculateNewGrid();
            //Debug.Log(" New Grid elements :");
            //PrintNextGenElements();
        }
        public void New()
        {
            //PrintOldGenElements();
            CalculateNewGrid();
         //   Debug.Log(" New Grid elements :");
            //PrintNextGenElements();
        }
        private void PrintNextGenElements()
        {
            for (int i = 0; i < currentGridSize; i++)
            {
                for (int j = 0; j < currentGridSize; j++)
                {
                    Debug.Log("<color=green> " + nextGenGrid[i, j].currentState + "</color>");
                }
            }
        }

        public void SwitchPrefab(GameObject newPrefab)
        {
            prefabToUse = newPrefab;
        }

        private void PrintOldGenElements()
        {
            for (int i = 0; i < currentGridSize; i++)
            {
                for (int j = 0; j < currentGridSize; j++)
                {
                    Debug.Log("<color=red> " + nextGenGrid[i, j].currentState + "</color>");
                }
            }
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
                    previousGenGrid[i, j].visual.transform.SetParent(gridParent.transform);
                    previousGenGrid[i, j].visual.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    previousGenGrid[i, j].SetColor();
                }
            }
        }

        private void CalculateNewGrid()
        {
            for (int i = 0; i < currentGridSize; i++)
            {
                for (int j = 0; j < currentGridSize; j++)
                {
                    GetNewElementState(previousGenGrid, i,j);
                }
            }
            SetNewStatesToNewGen();
            previousGenGrid = nextGenGrid;
        }

        private void SetNewStatesToNewGen()
        {
            for (int i = 0; i < currentGridSize; i++)
            {
                for (int j = 0; j < currentGridSize; j++)
                {
                    nextGenGrid[i, j].currentState = (e_StateType) newStates[i,j];
                    nextGenGrid[i, j].SetColor();
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
               // nextGenGrid[x, y].currentState = e_StateType.DEAD;
                //nextGenGrid[x, y].SetColor();
            }
            else 
            {
                newStates[x, y] = 1;
                //nextGenGrid[x, y].currentState = e_StateType.ALIVE;
                //nextGenGrid[x, y].SetColor();
            }           
        }

        private bool EdgeCase(int x, int y)
        {
            return (x - 1 < 0 || y - 1 < 0 || x + 1 >= currentGridSize || y + 1 >= currentGridSize); 
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

        public void Tick()
        {
            New();
        }
    }
}

//Uncomment this to use Wraparound world
//Can be deleted and added later in player settings > script define symbols
//Considers that edge cases are definite and cannot change

#define CONSIDER_EDGE_CASE

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
        /// <summary>
        /// Set the camera accordingly
        /// Create a new grid, delete the old one.
        /// Set random values in grid
        /// </summary>
        /// <param name="gridSizeX"></param>
        /// <param name="gridSizeY"></param>
        public void StartSimulation(int gridSizeX, int gridSizeY)
        {
            Camera.main.transform.localPosition = new Vector3(Globals.GridSizeX / 2f, Globals.GridSizeY / 2f, -10f);
            if (gridParent != null)
                GameObject.Destroy(gridParent);
            gridParent = new GameObject("GridParent");
            currentGridSizeX = gridSizeX;
            currentGridSizeY = gridSizeY;
            previousGenGrid = null;
            newStates = null;
            previousGenGrid = new VisualInfo[gridSizeX, gridSizeY];
            newStates = new int[gridSizeX,gridSizeY];
            SetRandomGrid();
            simulate = true;
        }
        /// <summary>
        /// interface method ITickable called by singleton game
        /// Calculate new grid
        /// increment generation and set the text to display
        /// </summary>
        public void Tick()
        {
            if (!simulate)            
                return;            
            CalculateNewGrid();
            gen++;
            genText.text ="Gen: "+gen.ToString();

        }
        /// <summary>
        /// Switches the prefab to use in grid
        /// </summary>
        /// <param name="newPrefab"></param>
        public void SwitchPrefab(GameObject newPrefab)
        {
            prefabToUse = newPrefab;
        }
        /// <summary>
        /// Stops Simulaton
        /// Resets Generation
        /// </summary>
        public void StopSimulation()
        {
            simulate = false;
            gen = 0;
        }
        /// <summary>
        /// Setting Text to be displayed
        /// </summary>
        /// <param name="generationText"></param>
        public void SetGenText(TextMeshProUGUI generationText)
        {
            genText = generationText;
        }

        #endregion
        #region Private_Methods
        /// <summary>
        /// to print new gen elements
        /// </summary>
        private void PrintNextGenElements()
        {
            for (int i = 0; i < currentGridSizeX; i++)
            {
                for (int j = 0; j < currentGridSizeY; j++)
                {
                    Debug.Log("<color=green> " + newStates[i, j] + "</color>");
                }
            }
        }    
        /// <summary>
        /// to print old gen  elements
        /// </summary>
        private void PrintOldGenElements()
        {
            for (int i = 0; i < currentGridSizeX; i++)
            {
                for (int j = 0; j < currentGridSizeY; j++)
                {
                    Debug.Log("<color=red> " + previousGenGrid[i, j].currentState + "</color>");
                }
            }
        }
        /// <summary>
        /// Setting Random grid
        /// </summary>
        private void SetRandomGrid()
        {
            for (int i = 0; i < currentGridSizeX; i++)
            {
                for (int j = 0; j < currentGridSizeY; j++)
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
        /// <summary>
        /// Calculates new states
        /// </summary>
        private void CalculateNewGrid()
        {
            for (int i = 0; i < currentGridSizeX; i++)
            {
                for (int j = 0; j < currentGridSizeY; j++)
                {
                    GetNewElementState(previousGenGrid, i,j);
                }
            }
            SetNewStatesToNewGen();
        }
        /// <summary>
        /// Sets new states to old grid
        /// </summary>
        private void SetNewStatesToNewGen()
        {
            for (int i = 0; i < currentGridSizeX; i++)
            {
                for (int j = 0; j < currentGridSizeY; j++)
                {
                    previousGenGrid[i, j].currentState = (e_StateType) newStates[i,j];
                    previousGenGrid[i, j].SetColor();
                }
            }
        }
        /// <summary>
        /// Getting new state info depending upon the rules
        /// </summary>
        /// <param name="previousGenGrid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void GetNewElementState(VisualInfo[,] previousGenGrid, int x, int y)
        {
            int neighborCount = 0;

#if CONSIDER_EDGE_CASE

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
#else
            //Wrap around world
            //where the grid is essentially connected in a circular manner
            //usually turns out that all elements die 

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int row =(x+i+(currentGridSizeX)) %currentGridSizeX;
                    int col =(y+i+(currentGridSizeY)) %currentGridSizeY;
                    neighborCount += (int)previousGenGrid[row,col].currentState;
                }
            }
#endif
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
        /// <summary>
        /// Edge Cases Ignored
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool EdgeCase(int x, int y)
        {
            return (x - 1 < 0 || y - 1 < 0 || x + 1 >= currentGridSizeX || y + 1 >= currentGridSizeY); 
        }
        /// <summary>
        /// Rules for dying or living
        /// </summary>
        /// <param name="neighborCount"></param>
        /// <param name="state"></param>
        /// <returns></returns>
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

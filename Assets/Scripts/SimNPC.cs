using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimNPC : MonoBehaviour
{
    //[SerializeField]
    private SimGridMap map;
    [SerializeField]
    private float cellSize;

    private float movementSpeed = 1;

    private bool isBusy = false; //Whether this NPC is assigned to do something.

    private Vector3 nextDestinationPosition;
    private Vector3[] path;
    private int numCurrentPathMoves = 0;
    private int maxNumPathMoves = 100;
    private int currentPathMoveIndex = 0;
    private int pathInputIndex = 0;

    [SerializeField]
    private Rigidbody npcRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("SimMap").GetComponent<SimGridMap>();
        cellSize = map.GetCellSize();

        nextDestinationPosition = transform.position;
        path = new Vector3[maxNumPathMoves];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void FetchWater()
    {
        //Find closest water source.
        //Find a star path to it.
        //Go.
    }

    public void FetchFood()
    {
        //Find closest food source.
        //Find a star path to it.
        //Go.
    }

    public bool IsBusy()
    {
        return isBusy;
    }

    public void SetNextDestination(Vector3 nextPosition)
    {
        nextDestinationPosition = nextPosition;
    }

    //Use in conjunction with GetCellMapPosition(row, col)
    public void AddToPath(Vector3 nextDestination)
    {
        if (numCurrentPathMoves < maxNumPathMoves)
        {
            path[pathInputIndex] = nextDestination;
            Debug.Log("Added " + nextDestination + " to NPC path.");
            pathInputIndex++;
            Debug.Log("CurrentPathInputIndex (B4 check): " + pathInputIndex);
            if (pathInputIndex == maxNumPathMoves)
            {
                pathInputIndex = 0;
            }
            Debug.Log("CurrentPathInputIndex: " + pathInputIndex);
            numCurrentPathMoves++;
            Debug.Log("NumPathMoves: " + numCurrentPathMoves);
        }
    }

    public void Move()
    {
        //If not at next destination, move!
        if (transform.position.Equals(nextDestinationPosition))
        {
            Debug.Log("At destination.");
            if (numCurrentPathMoves > 0) //If there is still more moving to do.
            {
                Debug.Log("Still more in path!");
                nextDestinationPosition = path[currentPathMoveIndex]; //Set new destination.
                Debug.Log("Next destination: " + nextDestinationPosition);
                numCurrentPathMoves--; //Decrease number of paths remaining.
                Debug.Log("NumPathMoves: " + numCurrentPathMoves);
                currentPathMoveIndex++; //Move to next path move.
                Debug.Log("CurrentPathMoveIndex (B4 check): " + currentPathMoveIndex);
                if (currentPathMoveIndex == maxNumPathMoves) //If at end of path buffer, reset.
                {
                    currentPathMoveIndex = 0;
                }
                Debug.Log("CurrentPathMoveIndex: " + currentPathMoveIndex);

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextDestinationPosition, movementSpeed * Time.deltaTime);
        }
    }


    public void FindAStarPathToDestination(Vector3 destination)
    {
        //Use map's mapgrid to find best path, then use AddToPath() with all the found path.
        Vector3 startPosition = transform.position;
        //int[,] fCosts; //The f costs of that [row,col] in map. Making a data structure to hold this instead.
        //Transform[] path; 
        //-----------------------

        SimGridCell startCell = map.GetCell(map.GetCellRowFromMapPosition(startPosition), map.GetCellColFromMapPosition(startPosition));
        SimGridCell destCell = map.GetCell(map.GetCellRowFromMapPosition(destination), map.GetCellColFromMapPosition(destination));

        List<SimGridCell> openSet = new List<SimGridCell>();
        HashSet<SimGridCell> closedSet = new HashSet<SimGridCell>();
        openSet.Add(startCell);

        while( openSet.Count > 0)
        {
            SimGridCell currentCell = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].GetFCost() < currentCell.GetFCost() || openSet[i].GetFCost() == currentCell.GetFCost() && openSet[i].GetHCost() < currentCell.GetHCost())
                {
                    currentCell = openSet[i];
                }
            }
            openSet.Remove(currentCell);
            closedSet.Add(currentCell);

            if(currentCell == destCell)
            {
                RetracePath(startCell, destCell);
                return; //DONE!
            }

            foreach (SimGridCell neighbor in map.GetCellNeighbors(currentCell.GetRowPosition(), currentCell.GetColPosition()))
            {
                if( neighbor.GetPathCost() != -1 || closedSet.Contains(neighbor))
                {
                    continue;
                }
                int newMoveCostToNeighbor = currentCell.GetGCost() + GetCellDistance(currentCell, neighbor);
                if(newMoveCostToNeighbor < neighbor.GetGCost() || !openSet.Contains(neighbor))
                {
                    neighbor.SetGCost(newMoveCostToNeighbor);
                    neighbor.SetHCost(GetCellDistance(neighbor,destCell));
                    neighbor.SetParentCell(currentCell);

                    if(!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }

            }
        }

    }

    private void RetracePath(SimGridCell startCell, SimGridCell endCell)
    {
        List<SimGridCell> path = new List<SimGridCell>();
        SimGridCell currentCell = endCell;
        while(currentCell != startCell)
        {
            path.Add(currentCell);
            currentCell = currentCell.GetParentCell();
        }
        path.Reverse();

        //Add this path to NPC's path.
        foreach(SimGridCell cell in path)
        {
            AddToPath(map.GetCellMapPosition(cell.GetRowPosition(), cell.GetColPosition()));
        }
    }

    //Need to incorporate the path cost!!
    private int GetCellDistance(SimGridCell cell1, SimGridCell cell2)
    {
        
        int xDistance = Mathf.Abs(cell1.GetColPosition() - cell2.GetColPosition());
        int zDistance = Mathf.Abs(cell1.GetRowPosition() - cell2.GetRowPosition());
        if(xDistance > zDistance)
        {
            return zDistance + (xDistance - zDistance);
        }
        return xDistance + (zDistance - xDistance);

        //return Vector3.Distance(map.GetCellMapPosition(cell1.GetRowPosition(), cell1.GetColPosition()), map.GetCellMapPosition(cell2.GetRowPosition(), cell2.GetColPosition()));
    }
}

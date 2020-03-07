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

    }

    public void FetchFood()
    {

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
            if(numCurrentPathMoves > 0) //If there is still more moving to do.
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


    public void FindAStarPathToDestination(Vector3 worldDestination)
    {
        //Use map's mapgrid to find best path.
    }
}

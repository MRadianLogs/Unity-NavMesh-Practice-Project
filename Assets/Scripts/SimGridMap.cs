using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGridMap : MonoBehaviour
{
    [SerializeField]
    private int[,] mapGrid = null; //private int[][] mapGrid; //Each cell represents a cell in the map. The values are the path costs.
    [SerializeField]
    private int mapSize = -1; //How many cells (mapsize x mapsize) are in the map.
    [SerializeField]
    private int cellSize = -1; //How big each cell should be, meaning how much space it represents in the map.
    [SerializeField]
    private float cellSizeOffset = -1; //The offset to get to the middle of the cell. Half the cellSize.

    [SerializeField]
    private Transform mapZeroZeroPosition = null; //The top left position of the map. The x position is the col, z is the row.

    [SerializeField]
    private GameObject testObject = null;

    [SerializeField]
    private GameObject testNPCPrefab = null;
    private GameObject testNPC = null;

    // Start is called before the first frame update
    void Start()
    {
        mapGrid = new int[mapSize, mapSize]; //mapGrid = new int[mapSize][];
        cellSizeOffset = (float)cellSize / 2; //We want the offset to put items in the middle(half) of a cell.

        //Pathing costs:
        //Grass is 3; (This is null in the grid in order to save performace of running through the whole grid.)
        //Path is 1.
        //House is -1. Not allowed.
        //Store is -1. Not allowed.
        //Well is -1. Not allowed.
        
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("SimPath"))
        {
            int row = GetCellRowFromMapPosition(item.transform.position);
            int col = GetCellColFromMapPosition(item.transform.position);
            mapGrid[row,col] = 1;
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("SimHouse"))
        {
            int row = GetCellRowFromMapPosition(item.transform.position);
            int col = GetCellColFromMapPosition(item.transform.position);
            mapGrid[row,col] = -1;
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("SimStore"))
        {
            int row = GetCellRowFromMapPosition(item.transform.position);
            int col = GetCellColFromMapPosition(item.transform.position);
            mapGrid[row,col] = -1;
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("SimWell"))
        {
            //Debug.Log("Item: " + item.name);
            int row = GetCellRowFromMapPosition(item.transform.position);
            //Debug.Log("Row: " + row);
            int col = GetCellColFromMapPosition(item.transform.position);
            //Debug.Log("Col: " + col);
            mapGrid[row,col] = -1;
        }
        
        /*
        foreach (GameObject gridItem in gridItemsFolder.transform.)
        {
            int row = GetCellRowFromMapPosition(gridItem.transform.position);
            int col = GetCellColFromMapPosition(gridItem.transform.position);
            string itemTag = gridItem.tag;

            switch (itemTag)
            {
                case "SimPath":
                    mapGrid[row][col] = 0;
                    break;
                case "SimHouse":
                    mapGrid[row][col] = 1;
                    break;
                case "SimStore":
                    mapGrid[row][col] = 2;
                    break;
                case "SimWell":
                    mapGrid[row][col] = 3;
                    break;
            }
        }
        */

        /*
        //Testing cell placements.
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Instantiate(testObject, new Vector3((mapZeroZeroPosition.position.x + ((cellSize * j) + cellSizeOffset)), 0, (mapZeroZeroPosition.position.z - ((cellSize * i) + cellSizeOffset))), mapZeroZeroPosition.rotation);
            }
        }
        
        //Instantiate(testObject, new Vector3((mapZeroZeroPosition.position.x + ((cellSize * 0) + cellSizeOffset)), 0, (mapZeroZeroPosition.position.z - ((cellSize * 0) + cellSizeOffset))), mapZeroZeroPosition.rotation);
        //Instantiate(testObject, GetCellMapPosition(0,1), mapZeroZeroPosition.rotation);
        InsertObjectInMapAtPosition(testObject, 0,0);
        InsertObjectInMapAtPosition(testObject, 1, 2);
        */

        //testNPC = InsertReturnObjectInMapAtPosition(testNPCPrefab, 0, 0);

        //Debug.Log("Test object in row: " + GetCellsFromMapPosition(testObject.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Time.fixedTime == 5f)
        {
            Debug.Log("Asking NPC to move.");
            testNPC.GetComponent<SimNPC>().AddToPath(GetCellMapPosition(1,1));
            testNPC.GetComponent<SimNPC>().AddToPath(GetCellMapPosition(1, 2));
            //testNPC.GetComponent<SimNPC>().ShiftPositionUp();
        }
        */
        //Debug.Log("Test object row,col: " + GetCellRowFromMapPosition(testObject.transform.position) + ", " +GetCellColFromMapPosition(testObject.transform.position));


    }

    public int GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetCellMapPosition(int row, int col)
    {
        return new Vector3((mapZeroZeroPosition.position.x + ((cellSize * col) + cellSizeOffset)), 0, (mapZeroZeroPosition.position.z - ((cellSize * row) + cellSizeOffset)));
    }

    public int GetCellRowFromMapPosition(Vector3 mapPosition)
    {
        int row = (int) Mathf.Round(mapPosition.z - mapZeroZeroPosition.position.z + cellSizeOffset) / (-cellSize);
        return row;
    }
    public int GetCellColFromMapPosition(Vector3 mapPosition)
    {
        int col = (int) Mathf.Round(mapPosition.x - mapZeroZeroPosition.position.x - cellSizeOffset) / (cellSize);
        return col;
    }

    public void InsertObjectInMapAtPosition(GameObject newObject, int row, int col)
    {
        Instantiate(newObject, GetCellMapPosition(row, col), this.transform.rotation);
    }

    public GameObject InsertReturnObjectInMapAtPosition(GameObject newObject, int row, int col)
    {
        return Instantiate(newObject, GetCellMapPosition(row, col), mapZeroZeroPosition.rotation);
    }

    public int GetCostOfGridAtPosition(int row, int col)
    {
        return mapGrid[row,col];
    }
}

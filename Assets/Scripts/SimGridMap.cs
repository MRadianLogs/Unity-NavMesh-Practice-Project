using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGridMap : MonoBehaviour
{
    [SerializeField]
    private int[][] mapGrid; //Each cell represents a cell in the map.
    [SerializeField]
    private int mapSize; //How many cells (mapsize x mapsize) are in the map.
    [SerializeField]
    private int cellSize; //How big each cell should be, meaning how much space it represents in the map.
    private float cellSizeOffset; //The offset to get to the middle of the cell. Half the cellSize.

    [SerializeField]
    private Transform mapZeroZeroPosition; //The top left position of the map. The x position is the col, z is the row.

    [SerializeField]
    private GameObject testObjectPrefab;
    [SerializeField]
    private GameObject testNPCPrefab;
    private GameObject testNPC;

    // Start is called before the first frame update
    void Start()
    {
        mapGrid = new int[mapSize][];
        cellSizeOffset = (float)cellSize / 2;

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

        testNPC = InsertReturnObjectInMapAtPosition(testNPCPrefab, 1, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime == 5f)
        {
            Debug.Log("Asking NPC to move.");
            testNPC.GetComponent<SimNPC>().ShiftPositionUp();
        }
        
        
        
    }

    public int GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetCellMapPosition(int row, int col)
    {
        return new Vector3((mapZeroZeroPosition.position.x + ((cellSize * col) + cellSizeOffset)), 0, (mapZeroZeroPosition.position.z - ((cellSize * row) + cellSizeOffset)));
    }

    public void InsertObjectInMapAtPosition(GameObject newObject, int row, int col)
    {
        Instantiate(newObject, GetCellMapPosition(row, col), mapZeroZeroPosition.rotation);
    }

    public GameObject InsertReturnObjectInMapAtPosition(GameObject newObject, int row, int col)
    {
        return Instantiate(newObject, GetCellMapPosition(row, col), mapZeroZeroPosition.rotation);
    }
}

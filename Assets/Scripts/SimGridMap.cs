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
    [SerializeField]
    private float cellSizeOffset; //The offset to get to the middle of the cell. Half the cellSize.

    [SerializeField]
    private Transform mapZeroZeroPosition; //The top left position of the map.

    [SerializeField]
    private GameObject testObject;

    // Start is called before the first frame update
    void Start()
    {
        mapGrid = new int[mapSize][];

        //Testing cell placements.
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Instantiate(testObject, new Vector3((mapZeroZeroPosition.position.x + ((cellSize * j) + cellSizeOffset)), 0, (mapZeroZeroPosition.position.z - ((cellSize * i) + cellSizeOffset))), mapZeroZeroPosition.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

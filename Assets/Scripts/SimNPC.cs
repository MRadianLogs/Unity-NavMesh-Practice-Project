using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimNPC : MonoBehaviour
{
    //[SerializeField]
    private SimGridMap map;
    [SerializeField]
    private float cellSize;

    private bool isBusy; //Whether this NPC is assigned to do something.

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("SimMap").GetComponent<SimGridMap>();
        cellSize = map.GetCellSize();
    }

    // Update is called once per frame
    void Update()
    {
        
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


    //Change to moving towards position over time! A star will be repeating those methods, following best path, until at end!
    public void ShiftPositionUp()
    {
        //Use rigidbody translate? To move npc to new position? Or just changed position?
        //transform.Translate(0,0, cellSize, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cellSize);
        Debug.Log("NPC shifted up!");
    }

    public void ShiftPositionDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cellSize);
    }

    public void ShiftPositionLeft()
    {
        transform.position = new Vector3(transform.position.x - cellSize, transform.position.y, transform.position.z);
    }

    public void ShiftPositionRight()
    {
        transform.position = new Vector3(transform.position.x + cellSize, transform.position.y, transform.position.z + cellSize);
    }
}

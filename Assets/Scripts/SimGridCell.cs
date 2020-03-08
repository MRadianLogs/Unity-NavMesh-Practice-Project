using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimGridCell
{
    private int rowPosition;
    private int colPosition;
    private int pathCost;

    private SimGridCell parentCell; //Used for storing the a star path.

    private int gCost;
    private int hCost;

    public SimGridCell(int cellPathCost, int cellRowPosition, int cellColPosition)
    {
        rowPosition = cellRowPosition;
        colPosition = cellColPosition;
        pathCost = cellPathCost;
    }

    public SimGridCell GetParentCell()
    {
        return parentCell;
    }
    public void SetParentCell(SimGridCell newParentCell)
    {
        parentCell = newParentCell;
    }

    public int GetRowPosition()
    {
        return rowPosition;
    }
    public int GetColPosition()
    {
        return colPosition;
    }
    public int GetPathCost()
    {
        return pathCost;
    }
    public int GetGCost()
    {
        return gCost;
    }
    public void SetGCost(int newGCost)
    {
        gCost = newGCost;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public void SetHCost(int newHCost)
    {
        hCost = newHCost;
    }
    public int GetFCost()
    {
        return gCost + hCost;
    }
}

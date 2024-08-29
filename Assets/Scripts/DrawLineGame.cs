using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineGame : MonoBehaviour
{
    public bool minigameFinished = false;
    public int finishNumber = -1;
    public int currentNumber = 0;

    public int gridWidth = -1;
    public int gridHeight = -1;

    public Tile currentTile = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentNumber == finishNumber)
        {
            minigameFinished = true;
        }
    }

    public void RemoveCurrentConnection(int tileID)
    {
        int countRemoved = 0;
        foreach (Transform tile in this.transform)
        {
            Tile tileObj = tile.GetComponent<Tile>();
            if (tileObj.tileID == tileID)
            {
                if (!tileObj.colorEnd)
                {
                    tileObj.tileID = 0;
                    countRemoved++;
                }
                tileObj.previousTile = null;
                tileObj.nextTile = null;
            }
        }

        if(currentTile == null && countRemoved > 0)
        {
            currentNumber--;
        }
    }
}

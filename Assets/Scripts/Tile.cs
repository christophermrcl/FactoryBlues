using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public int tileID = 0;
    public int horizontalAddress = -1;
    public int verticalAddress = -1;
    public bool colorEnd = false;
    public GameObject minigameManager;

    private DrawLineGame manager;

    private GameObject topLine;
    private GameObject bottomLine;
    private GameObject leftLine;
    private GameObject rightLine;
    private GameObject dot;

    public Tile previousTile;
    public Tile nextTile;
    private void Awake()
    {
        manager = minigameManager.GetComponent<DrawLineGame>();

        topLine = gameObject.transform.Find("Top").gameObject;
        bottomLine = gameObject.transform.Find("Bottom").gameObject;
        leftLine = gameObject.transform.Find("Left").gameObject;
        rightLine = gameObject.transform.Find("Right").gameObject;
        dot = gameObject.transform.Find("Dot").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (colorEnd)
        {
            dot.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
        LineHandler();
    }

    void ColorChange()
    {
        Color color;
        if (tileID == 1)
        {
            color = Color.red;
        }
        else if (tileID == 2)
        {
            color = Color.yellow;
        }
        else if (tileID == 3)
        {
            color = Color.blue;
        }
        else if (tileID == 4)
        {
            color = Color.green;
        }
        else if (tileID == 5)
        {
            color = new Color(255f / 255f, 133f / 255f, 0f, 255f);
        }
        else if (tileID == 6)
        {
            color = Color.magenta;
        }
        else
        {
            color = Color.white;
        }

        topLine.GetComponent<Image>().color = color;
        rightLine.GetComponent<Image>().color = color;
        leftLine.GetComponent<Image>().color = color;
        bottomLine.GetComponent<Image>().color = color;
        dot.GetComponent<Image>().color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (colorEnd)
        {
            if(manager.currentTile != null)
            {
                manager.RemoveCurrentConnection(manager.currentTile.tileID);
            }

            manager.RemoveCurrentConnection(tileID);

            manager.currentTile = this;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        if(manager.currentTile != null && Input.GetMouseButton(0))
        {
            if ((this.verticalAddress == manager.currentTile.verticalAddress + 1 && this.horizontalAddress == manager.currentTile.horizontalAddress) ||
            (this.verticalAddress == manager.currentTile.verticalAddress - 1 && this.horizontalAddress == manager.currentTile.horizontalAddress) ||
            (this.verticalAddress == manager.currentTile.verticalAddress && this.horizontalAddress == manager.currentTile.horizontalAddress + 1) ||
            (this.verticalAddress == manager.currentTile.verticalAddress && this.horizontalAddress == manager.currentTile.horizontalAddress - 1))
            {
                if (!colorEnd && tileID == 0)
                {
                    tileID = manager.currentTile.tileID;
                    manager.currentTile.nextTile = this;
                    previousTile = manager.currentTile;
                    manager.currentTile = this;
                }else if(this == manager.currentTile.previousTile)
                {
                    manager.currentTile.tileID = 0;
                    manager.currentTile.previousTile = null;
                    nextTile = null;
                    manager.currentTile = this;
                }else if (colorEnd)
                {
                    if(tileID == manager.currentTile.tileID)
                    {
                        manager.currentNumber++;
                        manager.currentTile.nextTile = this;
                        previousTile = manager.currentTile;
                        manager.currentTile = null;
                    }
                }
            }
        }
    }

    public void LineHandler()
    {
        if (horizontalAddress < manager.gridWidth - 1)
        {
            if (nextTile != null && nextTile.horizontalAddress == this.horizontalAddress + 1 && nextTile.verticalAddress == this.verticalAddress)
            {
                rightLine.SetActive(true);
            }
            else if (previousTile != null && previousTile.horizontalAddress == this.horizontalAddress + 1 && previousTile.verticalAddress == this.verticalAddress)
            {
                rightLine.SetActive(true);
            }
            else
            {
                rightLine.SetActive(false);
            }
        }
        else
        {
            rightLine.SetActive(false);
        }

        if (horizontalAddress > 0)
        {
            if (nextTile != null && nextTile.horizontalAddress == this.horizontalAddress - 1 && nextTile.verticalAddress == this.verticalAddress)
            {
                leftLine.SetActive(true);
            }
            else if (previousTile != null && previousTile.horizontalAddress == this.horizontalAddress - 1 && previousTile.verticalAddress == this.verticalAddress)
            {
                leftLine.SetActive(true);
            }
            else
            {
                leftLine.SetActive(false);
            }
        }
        else
        {
            leftLine.SetActive(false);
        }

        if (verticalAddress < manager.gridHeight - 1)
        {
            if (nextTile != null && nextTile.verticalAddress == this.verticalAddress + 1 && nextTile.horizontalAddress == this.horizontalAddress)
            {
                bottomLine.SetActive(true);
            }
            else if (previousTile != null && previousTile.verticalAddress == this.verticalAddress + 1 && previousTile.horizontalAddress == this.horizontalAddress)
            {
                bottomLine.SetActive(true);
            }
            else
            {
                bottomLine.SetActive(false);
            }
        }
        else
        {
            bottomLine.SetActive(false);
        }

        if (verticalAddress > 0)
        {
            if (nextTile != null && nextTile.verticalAddress == this.verticalAddress - 1 && nextTile.horizontalAddress == this.horizontalAddress)
            {
                topLine.SetActive(true);
            }
            else if (previousTile != null && previousTile.verticalAddress == this.verticalAddress - 1 && previousTile.horizontalAddress == this.horizontalAddress)
            {
                topLine.SetActive(true);
            }
            else
            {
                topLine.SetActive(false);
            }
        }
        else
        {
            topLine.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Item
{
    public int id;
    public int amount;
}

public class InventorySystem : MonoBehaviour
{
    public Item[] item;
    public GameObject[] prefab;
    public GameObject Group;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateVisual()
    {
        for (int i = Group.transform.childCount - 1; i >= 0; --i)
        {
            Destroy(Group.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < item.Count(); i++)
        {
            if (item[i].amount > 0)
            {
                for(int j = 0; j < item[i].amount; j++)
                {
                    Instantiate(prefab[i], Group.transform);
                }
            }
        }
    }
}

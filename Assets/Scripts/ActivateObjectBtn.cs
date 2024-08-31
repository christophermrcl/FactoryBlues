using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject affectedObj = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if(affectedObj.active == true)
        {
            affectedObj.SetActive(false);
        }
        else
        {
            affectedObj.SetActive(true);
        }
        
    }
}

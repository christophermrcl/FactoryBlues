using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    public bool isDialogueActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueCanvas.active == true)
        {
            isDialogueActive = true;
        }
        else
        {
            isDialogueActive = false;
        }
    }
}

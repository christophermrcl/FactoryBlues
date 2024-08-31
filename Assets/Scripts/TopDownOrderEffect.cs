using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownOrderEffect : MonoBehaviour
{
    private SpriteRenderer sprite;
    private GameObject playerObject;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerSprite = playerObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObject.transform.position.y < this.transform.position.y)
        {
            sprite.sortingOrder = playerSprite.sortingOrder - 1;
        }
        else
        {
            sprite.sortingOrder = playerSprite.sortingOrder + 1;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    bool isNearPlayer = false;
    bool isDialogueActive = false;

    public static event Action<Story> OnCreateStory;

    private GameObject playerObject;
    private SpriteRenderer sprite;
    void Awake()
    {
        // Remove the default message
        RemoveChildren();
        //StartStory();
    }

    // Creates a new Story object with the compiled story which we can then play!
    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();



        // Read all the content until we can't continue any more
        //while (story.canContinue)
        //{
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();

        List<string> tags = story.currentTags;

        if(tags.Count > 0)
        {
            puzzleCanvas.transform.Find(tags[0]).gameObject.SetActive(true);
        }

        if (text == "END")
        {
            isDialogueActive = false;
            RemoveChildren();
            canvasFull.SetActive(false);
            Debug.Log("story finish");
            return;
        }

        // Display the text on screen!
        CreateContentView(text);
        //}

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                GameObject button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.GetComponent<Button>().onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        /*else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate {
                StartStory();
            });
        }*/

        
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        GameObject storyText = Instantiate(textPrefab) as GameObject;
        storyText.GetComponent<TextMeshProUGUI>().text = text;
        storyText.transform.SetParent(textCanvas.transform, false);
    }


    // Creates a button showing the choice text
    GameObject CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        GameObject choice = Instantiate(buttonPrefab) as GameObject;
        choice.transform.SetParent(buttonCanvas.transform, false);

        // Gets the text from the button prefab
        TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        // Make the button expand to fit the text
        //HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        //layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCountButton = buttonCanvas.transform.childCount;
        for (int i = childCountButton - 1; i >= 0; --i)
        {
            Destroy(buttonCanvas.transform.GetChild(i).gameObject);
        }

        int childCountText = textCanvas.transform.childCount;
        for (int i = childCountText - 1; i >= 0; --i)
        {
            Destroy(textCanvas.transform.GetChild(i).gameObject);
        }
    }
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isNearPlayer)
        {
            if (Input.GetKeyUp(KeyCode.E) && !isDialogueActive)
            {
                canvasFull.SetActive(true);
                isDialogueActive = true;
                StartStory();
            }

            if (Input.GetMouseButtonUp(0) && isDialogueActive && story.canContinue)
            {
                RefreshView();
            }
        }

        if(playerObject.transform.position.x < this.transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX=false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    [SerializeField]
    private GameObject puzzleCanvas = null;

    [SerializeField]
    private GameObject canvasFull = null;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas textCanvas = null;
    [SerializeField]
    private Canvas buttonCanvas = null;

    // UI Prefabs
    [SerializeField]
    private GameObject textPrefab = null;
    [SerializeField]
    private GameObject buttonPrefab = null;
}

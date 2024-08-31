using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBtn : MonoBehaviour
{
    // The name of the scene you want to load
    public string sceneName;

    // Call this method when the button is pressed
    public void ChangeScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}

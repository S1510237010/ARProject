using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateToScene : MonoBehaviour {

	//Navigate to Scene by ID
    public static void GoToScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    //Navigate to Scene by Name
    public static void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

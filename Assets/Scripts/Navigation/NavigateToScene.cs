using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateToScene : MonoBehaviour {

	/// <summary>
	/// Navigate to a Scene by ID
	/// </summary>
	/// <param name="sceneId">Scene identifier.</param>
    public static void GoToScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

	/// <summary>
	/// Navigate to a Scene by name
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
    public static void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

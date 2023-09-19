using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        if(sceneName == null)
        {
            Debug.LogError(gameObject.name + " has no named scene");
        }
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}

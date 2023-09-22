using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public SceneChange sceneChange;

    void Start()
    {      
        if(!sceneChange.LevelCompleted)
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneChange.SceneName);
    }
}

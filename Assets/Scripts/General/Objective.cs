using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    public SceneChange currentLevel;
    public SceneChange nextLevel;
    public string nextScene;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit " + other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            currentLevel.LevelCompleted = true;
            nextLevel.LevelActive = true;
            SceneManager.LoadScene(nextScene);
        }
    }
}

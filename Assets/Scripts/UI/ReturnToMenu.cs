using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void OnLobbyButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}

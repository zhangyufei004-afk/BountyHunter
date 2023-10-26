using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void OnMenuButton()
    {
        StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        SceneManager.LoadScene("Lobby");
    }
    public void OnLobbyButton()
    {
        StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        SceneManager.LoadScene("Lobby");
    }
}

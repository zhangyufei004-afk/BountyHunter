using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public Money money;
    public void StartButton()
    {
        money.MoneyValue = 0;
        SceneManager.LoadScene("Lobby");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}

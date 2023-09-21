using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("Variables")]
    public int health;
    public int maxHealth = 6;

    [Header("UI")]
    public GameObject[] heartsUI;
    public GameObject gameOverPanel;

    [Header("Input Actions")]
    public InputActionReference takeDamageReference;

    void OnEnable()
    {
        takeDamageReference.action.performed += TakeDamageRef;
    }
    
    void OnDisable()
    {
        takeDamageReference.action.performed -= TakeDamageRef;
    }


    void Start()
    {
        health = maxHealth;
        gameOverPanel.SetActive(false);
        for(int i = 0; i < heartsUI.Length; i++)
        {
            heartsUI[i].SetActive(false);
        }
        heartsUI[health].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameOverPanel.SetActive(true);
    }

    public void TakeDamage()
    {
        heartsUI[health].SetActive(false);
        health--;
        heartsUI[health].SetActive(true);
    }

    private void TakeDamageRef(InputAction.CallbackContext context)
    {
        TakeDamage();
    }
}

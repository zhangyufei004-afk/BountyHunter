using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("Variables")]
    public int health;
    public int maxHealth = 6;
    public GameObject spawnpoint;
    public AudioSource hurtSound;

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

    public void TakeDamage(float damage)
    {
        for(int i = 0; i < damage; i++)
        {
            heartsUI[health].SetActive(false);
            hurtSound.Play();
            health--;
            heartsUI[health].SetActive(true);
        }
    }

    private void TakeDamageRef(InputAction.CallbackContext context)
    {
        TakeDamage(1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            gameObject.transform.position = spawnpoint.transform.position;
            TakeDamage(1);
        }
    }
}

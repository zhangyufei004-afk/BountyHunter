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
    public AudioSource heartSound;
    public bool isDead;

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
        isDead = false;
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
        isDead = true;
        gameOverPanel.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        if(gameObject.GetComponent<PlayerHealth>().isDead)
        {
            return;
        }
        // if(gameObject.GetComponent<PlayerMovement>().isDashing)
        // {
        //     return;
        // }
        hurtSound.Play();
        for(int i = 0; i < damage; i++)
        {
            if(health < 0)
            {
                break;
            }
            heartsUI[health].SetActive(false);
            health--;
            if(health < 0)
            {
                break;
            }
            heartsUI[health].SetActive(true);
        }
    }

    private void TakeDamageRef(InputAction.CallbackContext context)
    {
        TakeDamage(1);
    }

    public void ResetHealth()
    {
        health = maxHealth;
        heartSound.Play();
        for(int i = 0; i < heartsUI.Length; i++)
        {
            heartsUI[i].SetActive(false);
        }
        heartsUI[health].SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
            gameObject.transform.position = spawnpoint.transform.position;
            TakeDamage(1);
        }
    }
}

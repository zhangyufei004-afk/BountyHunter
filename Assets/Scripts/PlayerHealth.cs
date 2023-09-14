using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] heartsUI;
    public int health;
    public int maxHealth = 6;

    public GameObject gameOverPanel;
    // Start is called before the first frame update
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

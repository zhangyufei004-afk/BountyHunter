using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject winMenu;
    // Start is called before the first frame update
    void Start()
    {
        winMenu.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit " + other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            winMenu.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerNextLine : MonoBehaviour
{
    public bool activatedOnce = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            DialogueManager.GetInstance().ContinueStory();
            if(activatedOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

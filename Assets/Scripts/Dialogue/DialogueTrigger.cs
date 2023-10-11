using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private TextAsset inkJSON;
    public bool activatedOnce = false;

    void Awake()
    {
        playerInRange = false;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !DialogueManager.GetInstance().isPlayingDialogue)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            if(activatedOnce == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

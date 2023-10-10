using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool isPlayingDialogue { get; private set; }


    private static DialogueManager instance;
    public InputActionReference submitReference;
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        isPlayingDialogue = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if(!isPlayingDialogue)
        {
            return;
        }

        if(submitReference.action.IsPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isPlayingDialogue = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        isPlayingDialogue = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

}

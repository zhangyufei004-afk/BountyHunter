using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.04f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    private Story currentStory;
    public bool isPlayingDialogue { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;
    public InputActionReference submitReference;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    

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
        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        isPlayingDialogue = false;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if(!isPlayingDialogue)
        {
            return;
        }

        if(canContinueToNextLine && submitReference.action.triggered)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isPlayingDialogue = true;
        dialoguePanel.SetActive(true);

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

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
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";

        continueIcon.SetActive(false);

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            // if(submitReference.action.triggered)
            // {
            //     dialogueText.text = line;
            //     break;
            // }

            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if(letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }   

        continueIcon.SetActive(true);
        canContinueToNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(":");
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be parsed correctly");
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled");
                    break;
            }
        }
    }
}

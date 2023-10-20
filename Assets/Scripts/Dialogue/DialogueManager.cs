using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    private bool triggeredByObject = false;
    private bool canSkip = false;
    private bool submitSkip = false;
    private bool canExit = false;
    

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;
    public InputActionReference submitReference;
    public InputActionReference exitReference;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string MOVEMENT_TAG = "movement";
    private const string TRIGGER_TAG = "trigger";
    private const string CHANGE_SCENE_TAG = "scene";
    private const string VISIBILITY_TAG = "visiblity";


    

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
        if(exitReference.action.triggered && canExit)
        {
            List<string> tags = new List<string> {"movement: unlocked"};
            HandleTags(tags);
            StartCoroutine(ExitDialogueMode());
        }

        if(submitReference.action.triggered)
        {
            submitSkip = true;
        }

        if(!isPlayingDialogue)
        {
            return;
        }

        if(canContinueToNextLine && submitReference.action.triggered && !triggeredByObject)
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

    public void ContinueStory()
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
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);

        submitSkip = false;
        canSkip = false;
        canContinueToNextLine = false;

        StartCoroutine(CanSkip());

        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            if(submitSkip && canSkip)
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if(letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }

        }   
        if(!triggeredByObject)
        {
            continueIcon.SetActive(true);
        }
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
                case MOVEMENT_TAG:
                    if(tagValue == "locked")
                    {
                        if(PlayerManager.instance != null)
                        {
                            canExit = true;
                            PlayerManager.instance.player.GetComponent<PlayerMovement>().canMove = false;
                            PlayerManager.instance.player.GetComponent<Shoot>().canMove = false;
                        }
                    }
                    else
                    {
                        if(PlayerManager.instance != null)
                        {
                            canExit = false;
                            PlayerManager.instance.player.GetComponent<PlayerMovement>().canMove = true;
                            PlayerManager.instance.player.GetComponent<Shoot>().canMove = true;
                        }
                    }
                    break;
                case TRIGGER_TAG:
                    if(tagValue == "object")
                    {
                        triggeredByObject = true;
                        continueIcon.SetActive(false);
                    }
                    else
                    {
                        triggeredByObject = false;
                        continueIcon.SetActive(true);
                    }
                    break;
                case CHANGE_SCENE_TAG:
                    SceneManager.LoadScene(tagValue);
                    break;
                case VISIBILITY_TAG:
                    if(tagValue == "visible")
                    {
                        dialoguePanel.SetActive(true);
                    }
                    else if(tagValue == "invisible")
                    {
                        dialoguePanel.SetActive(false);
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tagKey);
                    break;
            }
        }
    }

    IEnumerator CanSkip()
    {
        canSkip = false;
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }

}

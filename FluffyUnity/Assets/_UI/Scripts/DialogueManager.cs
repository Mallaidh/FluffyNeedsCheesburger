using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct DialogueInfo
{
    public Sprite CharacterFace;
    public string[] StuffToSay;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public bool IsActive;
    public Image FaceImage;
    public Text DialogueText;
    private CanvasGroup group;
    private DialogueInfo CurrentDialogue;
    private int CurrentDialogueIndex;
    private bool DialogueJustStarted;
    private SayStuff talkingTo;

	void Start ()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        group = GetComponent<CanvasGroup>();
        ChangeActivation(false);
	}

    private void Update()
    {
        if (IsActive && Input.GetButtonDown("ProgressDialogue") && !DialogueJustStarted)
        {
            NextDialogue();
        }
        DialogueJustStarted = false;
    }

    public void NextDialogue()
    {
        CurrentDialogueIndex++;
        if (CurrentDialogue.StuffToSay.Length > CurrentDialogueIndex)
        {
            DialogueText.text = CurrentDialogue.StuffToSay[CurrentDialogueIndex];
        }
        else
        {
            talkingTo.DialogueEnded();
            ChangeActivation(false);
        }
    }

    public void StartDialogue(DialogueInfo info, SayStuff ss)
    {
        talkingTo = ss;
        ChangeActivation(true);
        CurrentDialogue = info;
        CurrentDialogueIndex = 0;
        DialogueText.text = info.StuffToSay[0];
        FaceImage.sprite = info.CharacterFace;
        DialogueJustStarted = true;
    }

    private void ChangeActivation(bool activate)
    {
        if (activate)
        {
            group.alpha = 1;
        }
        else
        {
            group.alpha = 0;
        }
        IsActive = activate;
        group.interactable = activate;
        group.blocksRaycasts = activate;
    }
}

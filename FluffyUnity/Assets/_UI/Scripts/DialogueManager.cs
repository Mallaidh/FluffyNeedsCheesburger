using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct DialogueInfo
{
    public Sprite PlayerFace;
    public Sprite PlayerBox;
    public Sprite CharacterFace;
    public Sprite BoxSprite;
    public string[] StuffToSay;
}

[Serializable]
public struct AllDialogueInfo
{
    public Sprite PlayerFace;
    public Sprite PlayerBox;
    public Sprite CharacterFace;
    public Sprite BoxSprite;
    public Conversation[] AllConversations;
}

[Serializable]
public struct Conversation
{
    public string[] StuffToSay;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public bool IsActive;
    public Image FaceImage;
    private Image BoxImage;
    public Text DialogueText;
    private CanvasGroup group;
    private DialogueInfo CurrentDialogue;
    private int CurrentDialogueIndex;
    private bool DialogueJustStarted;
    private SayStuff talkingTo;

	void Start ()
    {
        BoxImage = GetComponent<Image>();
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
            if (CurrentDialogue.StuffToSay[CurrentDialogueIndex].StartsWith("*"))
            {
                DialogueText.text = CurrentDialogue.StuffToSay[CurrentDialogueIndex].Substring(1);
                FaceImage.sprite = CurrentDialogue.PlayerFace;
                BoxImage.sprite = CurrentDialogue.PlayerBox;
            }
            else
            {
                DialogueText.text = CurrentDialogue.StuffToSay[CurrentDialogueIndex];
                FaceImage.sprite = CurrentDialogue.CharacterFace;
                BoxImage.sprite = CurrentDialogue.BoxSprite;
            }
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
        if (info.StuffToSay[0].StartsWith("*"))
        {
            DialogueText.text = info.StuffToSay[0].Substring(1);
            FaceImage.sprite = info.PlayerFace;
            BoxImage.sprite = info.PlayerBox;
        }
        else
        {
            DialogueText.text = info.StuffToSay[0];
            FaceImage.sprite = info.CharacterFace;
            BoxImage.sprite = info.BoxSprite;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayStuff : MonoBehaviour
{
    public AllDialogueInfo dialogueInfo;
    private int TimesTalkedTo = 0;
    public float DistanceToShowButton;
    public GameObject ButtonImage;
    private FluffyMoveCharacer player;
    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<FluffyMoveCharacer>();
	}
	
	void Update ()
    {
        ButtonImage.SetActive(Vector3.Distance(player.transform.position, transform.position) < DistanceToShowButton);
        if (Input.GetButtonDown("ProgressDialogue") && !DialogueManager.instance.IsActive && ButtonImage.activeSelf)
        {
            anim.SetBool("Talking", true);
            player.Animator.SetBool("Talking", true);
            DialogueInfo di = new DialogueInfo
            {
                StuffToSay = dialogueInfo.AllConversations[TimesTalkedTo].StuffToSay,
                PlayerFace = dialogueInfo.PlayerFace,
                PlayerBox = dialogueInfo.PlayerBox,
                BoxSprite = dialogueInfo.BoxSprite,
                CharacterFace = dialogueInfo.CharacterFace
            };
            DialogueManager.instance.StartDialogue(di, this);
        }
	}

    public void DialogueEnded()
    {
        TimesTalkedTo++;
        if(TimesTalkedTo>= dialogueInfo.AllConversations.Length)
        {
            TimesTalkedTo = 0;
        }
        player.Animator.SetBool("Talking", false);
        anim.SetBool("Talking", false);
    }
}

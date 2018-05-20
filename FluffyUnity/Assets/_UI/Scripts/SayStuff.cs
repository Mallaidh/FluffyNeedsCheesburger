using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayStuff : MonoBehaviour
{
    public DialogueInfo dialogueInfo;
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
            DialogueManager.instance.StartDialogue(dialogueInfo, this);
        }
	}

    public void DialogueEnded()
    {
        player.Animator.SetBool("Talking", false);
        anim.SetBool("Talking", false);
    }
}

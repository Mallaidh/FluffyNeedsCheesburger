using GeekyMonkeyHelpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyMoveCharacer : MonoBehaviour {

    public float MoveSpeed = 1;
    public float GravityScale = 1;
    public float JumpForce = 10;
    public float JumpDeceleration = 10;
    public Transform Fluffy;
    public Animator Animator;
    public float minZ = .714f;
    public float maxZ = 7.71f;
    public AudioSource JumpSound;

    private CharacterController charController;
    private float JumpMomentum = 0;

    // Use this for initialization
    void Start () {
        charController = GetComponent<CharacterController>();
        Animator = Fluffy.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {

        // X + Z Movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);

        // Jumping or falling
        if (JumpMomentum > 0 || !charController.isGrounded) {
            // Gravity
            JumpMomentum -= Time.deltaTime * JumpDeceleration;
            move.y = JumpMomentum;
        }

        if (move.x < 0)
        {
            Fluffy.localScale = new Vector3(Mathf.Abs(Fluffy.localScale.x) * -1, Fluffy.localScale.y, Fluffy.localScale.z);
        }
        else if (move.x > 0)
        {
            Fluffy.localScale = new Vector3(Mathf.Abs(Fluffy.localScale.x), Fluffy.localScale.y, Fluffy.localScale.z);
        }

        bool onGround = charController.isGrounded;//JumpMomentum <= 0;

        if (!DialogueManager.instance.IsActive) //Don't move when there is dialogue on screen
        {
            // Jump if on ground
            if (onGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    JumpMomentum = JumpForce;
                    Animator.SetTrigger("Jump");
                    JumpSound.Play();
                }
            }
            charController.Move(move);
        }


        // Keep him on the road
        if (transform.position.z > maxZ)
        {
            transform.position = transform.position.SetZ(maxZ);
        }
        if (transform.position.z < minZ)
        {
            transform.position = transform.position.SetZ(minZ);
        }

        // Animations
        if (Mathf.Abs(charController.velocity.x) > 0.001 || Mathf.Abs(charController.velocity.z) > 0.001)
        {
            Animator.SetBool("Walking", true);
        } else
        {
            Animator.SetBool("Walking", false);
        }
    }
}

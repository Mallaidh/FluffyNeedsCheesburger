using GeekyMonkeyHelpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyMoveCharacer : MonoBehaviour {

    private CharacterController charController;
    public float MoveSpeed = 1;
    public float GravityScale = 1;
    public float JumpForce = 10;
    public Transform Fluffy;
    public Animator Animator;

    // Use this for initialization
    void Start () {
        charController = GetComponent<CharacterController>();
        Animator = Fluffy.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, (Physics.gravity.y * GravityScale * Time.deltaTime), Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = new Vector3(move.x, -JumpForce, move.z);
            this.Animator.SetTrigger("Jump");
        }

        
        if (move.x < 0)
        {
            Fluffy.localScale = new Vector3(Mathf.Abs(Fluffy.localScale.x) * -1, Fluffy.localScale.y, Fluffy.localScale.z);
        }
        else if (move.x > 0)
        {
            Fluffy.localScale = new Vector3(Mathf.Abs(Fluffy.localScale.x), Fluffy.localScale.y, Fluffy.localScale.z);
        }
        
        charController.Move(move);


        // Keep him on the road
        if (transform.position.z > 5)
        {
            transform.position = transform.position.SetZ(5);
        }
        if (transform.position.z < 0)
        {
            transform.position = transform.position.SetZ(0);
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

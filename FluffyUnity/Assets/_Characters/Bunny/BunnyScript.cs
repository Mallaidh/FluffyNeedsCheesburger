using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BunnyStates
{
    Idle,
    Chasing,
    Attacking,
    RunningAway
}

public class BunnyScript : MonoBehaviour
{
    private FluffyMoveCharacer fluffy;
    private Animator anim;
    private int Health = 100;
    public BunnyStates CurrentState = BunnyStates.Idle;
    public float ChaseDistance;
    public float AttackDistance;
    public float MoveSpeed;

    void Start ()
    {
        fluffy = FindObjectOfType<FluffyMoveCharacer>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (CurrentState == BunnyStates.Chasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, fluffy.transform.position, MoveSpeed * Time.deltaTime);
        }
	}

    public void Think()
    {
        if (Vector3.Distance(fluffy.transform.position, transform.position) < AttackDistance)
        {
            Attack();
        }
        else if ((Vector3.Distance(fluffy.transform.position, transform.position) < ChaseDistance && CurrentState == BunnyStates.Idle) || (CurrentState == BunnyStates.Attacking && Vector3.Distance(transform.position, fluffy.transform.position) > AttackDistance))
        {
            StartChasing();
        }
    }

    void Attack()
    {
        CurrentState = BunnyStates.Attacking;
        anim.SetTrigger("Attack");
    }

    void StartChasing()
    {
        CurrentState = BunnyStates.Chasing;
        anim.SetTrigger("StartWalking");
    }

    private void Hit()
    {
        Health -= 20;
        if (Health <= 0)
        {
            RunAway();
        }
    }

    private void RunAway()
    {
        CurrentState = BunnyStates.RunningAway;
        anim.SetTrigger("StartWalking");
    }
}

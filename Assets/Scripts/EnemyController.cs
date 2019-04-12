﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

 public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int deathReward;
    [SerializeField] private GameObject partToOrient;
    private Vector3 directionOfMotion;
    [NonSerialized] public Vector3 spawnPosition;
    private HPController HPOfAtackedTarget;
    private bool isAttack;    
    private GameController gameController;
    private PlayerController playerController;
    [SerializeField] private Animator animator;
    

    private enum State
    {
        Walk,
        Attack,
        Die
    }
    
    void Start()
    {
        gameController = GameController.instance;
        playerController = PlayerController.instance;
        animator = gameObject.GetComponent<Animator>();
        directionOfMotion = -spawnPosition;
        partToOrient.transform.rotation = Quaternion.LookRotation(-directionOfMotion);
        currentState = State.Walk;
    }

    private State currentState;
    
    private void FixedUpdate()
    {
        UpdateState();
        if (HPOfAtackedTarget == null)
        {
            currentState = State.Walk;
        }
    }

    void UpdateState()
    {
        
        switch (currentState) {
            case State.Walk:
                Walk();
                break;
            case State.Attack:
                StartAttack();
                break;
            case State.Die:
                Die();
                break;
        }
    }

    void Walk()
    {
        animator.SetBool("attack", false);
        transform.Translate(directionOfMotion * speed * Time.deltaTime);
    }

    void Attack()
    {
        animator.SetBool("attack", true);
        Debug.Log("ATTACK");
        HPOfAtackedTarget.takeDamage(10);
    }

    void StartAttack()
    {
        if (!isAttack)
        {
            InvokeRepeating("Attack", 0f, 1f);
            isAttack = true;
        }
    }
    
    public void Die()
    {
        animator.SetTrigger("die");
        Destroy(gameObject, 1f);
        playerController.increaseMoney(100);
        gameController.UpdateNumOfEnemy();
        
    }

    private void OnTriggerEnter(Collider other)

    {
        if (other.CompareTag("Tower") || other.CompareTag("Turret"))
        {
            HPOfAtackedTarget = other.GetComponent<HPController>();
            currentState = State.Attack;
        }
    }
}
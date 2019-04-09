﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Vector3 directionOfMotion;
    [NonSerialized] public Vector3 spawnPosition;
    private HPController HPOfAtackedTarget;
    private bool isAttack;

    private enum State
    {
        Walk,
        Attack,
        Die
    }

    private State currentState;

    void Start()
    {
        directionOfMotion = -spawnPosition;
        transform.GetChild(0).transform.rotation = Quaternion.LookRotation(directionOfMotion);
        currentState = State.Walk;
    }

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
        transform.Translate(directionOfMotion * speed * Time.deltaTime);
    }

    void Attack()
    {
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
    
    void Die()
    {
        Debug.Log("Die");
    }

    private void OnTriggerEnter(Collider other)

    {
        if (other.CompareTag("Tower"))
        {
            HPOfAtackedTarget = other.GetComponent<HPController>();
            currentState = State.Attack;
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
 using System.Diagnostics;
 using TMPro;
 using UnityEngine;
using UnityEngine.UI;
 using Debug = UnityEngine.Debug;

 public class HPController : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints;
    public int currentHealthPoints;
    [SerializeField] private Image healthBar;
    private GameController gameController;

    private void Start()
    {
        gameController = GameController.instance;
        currentHealthPoints = maxHealthPoints;
    }

    public void takeDamage(int damage)
    {
        currentHealthPoints -= damage;

        healthBar.fillAmount = (float) currentHealthPoints / maxHealthPoints;

        if (currentHealthPoints <= 0)
        {
            death();
        }
    }

    void death()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                gameObject.GetComponent<EnemyController>().Die();
                break;
            case "Tower":
                gameController.GameOver();
                break;
            case "Turret":
                gameObject.GetComponent<TurretControll>().Destroy();
                break;
            
        }
       
    }
}
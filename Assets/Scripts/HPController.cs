﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints;
    public int currentHealthPoints;
    [SerializeField] private Image healthBar;

    private void Start()
    {
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
        Destroy(gameObject);
    }
}
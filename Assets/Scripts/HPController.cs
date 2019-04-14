﻿using UnityEngine;
 using UnityEngine.UI;

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

    private void death()
    {
        if (gameObject.CompareTag("Enemy"))
            gameObject.GetComponent<EnemyController>().Die();
        else if (gameObject.CompareTag("Tower"))
            gameController.GameOver();
        else if (gameObject.CompareTag("Turret")) gameObject.GetComponent<TurretController>().Destroy();
        
        Destroy(gameObject);
    }
}
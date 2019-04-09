using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    public void takeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            deathOfObject();
        }
    }

    void deathOfObject()
    {
        Debug.Log("Im die!");
        Destroy(this.gameObject);
    }
}
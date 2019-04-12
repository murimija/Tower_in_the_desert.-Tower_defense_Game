using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
    
    public int money;
    public int startMoney = 400;

    public void ReduceMoney(int valueToTakeAway)
    {
        money = money - valueToTakeAway;
        Debug.Log(valueToTakeAway);
    }
    
    public void increaseMoney(int valueToAdd)
    {
        money += valueToAdd;
    }

    void Start ()
    {
        money = startMoney;
    }
    
}
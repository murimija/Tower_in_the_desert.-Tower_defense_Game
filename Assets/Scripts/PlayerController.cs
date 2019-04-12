using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private int startMoney = 400;
    [SerializeField] private Text amountOfMoney;

    public void ReduceMoney(int valueToTakeAway)
    {
        money -= valueToTakeAway;
        amountOfMoney.text = money.ToString();
    }
    
    public void increaseMoney(int valueToAdd)
    {
        money += valueToAdd;
        amountOfMoney.text = money.ToString();
    }

    void Start ()
    {
        money = startMoney;
    }
    
}
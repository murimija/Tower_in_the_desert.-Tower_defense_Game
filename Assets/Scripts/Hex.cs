using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hex : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset = new Vector3(0, 1, 0);

    [HideInInspector] public GameObject turret;
    [HideInInspector] public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    private PlayerController playerController;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        playerController = PlayerController.instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

/*        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }*/

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(GameObject tuttet)
    {
        if (playerController.money < tuttet.GetComponent<TurretControll>().cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        GameObject _turret =
            (GameObject) Instantiate(tuttet, transform.position + positionOffset, transform.rotation);
        turret = _turret;
        
        playerController.ReduceMoney(tuttet.GetComponent<TurretControll>().cost);
        
/*        GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);*/
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;


        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
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

    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        GameObject turretToBuilt = BuildManager.instance.GetTurrretToBuilt();
        Instantiate(turretToBuilt, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
/*        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {*/
            rend.material.color = hoverColor;
/*        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }*/
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class Hex : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset = new Vector3(0, 1, 0);
    [SerializeField] private GameObject buildingTurretEffect;

    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    private PlayerController playerController;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        playerController = PlayerController.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void BuildTurret(GameObject turret)
    {
        if (playerController.money < turret.GetComponent<TurretController>().cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        var position = transform.position;
        Instantiate(turret, position + positionOffset, Quaternion.identity);

        playerController.ReduceMoney(turret.GetComponent<TurretController>().cost);

        var spawnedEffect = Instantiate(buildingTurretEffect, position + positionOffset, Quaternion.identity);
        Destroy(spawnedEffect, 2f);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;


        rend.material.color = buildManager.HasMoney ? hoverColor : notEnoughMoneyColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
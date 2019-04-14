using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }

        instance = this;
    }

    private GameObject turretToBuild;
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.instance;
    }

    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => playerController != null && playerController.money >= turretToBuild.GetComponent<TurretController>().cost;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }

        instance = this;
    }

    public GameObject buildEffect;

    private GameObject turretToBuild;
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.instance;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return playerController.money >= turretToBuild.GetComponent<TurretControll>().cost; } }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
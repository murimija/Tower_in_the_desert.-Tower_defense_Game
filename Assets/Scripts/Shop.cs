using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject oneBarrelTurret;
    [SerializeField] private GameObject twoBarrelTurret;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    // ReSharper disable once UnusedMember.Global
    public void SelectOneBarrelTurret()
    {
        buildManager.SelectTurretToBuild(oneBarrelTurret);
    }

    // ReSharper disable once UnusedMember.Global
    public void SelectTwoBarrelTurret()
    {
        buildManager.SelectTurretToBuild(twoBarrelTurret);
    }
}
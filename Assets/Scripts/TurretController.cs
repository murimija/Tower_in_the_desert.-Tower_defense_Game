using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float range = 15f;
    public int cost;

    private GameObject[] enemies;
    private float shortestDistance;
    private GameObject nearestEnemy;

    private Transform target;
    // ReSharper disable once NotAccessedField.Global
    public EnemyController targetEnemy;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    private Vector3 turretsLookRotation;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    public float fireRate = 1f;
    private float fireCountdown;

    [SerializeField] private GameObject destroyEffect;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void UpdateTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distanceToEnemy < shortestDistance)) continue;
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyController>();
        }
        else
        {
            target = null;
        }
    }

    private void LockOnTarget()
    {
        turretsLookRotation = Quaternion.Lerp(partToRotate.rotation,
            Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, turretsLookRotation.y, 0f);
    }

    private void Shoot()
    {
        var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<BulletController>().Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        var effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
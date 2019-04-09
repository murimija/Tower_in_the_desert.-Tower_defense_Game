using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrenControll : MonoBehaviour
{
    public float range = 15f;

    private GameObject[] enemies;
    private float shortestDistance;
    GameObject nearestEnemy;

    private Transform target;
    public EnemyController targetEnemy;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    private Vector3 turretsLookRotation;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
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

    void UpdateTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
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

    void LockOnTarget()
    {
        turretsLookRotation = Quaternion.Lerp(partToRotate.rotation,
            Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, turretsLookRotation.y, 0f);
    }

    void Shoot()
    {
        Debug.Log("ss");
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<BulletController>().Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public int damage = 10;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    void Update () {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            target.GetComponent<HPController>().takeDamage(damage);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }
    
    void HitTarget ()
    {
        Destroy(gameObject);
    }
    
    public void Seek(Transform _target)
    {
        target = _target;
    }
}
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public int damage = 10;
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        var dir = target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            target.GetComponent<HPController>().takeDamage(damage);
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        Destroy(gameObject);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
}
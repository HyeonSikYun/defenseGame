using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy targetEnemy;
    private Transform target;

    public float speed = 90f;
    public float explostionRadius = 0f;
    public GameObject impact;
    public int damage = 50;

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            Hit();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void Hit()
    {
        GameObject effect = Instantiate(impact, transform.position, transform.rotation);
        Destroy(effect, 2f);
        if(explostionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explostionRadius);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                //targetEnemy.TakeDamage(missileDamage * Time.deltaTime);
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
}

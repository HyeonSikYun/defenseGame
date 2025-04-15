using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    private Animator anim;

    [Header("General")]
    public float range = 15f;

    [Header("Laser")]
    public int laserDamage = 50;
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    public Light lightEffect;

    [Header("GunSoldier")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public bool useGunSoldier = false;

    [Header("Plant")]
    public int plantDamage = 5;
    public float attackRate = 1f;
    private float attackCountdown = 0f;
    public bool usePlant = false;

    [Header("DogKnight")]
    public int knightDamage = 10;
    public bool useKnight = false;

    [Header("Wizard")]
    public GameObject magicEffect;
    public bool useWizard = false;
    public float magicRate = 0.5f;
    private float magicCountdown = 0f;


    [Header("Setup")]
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform headRotate;
    public Transform firePoint;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    lightEffect.enabled = false;
                }

            }

            if(usePlant)
            {
                anim.SetBool("Attack", false);
            }

            if(useKnight)
            {
                anim.SetBool("Attack", false);
            }

            if(useWizard)
            {
                anim.SetBool("Attack", false);
            }

            
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (useLaser)
        {
            Laser();
        }

        if (usePlant)
        {
            if (attackCountdown <= 0f)
            {
                PlantAttack();
                SoundManager.Instance.PlaySound("punch_Sound");
                attackCountdown = 1f / attackRate;
            }
            attackCountdown -= Time.deltaTime;
        }

        if(useKnight)
        {
            if (attackCountdown <= 0f)
            {
                KnigthAttack();
                SoundManager.Instance.PlaySound("sword_Sound");
                attackCountdown = 1f / attackRate;
            }
            attackCountdown -= Time.deltaTime;
        }

        if (useGunSoldier)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                SoundManager.Instance.PlaySound("gun_missile_Sound");
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        if(useWizard)
        {
            if (magicCountdown <= 0f)
            {
                WizardAttack();
                SoundManager.Instance.PlaySound("wizard_Sound");
                magicCountdown = 1f / magicRate;
            }
            magicCountdown -= Time.deltaTime;
            
        }
    }

    void Shoot()
    {
        GameObject instBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = instBullet.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Laser()
    {
        targetEnemy.TakeDamage(laserDamage * Time.deltaTime);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
            lightEffect.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        laserEffect.transform.position = target.position + dir.normalized;

        laserEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void WizardAttack()
    {
        GameObject instMagic = Instantiate(magicEffect, firePoint.position, firePoint.rotation);
        anim.SetBool("Attack", true);

        Magic magic = instMagic.GetComponent<Magic>();
        if(magic != null)
        {
            magic.Seek(target);
        }
    }

    void PlantAttack()
    {
        anim.SetBool("Attack", true);
        targetEnemy.TakeDamage(plantDamage * Time.deltaTime);
        
    }

    void KnigthAttack()
    {
        anim.SetBool("Attack", true);
        targetEnemy.TakeDamage(knightDamage * Time.deltaTime);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}

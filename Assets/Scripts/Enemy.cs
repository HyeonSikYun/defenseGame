using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float baseHealth = 100f;
    public float health;
    public float healthIncreasePerRound = 10f; // 라운드당 체력 증가량
    public int value = 50;
    public float speed = 10f;

    private Transform target;
    private int waypointIndex = 0;
    public event Action OnEnemyDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        target = WayPoints.wayPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    public void Initialize(int currentRound)
    {
        // 현재 라운드에 따라 체력 계산
        health = baseHealth + (currentRound - 1) * healthIncreasePerRound;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if ((health <= 0))
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerStats.money += value;
        OnEnemyDestroyed?.Invoke();
        Destroy(gameObject);
    }

    void GetNextWayPoint()
    {
        if (waypointIndex >= WayPoints.wayPoints.Length - 1)
        {
            //Destroy(gameObject);
            target = WayPoints.wayPoints[0];
            waypointIndex = 0;
            return;
        }
        waypointIndex++;
        target = WayPoints.wayPoints[waypointIndex];
    }
}
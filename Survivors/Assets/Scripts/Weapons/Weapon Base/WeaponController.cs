using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base Script for all weapons
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int pierce;

    float currentCooldown;

    protected PlayerMovement playerMovement;

    protected virtual void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = cooldownDuration; //At the start set the current cooldown to be the cooldown duration
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}

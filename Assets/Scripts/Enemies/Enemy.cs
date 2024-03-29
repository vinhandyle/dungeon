﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The class that handles Enemy Behavior and other factors.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyHealth health;
    [SerializeField] protected LayerMask playerLayer;
    protected Rigidbody2D rb;
    protected Animator anim;
    [SerializeField] protected Vector2 direction;

    [Header("Base Stats")]
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;

    [Header("Projectile Manager")]
    [SerializeField] protected List<Transform> shootPoints;
    [SerializeField] protected List<GameObject> projectiles;

    [Header("Aggro")]
    [SerializeField] protected List<string> targetTags;
    [SerializeField] protected Transform currentTarget;
    [SerializeField] protected float aggroRange;
    [SerializeField] protected float deaggroRange;
    [SerializeField] protected bool aggroed;

    [Header("Knockback")]
    [Tooltip("Flame dash and shell smash behave differently with small and large enemies.")]
    [SerializeField] protected bool isSmall;
    [SerializeField] protected bool dealContactDmg;
    [SerializeField] protected float kbHorizontal = 75;
    [SerializeField] protected float kbVertical = 25;

    [SerializeField] protected EnemyTerrainCheck enemyTerrainCheck;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //direction = Vector2.zero;
    }

    protected virtual void Update()
    {
        AI();
    }

    /// <summary>
    /// Defines the enemy's behavior.
    /// </summary>
    protected abstract void AI();

    #region Projectile System
    /// <summary>
    /// Shoot a projectile from a shoot point at the given speed.
    /// </summary>
    /// <param name="shootPointIndex">Index of the shoot point in the component list.</param>
    /// <param name="projIndex">Index of the projectile in the component list.</param>
    /// <returns>The projectile gameobject.</returns>
    protected virtual GameObject Shoot(int shootPointIndex, int projIndex, float speed = 1)
    {
        Transform shootPoint = shootPoints[shootPointIndex];
        GameObject proj = Instantiate(projectiles[projIndex], shootPoint.position, shootPoint.rotation).gameObject;
        proj.GetComponent<Projectile>().SetOrigin(transform);
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * speed;
        return proj;
    }

    /// <summary>
    /// Rotates the given focal point so that its transform.right is pointing towards the target point.
    /// </summary>
    /// <param name="trackAhead">Set true if the focal point should track where target will be, based on the target's velocity</param>
    protected virtual void PointAtTarget(Transform focus, Transform target, bool trackAhead = false)
    {
        Vector2 direction = Vector2.zero;
        if (trackAhead && target.GetComponent<Rigidbody2D>() != null)
        {
            Vector3 deltaPos = target.GetComponent<Rigidbody2D>().velocity; // Not the most effective
            direction = (target.position + deltaPos - transform.position).normalized;
        }
        else
        {
            direction = (target.position - transform.position).normalized;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        focus.eulerAngles = Vector3.forward * angle;
        focus.localScale = transform.localScale;
    }

    #endregion

    #region Aggro System

    /// <summary>
    /// Returns all potential targets (and their disance from the enemy) within the given the distance from the enemy.
    /// </summary>
    protected virtual Dictionary<GameObject, float> GetAllTargets(float distance)
    {
        if (transform == null) return null;

        Dictionary<GameObject, float> targets = new Dictionary<GameObject, float>();
        foreach (string tag in targetTags)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                float distanceFromObject = (obj.transform.position - transform.position).sqrMagnitude;
                if (distanceFromObject <= distance) targets.Add(obj, distanceFromObject);
            }
        }
        return targets;
    }

    /// <summary>
    /// Returns the closest target to the enemy within the given distance.
    /// </summary>
    protected virtual Transform GetNearestTarget(float distance)
    {
        if (transform == null) return null;

        Transform bestTarget = null;
        float closestDistance = distance;
        var targets = GetAllTargets(distance);

        // Find nearest potential target
        foreach (var kvp in targets)
        {
            if (kvp.Value < closestDistance)
            {
                closestDistance = kvp.Value;
                bestTarget = kvp.Key.transform;
            }
        }

        return bestTarget;
    }

    /// <summary>
    /// Flips the enemy to face the opposite direction
    /// </summary>
    void FlipEnemy()
    {
        Vector3 oppDirection = transform.localScale;
        oppDirection.x *= -1;
        transform.localScale = oppDirection;
    }

    /// <summary>
    /// Sets the cardinal direction of the target relative to the enemy.
    /// </summary>
    protected virtual void GetTargetDirection(Transform target)
    {
        if (target == null) return;

        int x = (target.position.x > transform.position.x) ? 1 : -1;
        int y = (target.position.y > transform.position.y) ? 1 : -1;

        if (x != transform.localScale.x) FlipEnemy();

        direction.x = x;
        direction.y = y;
    }

    /// <summary>
    /// Sets the enemy's aggro status and its current target.
    /// </summary>
    protected virtual void CheckAggro()
    {
        List<GameObject> objectsInBothRanges = GetAllTargets(deaggroRange).Keys.ToList();
        Transform closest = GetNearestTarget(aggroRange);

        if (closest == null)
        {
            // Check if current target is between aggro and deaggro ranges
            if (objectsInBothRanges.All(o => o.transform != currentTarget))
            {
                currentTarget = null;
                aggroed = false;
            }
        }
        else
        {
            currentTarget = closest;
            aggroed = true;
        }
    }

    #endregion

    #region Free to Move Directly to Target

    /// <summary>
    /// The enemy moves in a straight line towards the target.
    /// </summary>
    /// <param name="speedMult">Amount to multiply the enemy's base speed by.</param>
    protected virtual void MoveTowardsTarget(Transform target, float speedMult = 1)
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        direction *= speed * speedMult;
        rb.velocity = direction;
    }

    #endregion

    #region Restricted to Moving Along Ground

    /// <summary>
    /// Check if the enemy has "stable" footing on the ground. Changes direction if not.
    /// </summary>
    protected virtual void CheckSurroundings()
    {
        enemyTerrainCheck.onGround = Physics2D.Raycast(enemyTerrainCheck.groundCheck.position, Vector2.down, enemyTerrainCheck.radius, enemyTerrainCheck.isGround);
        enemyTerrainCheck.againstWall = Physics2D.Raycast(enemyTerrainCheck.wallCheck.position, Vector2.right, enemyTerrainCheck.radius, enemyTerrainCheck.isWall);

        // Enemy will not change direction if following a target
        if ((!enemyTerrainCheck.onGround || enemyTerrainCheck.againstWall) && !aggroed)
        {
            FlipEnemy();
            direction.x *= -1;
        }
    }

    /// <summary>
    /// The enemy moves horizontally in the target's direction relative to it.
    /// </summary>
    /// <param name="speedMult">Amount to multiply the enemy's base speed by.</param>
    protected virtual void MoveInTargetDirection(Transform target, float speedMult = 1)
    {
        GetTargetDirection(target);
        CheckSurroundings();

        // Enemy will stop at edge if not following a target OR following a target but scared of falling
        if (!enemyTerrainCheck.onGround && ((aggroed && !enemyTerrainCheck.fearless) || !aggroed))
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(direction.x * speed * speedMult, rb.velocity.y);
        }
    }

    #endregion
}

/// <summary>
/// Collapsible section for the enemy's ground check.
/// </summary>
[System.Serializable]
public class EnemyTerrainCheck
{
    public LayerMask isGround;
    public LayerMask isWall;
    public Transform groundCheck;
    public Transform wallCheck;
    public float radius;
    public bool fearless;
    public bool onGround;
    public bool againstWall;
}

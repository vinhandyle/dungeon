using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{   
    [Header("Generic Enemy Info")]
    [SerializeField] protected SpriteRenderer sprite = null;
    [SerializeField] protected EnemyHealth health = null;

    [Header("AI")]
    [SerializeField] protected string aiState = "";
    [SerializeField] protected float aiTimer = 0;

    protected MovePool allMoves = new MovePool();
    protected Player player = null;
    protected bool inRange = false;
    protected bool moveInit = true;

    protected const string playerTag = "Player";

    protected abstract void InitializeAI();

    protected abstract void SetUpMovePools();

    protected virtual void Awake()
    {
        SetUpMovePools();
        health.FullHeal();
    }

    protected virtual void FaceTowardsPlayer()
    {
        if (player)
        {
            sprite.flipX = player.transform.position.x > transform.position.x;
        }
    }

    protected virtual void SetUpMove(string aiState, float duration)
    {
        if (moveInit)
        {
            this.aiState = aiState;
            aiTimer = duration;
            moveInit = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.GetComponentInParent<Player>();
            inRange = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = null;
            inRange = false;
        }
    }
}
using UnityEngine;

public abstract class Melee : MonoBehaviour
{
    [Header("Generic Melee Info")]
    [SerializeField] protected int damage = 0;
    [SerializeField] protected bool parryable = true;

    [HideInInspector] public bool parried = false;

    protected PlayerHealth player = null;

    protected const string playerTag = "Player";

    protected abstract void OnTriggerEnter2D(Collider2D collision);    
}

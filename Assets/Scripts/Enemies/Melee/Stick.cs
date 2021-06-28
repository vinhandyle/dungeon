using UnityEngine;

public class Stick : Melee
{
    [HideInInspector] public float swingSpeed = 0;
    [HideInInspector] public bool swingingForward = false;

    private const int quarterCircle = 45;

    private void Update()
    {
        Swing();
    }

    public void Swing()
    {
        if (swingingForward)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * quarterCircle * swingSpeed);
        }        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject.GetComponentInParent<PlayerHealth>();
            player.TakeDamage(damage);
        }
    }
}

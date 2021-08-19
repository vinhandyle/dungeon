using System.Collections;
using UnityEngine;

/// <summary>
/// Represents any enemy's health.
/// </summary>
public class EnemyHealth : Health
{
    [SerializeField] protected bool isBoss = false;

    [Header("Indication of being Hit")]
    [SerializeField] protected int damageFlashDelta = 0;
    [SerializeField] protected float damageFlashTime = 0;
    protected Color initialColor = new Color();

    private void Awake()
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        initialColor = material.color;
    }

    protected override void DamageTakenEvent()
    {
        StartCoroutine(DamageFlash());
    }

    protected override void DeathEvent()
    {

    }

    protected void SetBrightness(int delta)
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        Color newColor = new Color(
            initialColor.r + delta > 255 ? 255 : initialColor.r + delta,
            initialColor.g + delta > 255 ? 255 : initialColor.g + delta,
            initialColor.b + delta > 255 ? 255 : initialColor.b + delta);
        material.SetColor("_Color", newColor);
    }

    protected void ResetBrightness()
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        material.SetColor("_Color", initialColor);
    }

    protected IEnumerator DamageFlash()
    {
        SetBrightness(damageFlashDelta);
        yield return new WaitForSeconds(damageFlashTime);
        ResetBrightness();
    }
}

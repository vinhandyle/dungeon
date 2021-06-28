using System.Collections;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private int damageFlashDelta = 0;
    [SerializeField] private float damageFlashTime = 0;
    private Color initialColor = new Color();

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

    private void SetBrightness(int delta)
    {
        Material material = transform.Find("Graphic").GetComponent<SpriteRenderer>().material;
        Color newColor = new Color(
            initialColor.r + delta > 255 ? 255 : initialColor.r + delta,
            initialColor.g + delta > 255 ? 255 : initialColor.g + delta,
            initialColor.b + delta > 255 ? 255 : initialColor.b + delta);
        material.SetColor("_Color", newColor);
    }

    private void ResetBrightness()
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

using UnityEngine;

/// <summary>
/// Base class for any item the player can obtain.
/// </summary>
public abstract class Item : MonoBehaviour
{
    protected string itemName;
    protected string itemDescription;

    /// <summary>
    /// Sets the item's name and description when it is called into the scene for the first time.
    /// </summary>
    protected abstract void SetUpItem();

    protected void Awake()
    {
        SetUpItem();
    }

    /// <summary>
    /// Returns the object this component is attached to.
    /// </summary>
    public GameObject GetObject()
    {
        return gameObject;
    }

    /// <summary>
    /// Returns the item's name.
    /// </summary>
    public string GetName()
    {
        return itemName;
    }

    /// <summary>
    /// Returns the item's description.
    /// </summary>
    public string GetDesc()
    {
        return itemDescription;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the player's invenotry.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [Header("Tools")]
    private LinkedList<Tool> equippedTools = null;
    private LinkedListNode<Tool> _currentHeldTool = null;
    public Tool currentHeldTool { get { return _currentHeldTool?.Value; } }

    [Header("Items")]
    private List<Item> items = null;

    private void Awake()
    {
        TestEquip();
    }

    /// <summary>
    /// Converts a list of tools into a linked list of tools.
    /// </summary>
    /// <param name="tools">A list of tools to be equipped.</param>
    public void EquipTools(List<Tool> tools)
    {
        string debugMessage = "Equipped: ";
        equippedTools = new LinkedList<Tool>(); ;

        foreach (Tool tool in tools)
        {
            debugMessage += tool + " | ";
            equippedTools.AddLast(tool);
        }

        Debug.Log(debugMessage);
        _currentHeldTool = equippedTools.First;
        PrintToolState("Tools Equipped");
    }

    /// <summary>
    /// Returns the next tool in the cycle.
    /// </summary>
    public void GetNextTool()
    {
        _currentHeldTool = _currentHeldTool.Next ?? equippedTools.First;
        PrintToolState("Cycled to Next Tool");
    }

    /// <summary>
    /// Returns the previous tool in the cycle.
    /// </summary>
    public void GetPrevTool()
    {
        _currentHeldTool = _currentHeldTool.Previous ?? equippedTools.Last;
        PrintToolState("Cycled to Previous Tool");
    }

    /// <summary>
    /// Equips the spear, shield, and water flask tools in that order.
    /// </summary>
    private void TestEquip()
    {
        List<Tool> tools =
            new List<Tool>()
            {
                GetComponentInChildren<Spear>(),
                GetComponentInChildren<Shield>(),
                GetComponentInChildren<WaterFlask>()
            };

        EquipTools(tools);
    }

    /// <summary>
    /// Prints to the console the current tool held and
    /// the current configuration of the tool wheel.
    /// </summary>
    private void PrintToolState(string header)
    {
        string debugMessage = header + "\nCurrent Tool: " + _currentHeldTool.Value + "\nTool Wheel: ";
        LinkedListNode<Tool> tool = _currentHeldTool;

        while (tool != null)
        {
            debugMessage += tool.Value + " ";
            tool = tool.Next;
        }

        Debug.Log(debugMessage);
    }
}
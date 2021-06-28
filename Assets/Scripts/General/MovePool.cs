using System;
using System.Collections.Generic;

/// <summary>
/// Represents a set of moves that an enemy can do.
/// </summary>
public class MovePool
{
    private Dictionary<string, Action> movePool = new Dictionary<string, Action>();
    private List<string> aiStates = new List<string>();

    /// <summary>
    /// Adds new moves from the given dictionary into this MovePool.
    /// </summary>
    public void MergePool(Dictionary<string, Action> movePool)
    {
        foreach (KeyValuePair<string, Action> move in movePool)
        {
            this.movePool.Add(move.Key, move.Value);
            aiStates.Add(move.Key);
        }
    }

    /// <summary>
    /// Adds new moves from the given MovePool into this MovePool.
    /// </summary>
    public void MergePool(MovePool mp)
    {
        foreach (KeyValuePair<string, Action> move in mp.movePool)
        {
            movePool.Add(move.Key, move.Value);
            aiStates.Add(move.Key);
        }
    }

    /// <summary>
    /// Executes the move from the MovePool that corresponds to the given key value.
    /// </summary>
    /// <param name="aiState">The state that the enemy will take.</param>
    public void Move(string aiState)
    {
        if (movePool.ContainsKey(aiState))
        {
            movePool[aiState]();
        }
    }

    /// <summary>
    /// Executes a random move from the MovePool.
    /// </summary>
    public void RandomMove()
    {
        movePool[aiStates[UnityEngine.Random.Range(0, aiStates.Count)]]();
    }
}

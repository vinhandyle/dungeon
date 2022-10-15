using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that is a segment of a path.
/// </summary>
public class PathTile : MonoBehaviour
{
    [Tooltip("Neighbors must not be neighbors of each other")]
    [SerializeField] private List<PathTile> neighbors = new List<PathTile>();

    public List<List<PathTile>> GetPathsToTarget(PathTile prev, PathTile target)
    {
        List<List<PathTile>> paths = new List<List<PathTile>>();

        // Get path from neighbors
        foreach (PathTile n in neighbors)
        {
            if (n == target)
            {
                paths.Add(new List<PathTile>() { target });
            }
            else if (n != prev)
            {
                List<List<PathTile>> pathN = n.GetPathsToTarget(this, target);

                if (pathN.Count > 0)
                {
                    paths.AddRange(pathN);
                }
            }
        }

        // Insert tile into path
        foreach (List<PathTile> path in paths)
        {
            if (prev)
                path.Insert(0, this);
        }

        return paths;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PathTarget>())
        {
            collision.GetComponent<PathTarget>().currentTile = this;
        }
        else if (collision.GetComponent<PathFinder>())
        {
            collision.GetComponent<PathFinder>().currentTile = this;
        }
    }
}

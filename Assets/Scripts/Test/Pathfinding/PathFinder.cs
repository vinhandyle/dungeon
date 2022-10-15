using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// An object that moves along a path of tiles to reach its target.
/// </summary>
public class PathFinder : MonoBehaviour
{
    [SerializeField] private float vMult = 1;
    [SerializeField] private float marginOfError = 0.01f;

    [SerializeField] private bool search = false;
    [SerializeField] private PathTarget target = null;
    [SerializeField] private List<PathTile> path = null;

    public PathTile targetTile = null;
    public PathTile currentTile = null;

    void Update()
    {
        PathTile prevTargetTile = targetTile;
        targetTile = target.GetComponent<PathTarget>().currentTile;

        if (targetTile == currentTile)
        {
            MoveTowards(gameObject, target.gameObject);
        }
        else if (targetTile == prevTargetTile)
        {
            if (path.Count > 0)
            {
                MoveTowards(gameObject, path[0].gameObject);

                if (((Vector2)transform.position - (Vector2)path[0].transform.position).magnitude < marginOfError)
                {
                    path.RemoveAt(0);
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        else
        {
            GetShortestPath();
        }
    }

    private void MoveTowards(GameObject g, GameObject dest)
    {
        Vector2 v = 
            new Vector2(
                dest.transform.position.x - g.transform.position.x,
                dest.transform.position.y - g.transform.position.y
                );
        v.Normalize();
        g.GetComponent<Rigidbody2D>().velocity = v * vMult;        
    }

    private void GetShortestPath()
    {
        path =
            currentTile.GetPathsToTarget(null, targetTile)
                       .OrderBy(p => p.Count)
                       .ThenBy(p => (p[0].transform.position - transform.position).magnitude)
                       .First();
    }
}

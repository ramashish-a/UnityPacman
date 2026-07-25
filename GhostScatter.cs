using UnityEngine;
using System.Linq;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Only choose a direction if scatter is active AND not frightened
        if (node != null && enabled  && !ghost.frightened.enabled)
        {
            //var dirs = node.availableDirections;
            var dirs = node.availableDirections.ToList();

            // Filter out the reverse direction if possible
            if (dirs.Count > 1)
            {
                dirs = dirs.Where(d => d != -ghost.movement.direction).ToList();
            }

            // Safety check: if filtering removed everything, fall back to original list
            if (dirs.Count == 0)
            {
                dirs = node.availableDirections.ToList();
            }

            int index = Random.Range(0, dirs.Count);
            ghost.movement.SetDirection(dirs[index]);
        }
    }
}

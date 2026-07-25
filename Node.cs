using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    // public readonly List<Vector2> availableDirections = new();
    public List<Vector2> AvailableDirections { get; private set; } = new();

    private void Start()
    {
        availableDirections.Clear();

        // We determine if the direction is available by box casting to see if
        // we hit a wall. The direction is added to list if available.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);

         if (Physics2D.OverlapBox(transform.position, Vector2.one * 0.9f, 0f, obstacleLayer))
         {
                Debug.LogWarning($"Node {name} is inside a wall!");
         }
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.9f, 0f, direction, 0.8f, obstacleLayer);

        // If no collider is hit then there is no obstacle in that direction
        if (hit.collider == null) {
            availableDirections.Add(direction);
        }
    }

}

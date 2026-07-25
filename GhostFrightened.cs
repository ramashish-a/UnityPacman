using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    private bool eaten;

    public override void Enable(float duration)
    {
        // Prevent accidental activation with invalid duration
        if (duration <= 0f)
            return;

        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        eaten = false;

        // Flash halfway through frightened duration
        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();

        // Restore normal appearance
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;

        eaten = false;
    }

    private void Eaten()
    {
        eaten = true;

        ghost.SetPosition(ghost.home.inside.position);
        ghost.home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flash()
    {
        // Only flash if still frightened and not eaten
        if (!enabled || eaten)
            return;

        blue.enabled = false;
        white.enabled = true;

        var anim = white.GetComponent<AnimatedSprite>();
        if (anim != null)
            anim.Restart();
    }

    private void OnEnable()
    {
        // Only restart animation if actually frightened
        if (blue != null)
        {
            var anim = blue.GetComponent<AnimatedSprite>();
            if (anim != null)
                anim.Restart();
        }

        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 bestDirection = Vector2.zero;
            float maxDistance = float.MinValue;

            // Choose direction that moves farthest from Pac-Man
            foreach (Vector2 dir in node.availableDirections)
            {
                Vector3 newPos = transform.position + (Vector3)dir;
                float dist = (ghost.target.position - newPos).sqrMagnitude;

                if (dist > maxDistance)
                {
                    maxDistance = dist;
                    bestDirection = dir;
                }
            }

            ghost.movement.SetDirection(bestDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
                Eaten();
        }
    }
}

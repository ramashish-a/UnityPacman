using UnityEngine;

public class Pellet : MonoBehaviour
{
    // assigning the value to each pellet (10 pts)
    public int points = 10;

    // this code and below manages character(s) and pellet(s) interaction(s)
    protected virtual void Eat()
    {
        // finding our game manager in our scene
        FindAnyObjectByType<GameManager>().PelletEaten(this);
    }

    // detecting when pacman hits pellet
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}

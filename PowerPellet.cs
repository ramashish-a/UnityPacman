using UnityEngine;

public class PowerPellet : Pellet
{
    // specifying the duration of the power pellet
    public float duration = 8.0f;

    // this code and below manages character(s) and pellet(s) interaction(s)
    protected override void Eat()
    {
        // finding our game manager in our scene
        FindAnyObjectByType<GameManager>().PowerPelletEaten(this);
    }

}

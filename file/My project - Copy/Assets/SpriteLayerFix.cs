using UnityEngine;

public class SpriteLayerFix : MonoBehaviour
{

    [SerializeField] BoxCollider2D front_collision;
    [SerializeField] BoxCollider2D back_collision;
    [SerializeField] PolygonCollider2D player;
    [SerializeField] SpriteRenderer[] sprites_to_flip;
    [SerializeField] int front_layer;
    [SerializeField] int back_layer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(front_collision.IsTouching(player))
        {
            foreach(SpriteRenderer sprite in sprites_to_flip)
            {
                sprite.sortingOrder = front_layer;
            }
        }

        if (back_collision.IsTouching(player))
        {
            foreach (SpriteRenderer sprite in sprites_to_flip)
            {
                sprite.sortingOrder = back_layer;
            }
        }
    }
}

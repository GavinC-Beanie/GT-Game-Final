using UnityEngine;

public class InteractionDebugger : MonoBehaviour
{
    private CharacterInteraction interaction;
    
    void Start()
    {
        interaction = GetComponent<CharacterInteraction>();
        
        if (interaction == null)
        {
            Debug.LogError(gameObject.name + " has no CharacterInteraction component!");
            return;
        }
        
        Debug.Log(gameObject.name + " has interaction script");
        
        // Check if 2D collider exists
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError(gameObject.name + " has no 2D collider!");
        }
        else if (!col.isTrigger)
        {
            Debug.LogError(gameObject.name + " collider is not set as trigger!");
        }
    }
    
    void Update()
    {
        // Temporary test - press space to force dialogue
        if (Input.GetKeyDown(KeyCode.Space) && interaction != null)
        {
            Debug.Log("Space pressed - forcing dialogue");
            interaction.Interact();
        }
    }
}
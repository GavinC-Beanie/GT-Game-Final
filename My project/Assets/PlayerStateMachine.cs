using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    I_Player_State current_state;
    [HideInInspector] public Rigidbody2D rb;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SwitchState(new S_Idle());
    }

    void Update()
    {
        current_state.UpdateState(this);
    }

    public void SwitchState(I_Player_State state)
    {
        if(current_state != null)
        {
            current_state.ExitState(this);
        }
        current_state = state;
        current_state.EnterState(this);
    }
}

public interface I_Player_State
{
    void EnterState(PlayerStateMachine player);
    void UpdateState(PlayerStateMachine player);
    void ExitState(PlayerStateMachine player);
}

public class S_Idle : I_Player_State
{
    public void EnterState(PlayerStateMachine player)
    {
    
        Debug.Log("Entering Idle");
    }

    public void UpdateState(PlayerStateMachine player)
    {
        //Debug.Log("Idle running");
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            player.SwitchState(new S_Walking());
        }
    
    }

    public void ExitState(PlayerStateMachine player)
    {
        Debug.Log("Leaving Idle");
    } 
}

public class S_Walking : I_Player_State
{
    public void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Starting Walking");
    }

    public void UpdateState(PlayerStateMachine player)
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveDir.y += 1;
        if (Input.GetKey(KeyCode.S)) moveDir.y -= 1;
        if (Input.GetKey(KeyCode.D)) moveDir.x += 1;
        if (Input.GetKey(KeyCode.A)) moveDir.x -= 1;

        moveDir = moveDir.normalized;

        Vector2 newPos = player.rb.position + moveDir * player.moveSpeed * Time.deltaTime;
        player.rb.MovePosition(newPos);

        if (moveDir == Vector2.zero)
        {
            player.SwitchState(new S_Idle());
        }
    }

    public void ExitState(PlayerStateMachine player)
    {
        Debug.Log("Leaving Walking");
    }
}

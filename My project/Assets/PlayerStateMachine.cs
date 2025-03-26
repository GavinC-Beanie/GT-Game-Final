using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    I_Player_State current_state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchState(new S_Idle());
    }

    // Update is called once per frame
    void Update()
    {
        current_state.UpdateState(this);
    }

    public void SwitchState(I_Player_State state)
    {
        if(current_state!=null)
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
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            player.SwitchState(new S_Idle());
        }
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 POS=player.transform.position;
            POS.y=POS.y+.01f;
            player.transform.position=POS;
        }
         if(Input.GetKey(KeyCode.A))
        {
            Vector3 POS=player.transform.position;
            POS.x=POS.x-.01f;
            player.transform.position=POS;
        }
         if(Input.GetKey(KeyCode.S))
        {
            Vector3 POS=player.transform.position;
            POS.y=POS.y-.01f;
            player.transform.position=POS;
        }
         if(Input.GetKey(KeyCode.D))
        {
            Vector3 POS=player.transform.position;
            POS.x=POS.x+.01f;
            player.transform.position=POS;
        }
    }

    public void ExitState(PlayerStateMachine player)
    {
        Debug.Log("Leaving Walking");
    } 
}
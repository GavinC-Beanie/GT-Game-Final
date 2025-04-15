using UnityEngine;
using System. Collections.Generic;
using System.Collections;
[RequireComponent (typeof (Rigidbody2D))]

public class PlayerMovementController : MonoBehaviour
{
[SerializeField] AnimationCurve curveY;
Rigidbody2D rb;
[SerializeField] Animator ani;
Vector2 movement;
Vector2 currentPos;
Vector2 landingPos;
public float landingDis;
public float speed = 1f;
public float timeElapsed = 0f;
bool onGround = true;
bool jump = false;
void Start() 
{
rb = GetComponent<Rigidbody2D>();
}

void Update()
{
    InputHandler();
}

void FixedUpdate()
{
    if(jump)
    {
        JumpHandler();
    }
    else
    {
         MovementHandler();
    }
}

void JumpHandler()
    {
        if(onGround)
        {
            currentPos = rb.position;
            landingPos = currentPos + movement.normalized * speed;
            landingDis = Vector2.Distance(landingPos, currentPos);
            timeElapsed = 0f;
            onGround = false;
        }
        else
        {
            timeElapsed += Time.fixedDeltaTime * speed / landingDis;
            if(timeElapsed <= 1f)
            {
                currentPos = Vector2.MoveTowards (currentPos, landingPos, Time.fixedDeltaTime * speed);
                rb.MovePosition(new Vector2(currentPos.x, currentPos.y + curveY.Evaluate(timeElapsed)));
            }
            else
            {
                jump = false; 
                onGround = true;
            }
        }       
    }

void MovementHandler()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

void InputHandler()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal ==0 && vertical ==0)
        {
            ani.SetBool("walking", false);
        }
        else 
        {
            ani.SetBool("walking", true);
        }

        movement = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown ("space"))
        {
            jump = true;
        }
    }




}
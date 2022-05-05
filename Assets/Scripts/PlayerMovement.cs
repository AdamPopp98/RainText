using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private KeyCode Jump;
	private KeyCode MoveLeft;
	private KeyCode MoveRight;
	public float jumpStrength;
	public float accel;
	public float reverseAccel;
	public float autoDeccel;
	public float topSpeed;
	public float gravity;
	public float groundPlane;
	float airTime;
	float xVelocity;
	float yVelocity;
	bool jumpPressed;
	int walkDirection;
    // Start is called before the first frame update
    void Awake()
    {
    	jumpPressed = false;
    	walkDirection = 0;
    	ChangeBindings();
    }

    // Update is called once per frame
    void Update()
    {
    	WalkInput();
    	JumpInput();
    	WalkInput();
    	transform.Translate(new Vector3(xVelocity, yVelocity, 0) * Time.deltaTime);
    	WallWrap();
    }
    
	void ChangeBindings()
	{
		Jump = RebindKeys.Instance.GetBinding("Jump");
		MoveLeft = RebindKeys.Instance.GetBinding("MoveLeft");
		MoveRight = RebindKeys.Instance.GetBinding("MoveRight");
	}
    
    void FixedUpdate()
    {
    	WalkPhysics();
    	JumpPhysics();
    }
    
    void WallWrap()
    {
    	if (transform.position.x < -13.5F)
    	{
		Vector3 pos = transform.position;
		pos.x = 13.5F;
		transform.position = pos;
    	}
    	
    	if (transform.position.x > 13.5F)
    	{
		Vector3 pos = transform.position;
		pos.x = -13.5F;
		transform.position = pos;
    	}
    }
    
    void WalkInput()
    {
    	if (Input.GetKey(MoveRight))
    	{
    		walkDirection = 1;
    	}
    	else if (Input.GetKey(MoveLeft))
    	{
    		walkDirection = -1;
    	}
    	else
    	{
    		walkDirection = 0;
    	}
    }
    
    void WalkPhysics()
    {
    	if (walkDirection != 0)
    	{
    		float accelBy = (xVelocity * walkDirection >  0) ? accel : reverseAccel;
    		if (walkDirection == 1)
	    	{
	    		xVelocity = Math.Min((xVelocity + accelBy), topSpeed);    	
	    	}
	    	else
	    	{
	    		xVelocity = Math.Max((xVelocity - accelBy), 0 - topSpeed);
	    	}	
    	}
    	else
    	{
    		if (xVelocity < 0)
    		{
    			xVelocity = Math.Min((xVelocity + autoDeccel), 0);    			
    		}
    		else
    		{
    			xVelocity = Math.Max((xVelocity - autoDeccel), 0);
    		}
    	}
    }
    
    void JumpInput()
    {
    	if (jumpPressed == false && Input.GetKey(Jump))
    	{
    		yVelocity = jumpStrength;
    		jumpPressed = true;
    	}
    	else
    	{
    		jumpPressed = InAir();    		
    	}
    }
    
    void JumpPhysics()
    {
    	if (jumpPressed == true)
    	{
    		yVelocity -= gravity * airTime;
    		airTime += Time.deltaTime;    		
    	}
    }
    
    bool InAir()
    {
	Vector3 pos = transform.position;
    	if (pos.y  - (yVelocity * Time.deltaTime) <= groundPlane)
    	{
    		pos.y = groundPlane;
		transform.position = pos;
		yVelocity = 0.0F;
		airTime = 0.0F;		
    	}
    	return (pos.y > groundPlane);
    }
}

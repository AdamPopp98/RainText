using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPath : MonoBehaviour
{
	public bool outOfBounds;
	float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
    	GenerateCloud();
    }

    // Update is called once per frame
    void Update()
    {
    	MovementPath();
    }
    
    void MovementPath()
    {
    	transform.Translate(new Vector3(xVelocity * Time.deltaTime, 0, 0));
    	Vector3 pos = transform.position;
    	if (xVelocity > 0 && pos.x > 16)
    	{
    		GenerateCloud();
    	}
    	
    	else if (xVelocity < 0 && pos.x < -16)
    	{
    		GenerateCloud();
    	}
    }
    
    void GenerateCloud()
    {
	System.Random rand = new System.Random((int)DateTime.UtcNow.Ticks);
        xVelocity = rand.Next(1,3) + (float)rand.NextDouble();
        int direction = rand.Next(0, 2);
        if (direction != 1)
        {
        	xVelocity *= -1;
        }
        Vector3 pos = transform.position;
		pos.y = rand.Next(0, 8) + (float)rand.NextDouble();
		if (xVelocity < 0)
		{
			pos.x = 16;
		}
		else
		{
			pos.x = -16;
		}
		pos.z = (float)xVelocity;
		transform.position = pos;
	}
}

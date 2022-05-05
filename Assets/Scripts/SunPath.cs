using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPath : MonoBehaviour
{
	public bool outOfBounds;
	public float radius;
	public float angle;
    // Start is called before the first frame update
    void Start()
    {
    	angle = 0;
    	Vector3 pos = transform.position;
	pos.y = 0;
	pos.x = Mathf.Cos(0) * radius * 2;
	transform.position = pos;
    	outOfBounds = false;        
    }

    // Update is called once per frame
    void Update()
    {
    	MovementPath();
    }
    
    void MovementPath()
    {
        angle += Time.deltaTime;
        if (angle >= 720.0F)
        {
        	angle = 0.0F;
        }	
    	Vector3 pos = transform.position;
    	float xShift = Mathf.Cos(angle * 0.25F) * radius * 2 - pos.x;
    	float yShift = Mathf.Sin(angle * 0.25F) * radius - pos.y - 5;
    	transform.Translate(new Vector3(xShift, yShift, 0));
    	if (xShift > 0)
    	{
    		outOfBounds = true;
    	}
    }
}

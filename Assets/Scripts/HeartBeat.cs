using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartBeat : MonoBehaviour
{
	/*public float timeBetween;
	public float maxSize;
	float curTimer;
	public bool lostLife;
	bool firstLine;
	bool pulse;
	bool regaining;
	bool losing;
	bool increasing;
	float curScale;
	
    // Start is called before the first frame update
    void Start()
    {
    	losing = false;
    	regaining = false;
    	curTimer = 0;
    	lostLife = false;
    	firstLine = true;
    	pulse = false;
    	increasing = true;
    	curScale = 1.0F;
    	maxSize = 1.2F;       
    }

    // Update is called once per frame
    void Update()
    {
    	//curTimer continues to incremnt even while the heart is dead
    	//to ensure all hearts remained synced
	curTimer += Time.deltaTime;
	if (curTimer >= timeBetween)
    	{
    		curTimer = 0.0F;
    		//pulse is only set to true if the heart is still alive
    		if (lostLife == false)
    		{
    			pulse = true;
    		}
    	}
    	//Pulse animation completes before the LoseLife animation can occur
    	else if (pulse == true)
    	{
    		Pulse();
    	}
    	
    	//Timer adds a short delay between the sprites 
    	//that form the X in front of the heart.
    	else if (firstLine == true && lostLife == true)
    	{
    		LoseLife();
    	}
    }
    
    void Pulse()
    {
	if (transform.localScale.y < maxSize && increasing == true)
	{
		float rate = Math.Min(maxSize - curScale, Time.deltaTime);
		transform.localScale += new Vector3(rate, rate, 0.0F);
		if (transform.localScale.y >= maxSize)
		{
			increasing = false;
		}
	}
	else if (transform.localScale.y > 1.0F)
	{
		float rate = Math.Max(curScale - 1.0F, Time.deltaTime / 2);
		transform.localScale -= new Vector3(rate, rate, 0.0F);
	}
	else
	{
		pulse = false;
		increasing = true;
	}
    }
    
    void LoseLife()
    {
    	if (regaining == true)
    	{
    		regaining = false;
    		Invoke("LoseLife", 0.75F);
    		return;    		
    	}
    	GameObject xSymbol = transform.GetChild(1).gameObject;
    	if (firstLine == true)
    	{
    		xSymbol.gameObject.SetActive(true);
	    	xSymbol.transform.GetChild(0).gameObject.SetActive(true);
	    	firstLine = false;
	    	Invoke("LoseLife", 0.5F);
    	}
    	else
    	{
    	    	xSymbol.transform.GetChild(1).gameObject.SetActive(true);
    	}   	
    }
    
    public void AddLife()
    {
        if (losing == true)
    	{
    		losing = false;
    		Invoke("GainLife", 0.75F);
    		return;    		
    	}
    	GameObject xSymbol = transform.GetChild(1).gameObject;
	UnityEngine.Debug.Log("Gain Life");
    	if (firstLine == false)
    	{
    		regaining = true;
	    	xSymbol.transform.GetChild(0).gameObject.SetActive(false);
	    	firstLine = true;
	    	lostLife = false;
	    	Invoke("AddLife", 0.5F);
    	}
    	else
    	{
	    	xSymbol.transform.GetChild(1).gameObject.SetActive(false);
	    	xSymbol.gameObject.SetActive(false);
    	}
    }*/
    
    public float timeBetween;
    public Animator heartAnimator;
    public Animator xAnimator;
    private float timer;
    private bool alive;
    void Awake()
    {
    	alive = true;
    	timer = 0.0F;
    }
    
    void Update()
    {
    	Heartbeat();
    }
    
    public void ToggleAlive()
    {
    	alive = !alive;
    	xAnimator.SetBool("Alive", alive);
    	xAnimator.SetTrigger("Toggle");
    }
    
    private void Heartbeat()
    {
    	timer = timer += Time.deltaTime;
    	if (timer > timeBetween)
    	{
    		timer = 0.0F;
    		if (alive == true && !xAnimator.IsInTransition(0))
    		{
	    		heartAnimator.SetTrigger("Beat");
    		}
    	}
    }
    
    public bool GetAlive()
    {
    	return alive;
    }    
}

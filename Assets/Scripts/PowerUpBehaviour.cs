using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpBehaviour : MonoBehaviour
{
	public GameObject FXplayer;
	private KeyCode UseItem;
	private bool held;
	private GameObject holder;
	private float yVelocity;
	private float xVelocity;

    // Update is called once per frame
    void Update()
    {
    	if (held == false)
    	{
    		this.gameObject.transform.Translate(new Vector3(xVelocity, yVelocity, 0) * Time.deltaTime);
    		if (transform.position.y <= -8)
    		{
    			Destroy(this.gameObject);
    		}
    	}
    	else if (Input.GetKey(UseItem))
    	{
    		UsePowerUp();
    	}
    }
    
    void Awake()
    {
    	held = false;
    	SetStartValues();
    	ChangeBindings();
    }
    
    void ChangeBindings()
    {
    	UseItem = RebindKeys.Instance.GetBinding("UsePowerUp");
    }
    
    private void SetStartValues()
    {
        Vector3 pos = transform.position;
        System.Random rand = new System.Random((int)DateTime.UtcNow.Ticks);
        xVelocity = (float)rand.Next(5);
        pos.x = (float)rand.Next(1, 10);
        int direction = rand.Next(2);
        if (direction != 1)
        {
        	pos.x = 0 - pos.x;
        }
        else
        {
        	xVelocity = 0 - xVelocity;
        }
        pos.z = -5;
        pos.y = 10;
        yVelocity = (float)(0 - rand.Next(4, 8));
        transform.position = pos;
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
    	{
    		if (this.gameObject.tag == "Extra Life")
    		{
    			AddLife();
    			return;
    		}
	    	SpawnPowerUps spScript = holder.gameObject.GetComponent<SpawnPowerUps>();
	    	spScript.CollectPowerUp(this.gameObject);
    	}
    }
    
    public void SetHolder(GameObject holderObject)
    {
    	holder = holderObject;    
    }
    
    public void SetHeld(bool isHeld)
    {
    	held = isHeld;
    }
    
    void UsePowerUp()
    {
	PlayAudio();
	switch (this.gameObject.tag)
	{
		case "Water Bucket":
			DouseFires();
			break;
		default:
			FreezeMeteors();    				
			break;
	}
    }
    
    private void AddLife()
    {
    	GameObject hearts = GameObject.Find("Hearts");
    	for (int i = hearts.transform.childCount - 1; i >= 0; i--)
    	{
    		HeartBeat heartScript = hearts.transform.GetChild(i).gameObject.GetComponent<HeartBeat>();
    		if (!heartScript.GetAlive())
    		{
    			PlayAudio();
    			heartScript.ToggleAlive();
    			break;
    		}
    	}
    	Destroy(this.gameObject);
    }
    
    private void DouseFires()
    {
	GameObject meteors = GameObject.Find("WordMeteorContainer");
	meteors.GetComponent<ParseWords>().DouseMeteors();
	GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
	foreach (GameObject fire in fires)
	{
		Destroy(fire);
	}
	Destroy(this.gameObject);
    }
    
    private void FreezeMeteors()
    {
	GameObject meteors = GameObject.Find("WordMeteorContainer");
	meteors.GetComponent<ParseWords>().FreezeMeteors();
	Destroy(this.gameObject);
    }
    
    private void PlayAudio()
    {
    	PlaySoundFXAfterDestroy script = FXplayer.GetComponent<PlaySoundFXAfterDestroy>();
    	script.PlayAudio();
    }
}

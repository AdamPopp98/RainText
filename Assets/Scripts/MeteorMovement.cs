using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeteorMovement : MonoBehaviour
{
	public GameObject textbox;
	public GameObject firePool;
	float yVelocity;
	float xVelocity;
	float minVelo;
	float maxVelo;
    // Start is called before the first frame update
    void Awake()
    {
    	minVelo = StoreSliderSettings.Instance.GetSetting("MinVelo");
    	maxVelo = StoreSliderSettings.Instance.GetSetting("MaxVelo");
            Vector3 pos = transform.position;
            pos.z = -5;
            transform.position = pos;
    }    

    // Update is called once per frame
    void Update()
    {
		transform.Translate(new Vector3(xVelocity, yVelocity, 0) * Time.deltaTime);
		if (transform.position.y < -8)
		{
			RequestNextWord();
		}
    }
    
    public void GenerateWord(string nextWord)
    {
    	MeteorTextSetter textSetterScript = textbox.GetComponent<MeteorTextSetter>();
        Vector3 pos = transform.position;
        if (textSetterScript.CheckIfSpecial() == true)
        {
        	SpawnFirePool(textSetterScript.GetTextWidth());
        }
        UnityEngine.Random.InitState((int)DateTime.UtcNow.Ticks + nextWord.GetHashCode());
        //Sets the starting x position to a random value
        pos.x = UnityEngine.Random.Range(-10.0f, 10.0f);
        pos.y = 10f;
        pos.z = -5f;
        transform.position = pos;
        textSetterScript.SetText(nextWord);
        //calculates the angle between the starting position and a random position on the ground
        double angle = Math.Atan2(-18f, UnityEngine.Random.Range(-11.5f, 11.5f) - pos.x);
        //randomizes the velocity
        float velo = UnityEngine.Random.Range(minVelo, maxVelo);
        //Calculates the x and y velocities based on the angle and the overall velocity
        xVelocity = (float)(Math.Cos(angle) * velo);
        yVelocity = (float)(Math.Sin(angle) * velo);
    }
    
    public void RequestNextWord()
    {
        Vector3 pos = transform.position;
        if (pos.y < 0)
        {
        	pos.z = -6;
        }
        transform.position = pos;
    }
    
    void SpawnFirePool(float poolWidth)
    {
	    GameObject fp = Instantiate(firePool, transform);
	    Vector3 pos = fp.gameObject.transform.position;
	    pos.z = 1;
	    pos.y = -8.0F;
	    fp.gameObject.transform.position = pos;
	    fp.gameObject.transform.parent = null;
	    fp.gameObject.GetComponent<SpawnFire>().PopulateArray(poolWidth);
    }
    
    public void Douse()
    {
    	textbox.gameObject.GetComponent<MeteorTextSetter>().Douse();
    }
}

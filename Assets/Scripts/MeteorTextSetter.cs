using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class MeteorTextSetter : MonoBehaviour
{
	public Color specialColor;
	public Color normalColor;
	public TMP_Text textbox;
	public BoxCollider2D col;
	[SerializeField]
	[Range(0.0F, 1.0F)]
	float fireSpawnChance;
	float textWidth;
	bool updateBounds;
    // Start is called before the first frame update
    void Awake()
    {
    	fireSpawnChance = StoreSliderSettings.Instance.GetSetting("FireFrequency");
    	updateBounds = false;
    }

    // Update is called once per frame
    void Update()
    {
    	if (updateBounds == true)
    	{
    		SetBounds();
    	}    
    }
    
    public void SetText(string newText)
    {
    	textbox.text = newText;
	textbox.color = normalColor;
    	if (UnityEngine.Random.Range(0.0F, 1.0F) <= fireSpawnChance)
    	{
    		textbox.color = specialColor;
    	}
    	updateBounds = true;
    }
    
    void SetBounds()
    {
    	col.size = textbox.textBounds.size;
    	updateBounds = false;
    	textWidth = col.size.x;
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
    	if (otherCollider.gameObject.tag == "Player")
    	{
    		otherCollider.gameObject.GetComponent<CollisionScript>().ObstacleCollision();
    		transform.parent.gameObject.GetComponent<MeteorMovement>().RequestNextWord();
    	}
    }
    
    public float GetTextWidth()
    {
    	return textWidth;
    }
    
    public bool CheckIfSpecial()
    {
    	return (textbox.color == specialColor);
    }
    
    public void Douse()
    {
    	textbox.color = normalColor;    	
    }
}

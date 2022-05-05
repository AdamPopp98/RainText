using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnFire : MonoBehaviour
{
	public float spacing;
	public GameObject fireSprite;
	private bool populated;
	private bool markedForRemoval;
	private float lingerDuration;
	
	void Awake()
	{
		lingerDuration = StoreSliderSettings.Instance.GetSetting("FireDuration");
		populated = false;
		markedForRemoval = false;
	}

    // Update is called once per frame
    void Update()
    {
    	if (populated == true && markedForRemoval == false)
    	{
    		markedForRemoval = true;
    		Destroy(this.gameObject, lingerDuration);		
    	}
    }
    
    public void PopulateArray(float w)
    {
    	int numFires = 1;
    	w -= spacing;
    	while (w >= spacing)
    	{
    		w -= spacing;
    		numFires++;
    	}
		float j = 0 - (numFires * 0.5F) * spacing;
    	for (int i = 0; i < numFires; i++)
    	{
	    	GameObject fire = Instantiate(fireSprite);
	    	Vector3 pos = transform.position;
			pos.x += j;
			pos.z = -1;
			fire.transform.position = pos;
			fire.transform.SetParent(this.transform);
			j += spacing;
    	}
    	SetColliderBounds(j);
    	populated = true;
    }
    
    private void SetColliderBounds(float w)
    {
    	GetComponent<BoxCollider2D>().size = new Vector2(w, 0.5f);
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
    	if (otherCollider.gameObject.tag == "Player")
    	{
    		otherCollider.gameObject.GetComponent<CollisionScript>().ObstacleCollision();
    	}
    }
}

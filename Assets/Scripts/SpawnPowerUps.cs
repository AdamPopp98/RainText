using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPowerUps : MonoBehaviour
{
	public GameObject[] powerUpPrefabs;
	public float timeBetween;
	[SerializeField]
	[Range(0.0F, 1.0F)]
	private float spawnChance;
	private float timeElapsed;
	private bool isHolding;
    void Awake()
    {
    	spawnChance = StoreSliderSettings.Instance.GetSetting("PowerUpFrequency");
    	timeElapsed = 0.0F;
    	isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeBetween)
        {
        	RNGSpawn();
        }
        if (isHolding == true && this.transform.childCount == 0)
        {
        	isHolding = false;
        }
    }
    
    void RNGSpawn()
    {
    	UnityEngine.Random.Range(0.0F, 1.0F);
        if (UnityEngine.Random.Range(0.0F, 1.0F) <= spawnChance)
        {
        	SpawnNewPowerUp();        	
        }
        timeElapsed = 0.0F;
    }
    
    void SpawnNewPowerUp()
    {
        System.Random rand = new System.Random((int)DateTime.UtcNow.Ticks);
        GameObject spawnedPowerUp = Instantiate(powerUpPrefabs[rand.Next(powerUpPrefabs.Length)]);
        spawnedPowerUp.gameObject.GetComponent<PowerUpBehaviour>().SetHolder(this.gameObject);
    }
    
    public void CollectPowerUp(GameObject powerUp)
    {
    	if (isHolding == false)
    	{
    		isHolding = true;
    		powerUp.transform.SetParent(this.transform);
    		powerUp.transform.position = transform.position;
    		powerUp.gameObject.GetComponent<PowerUpBehaviour>().SetHeld(true);
    	}
    	else
    	{
    		Destroy(powerUp);
    	}
    }
}

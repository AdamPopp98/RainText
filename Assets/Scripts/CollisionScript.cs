using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionScript : MonoBehaviour
{
	public GameObject playerLives;
	public float immunityDuration;
	public SpriteRenderer[] sprites;
	private float timeElapsed;
	private bool immune;
	private Animator animator;
	private PlaySoundFX FXplayer;
    // Start is called before the first frame update
    void Start()
    {
    	animator = this.gameObject.GetComponent<Animator>();
    	FXplayer = this.gameObject.GetComponent<PlaySoundFX>();
    	immune = false;
    }
    
    public void ObstacleCollision()
    {
    	if (immune == false)
    	{
    		ToggleImmune();
    		if (LoseLife() == true)
    		{
    			GameOverSequence();
    			return;   			
    		}
    		Invoke("ToggleImmune", immunityDuration);
    		FXplayer.PlayAudio(0.25F);
    	}
    }
    
    private void ToggleImmune()
    {
    	immune = !immune;
    }
    
    void GameOverSequence()
    {
    	this.gameObject.transform.GetChild(3).GetComponent<PlaySoundFX>().PlayAudio();
    	var cam = GameObject.Find("WindowCamera");
    	cam.gameObject.GetComponent<AudioSource>().mute = true;
    	Invoke("LoadGameOverScene", 0.5F);    	
    }
    
    void LoadGameOverScene()
    {
    	LevelManager.Instance.LoadScene("Game Over");   
    }
    
    bool LoseLife()
    {
    	bool gameOver = false;
    	for (int i = 0; i < playerLives.transform.childCount; i++)
    	{
    		HeartBeat heartScript = playerLives.transform.GetChild(i).gameObject.GetComponent<HeartBeat>();
    		if (i == playerLives.transform.childCount - 1)
			{
				gameOver = true;
			}
    		if (heartScript.GetAlive())
    		{
    			heartScript.ToggleAlive();
    			break;
    		}
    	}
	animator.SetTrigger("Immune");
    	return gameOver;
    }
    
    public bool isImmune()
    {
    	return immune;
    }
}

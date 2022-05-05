using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFX : MonoBehaviour
{
	private AudioSource src;
    // Start is called before the first frame update
    void Awake()
    {
    	src = this.gameObject.GetComponent<AudioSource>();
    }
    
    public void PlayAudio(float startPos = 0.0F)
    {
    	src.time = startPos;
    	src.Play();	    	    	
    }
}

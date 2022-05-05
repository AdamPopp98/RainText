using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFXAfterDestroy : MonoBehaviour
{
	public AudioSource aud;
    
    public void PlayAudio()
    {
    	this.transform.SetParent(PreserveBackground.Instance.transform, true);
    	aud.Play();
    	Destroy(this.gameObject, aud.clip.length);    	
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
	public AudioSource musicAudio;

	public void ChangeMusicVolume(float newVol)
	{
		musicAudio.volume = newVol;
	}
}

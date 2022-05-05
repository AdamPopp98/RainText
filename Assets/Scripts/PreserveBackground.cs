using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreserveBackground : MonoBehaviour
{
	public static GameObject Instance;
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this.gameObject;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}

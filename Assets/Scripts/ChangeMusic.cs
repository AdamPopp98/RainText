using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class ChangeMusic : MonoBehaviour
{
	private KeyCode prev;
	private KeyCode next;
	private KeyCode alt;
	private Scene curScene;
	public AudioClip[] songs;
	public AudioSource music;
	public float lastChange;
	private int curSong;
	public static GameObject Instance;

	void Awake()
	{
		curScene = SceneManager.GetActiveScene();
		curSong = 0;
		music.clip = songs[curSong];
		music.Play();
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
	
	void Start()
	{
		ChangeBindings();
	}
	
	void ChangeBindings()
	{
		prev = RebindKeys.Instance.GetBinding("PrevSong");
		next = RebindKeys.Instance.GetBinding("NextSong");
		alt = RebindKeys.Instance.GetBinding("Modify");
	}
	
	void OnSceneChange()
	{
		if (curScene != SceneManager.GetActiveScene())
		{
			this.gameObject.GetComponent<AudioSource>().mute = false;
			curScene = SceneManager.GetActiveScene();
			ChangeBindings();
		}
	}

    // Update is called once per frame
    void Update()
    {
    	ChangeVolume();
    	if (lastChange > 1)
    	{
    		if (Input.GetKey(next) && Input.GetKey(alt))
			{
				curSong = songs.Length - 1;
			}
    	    else if (Input.GetKey(next))
			{
				if (curSong == 0)
				{
					curSong = songs.Length - 2;
				}
				else
				{
					curSong--;
				}
			}
			else if (Input.GetKey(prev))
			{
				if (curSong > songs.Length - 3)
				{
					curSong = 0;
				}
				else
				{
					curSong++;
				}
			}
			
			if (music.clip != songs[curSong])
			{
				lastChange = 0;
				music.clip = songs[curSong];
				music.Play();
			}
			OnSceneChange();
    	}
    	else
    	{
    		lastChange += Time.deltaTime;    		
    	}
    }
    
    void ChangeVolume()
    {
		if (Input.GetKey(KeyCode.Minus))
			{
				AudioListener.volume = Math.Max(AudioListener.volume - (0.2F * Time.deltaTime), 0.0F);				
			}
		else if (Input.GetKey(KeyCode.Equals))
		{
				AudioListener.volume = Math.Min(AudioListener.volume + (0.2F * Time.deltaTime), 1.0F);
		}
    }
}

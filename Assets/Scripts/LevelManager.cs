using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;
	private bool loading;
	
	
	void Awake()
	{
		loading = false;
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	public async void LoadScene(string sceneName)
	{
		if (loading == false)
		{
			loading = true;
			var scene = SceneManager.LoadSceneAsync(sceneName);
			scene.allowSceneActivation = false;
			await Task.Delay(100);
			while (scene.progress < 0.9F)
			{
				await Task.Delay(100);
			}
			await Task.Delay(1000);
			scene.allowSceneActivation = true;
			loading = false;
		}
	}
	
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			ExitGame();
		}
	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
}

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSliderSettings : MonoBehaviour
{
	public static StoreSliderSettings Instance;
	private Dictionary<string, float> sliderSettings;
	private Dictionary<string, float> defSliderSettings;
	private const float defNumWords = 4.0F;
	private const float defMinVelo = 6.0F;
	private const float defMaxVelo = 10.0F;
	private const float defPowerUpFrequency = 0.10F;
	private const float defFireFrequency = 0.15F;
	private const float defFireDuration = 4.0F;
	private const float defVolume = 0.50F;

    // Start is called before the first frame update
    void Awake()
    {
    		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		sliderSettings = new Dictionary<string, float>();
		sliderSettings.Add("NumWords", defNumWords);
		sliderSettings.Add("MinVelo", defMinVelo);
		sliderSettings.Add("MaxVelo", defMaxVelo);
		sliderSettings.Add("PowerUpFrequency", defPowerUpFrequency);
		sliderSettings.Add("FireFrequency", defFireFrequency);
		sliderSettings.Add("FireDuration", defFireDuration);
		sliderSettings.Add("Volume", defVolume);
		
		//TODO replace this with an array to reduce memory usage.
		defSliderSettings = new Dictionary<string, float>();
		defSliderSettings.Add("NumWords", defNumWords);
		defSliderSettings.Add("MinVelo", defMinVelo);
		defSliderSettings.Add("MaxVelo", defMaxVelo);
		defSliderSettings.Add("PowerUpFrequency", defPowerUpFrequency);
		defSliderSettings.Add("FireFrequency", defFireFrequency);
		defSliderSettings.Add("FireDuration", defFireDuration);
		defSliderSettings.Add("Volume", defVolume);        
    }
    
    void Start()
    {
    	ChangeMusicVolume();
    }
    
    public Dictionary<string, float> GetSliderSettings()
    {
    	return sliderSettings;
    }
    
    
    //Called externally when a slider value is changed. Used to modify the settings dictionary accordingly
    public void ChangeSetting(string settingToChange, float newValue)
    {
    	foreach(KeyValuePair<string, float> setting in sliderSettings)
	{
		if (setting.Key == settingToChange)
		{
			sliderSettings[setting.Key] = newValue;
			if (setting.Key == "Volume")
			{
				ChangeMusicVolume();
			}
			Debug.Log(String.Format("{0} was changed to {1}.", setting.Key, newValue.ToString()));
			return;
		}
	}
    }
    
    public bool GetIsDefault()
    {
    	foreach(KeyValuePair<string, float> setting in sliderSettings)
	{
		if (sliderSettings[setting.Key] != defSliderSettings[setting.Key])
		{
			//Returns false if a setting is not its default value.
			return false;
		}
	}
	//Returns true if all settings are the default values.
	return true;
    }
    
    public void ResetSettings()
    {
    	/*Changes each setting back to its original value.
    	
    	NOTE: using foreach loop to reset settings causes an error*/
    	string curKey;
        for (int i = 0; i < sliderSettings.Count; i++)
	{
		curKey = sliderSettings.ElementAt(i).Key;
		sliderSettings[curKey] = defSliderSettings[curKey];
	}
	ChangeMusicVolume();
    }
    
    
    //Called externally by game objects to get specific settings they require.
    public float GetSetting(string settingName)
    {
    	return sliderSettings[settingName];
    }
    
    
    /*TODO potentially change this to a public function and call this when the music volume slider is changed.
    This could potentially improve performance by eliminating an if statement in the ChangeSetting method.*/
    private void ChangeMusicVolume()
    {
    	this.gameObject.GetComponent<ChangeVolume>().ChangeMusicVolume(sliderSettings["Volume"]);    	    	
    }
}

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RebindKeys : MonoBehaviour
{	

	public static RebindKeys Instance;
	private Dictionary<string, KeyCode> KeyBindings;
	private Dictionary<string, KeyCode> defKeyBindings;
	private KeyCode defJump = KeyCode.UpArrow;
	private KeyCode defMoveLeft = KeyCode.LeftArrow;
	private KeyCode defMoveRight = KeyCode.RightArrow;
	private  KeyCode defPrevSong = KeyCode.LeftShift;
	private  KeyCode defNextSong = KeyCode.RightShift;
	private  KeyCode defUsePowerUp = KeyCode.Space;
	private  KeyCode defModify = KeyCode.RightControl;
	
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
		KeyBindings = new Dictionary<string, KeyCode>();
		KeyBindings.Add("Jump", defJump);
		KeyBindings.Add("MoveLeft", defMoveLeft);
		KeyBindings.Add("MoveRight", defMoveRight);
		KeyBindings.Add("PrevSong", defPrevSong);
		KeyBindings.Add("NextSong", defNextSong);
		KeyBindings.Add("UsePowerUp", defUsePowerUp);
		KeyBindings.Add("Modify", defModify);
		
		
		//TODO replace this with an array to reduce memory usage.
		defKeyBindings = new Dictionary<string, KeyCode>();
		defKeyBindings.Add("Jump", defJump);
		defKeyBindings.Add("MoveLeft", defMoveLeft);
		defKeyBindings.Add("MoveRight", defMoveRight);
		defKeyBindings.Add("PrevSong", defPrevSong);
		defKeyBindings.Add("NextSong", defNextSong);
		defKeyBindings.Add("UsePowerUp", defUsePowerUp);
		defKeyBindings.Add("Modify", defModify);
	}
    
    public string ResetKeyBindings()
    {
    	/*Changes each binding back to its original value.
    	
    	NOTE: using foreach loop to reset bindings causes an error*/
    	string curKey;
    	for (int i = 0; i < KeyBindings.Count; i++)
		{
			curKey = KeyBindings.ElementAt(i).Key;
			KeyBindings[curKey] = defKeyBindings[curKey];
		}
		//This message gets displyed.
    	return "All bindings have been reset.";       	   	
    }
    
    /*Called externally in order to get all key bindings.
    
    TODO check to see if code can be refactored to where this method can be replaced with GetBinding*/
    public Dictionary<string, KeyCode> GetBindings()
    {
    	return KeyBindings;
    }
    
    //Called externally by gameobjects in order to get the correct binding for a specific action.
    public KeyCode GetBinding(string action)
    {
    	return KeyBindings[action];
    }
    
    public string ChangeBinding(string actionToRebind, KeyCode newBinding)
    {
    	if (KeyBindings.ContainsValue(newBinding))
    	{
    		//This message gets displayed.
    		return String.Format("{0} is already bound.", newBinding);		
    	}
    	else
    	{
    		foreach(KeyValuePair<string, KeyCode> binding in KeyBindings)
    		{
    			if (binding.Key == actionToRebind)
    			{
    				KeyBindings[binding.Key] = newBinding;
    				//This message gets displayed.
    				return String.Format("{0} was rebound to {1}.", binding.Key, newBinding.ToString());
    			}
    		}
    		//This should never occur but is required to prevent compile time error
    		return "Error action not found";
    	}
    }
    
    public string PreviewBinding(string actionToPreview)
    {
    	    foreach(KeyValuePair<string, KeyCode> binding in KeyBindings)
    		{
    			if (binding.Key == actionToPreview)
    			{
    				//This message gets displayed.
    				return String.Format("{0} is bound to {1}.", binding.Key, binding.Value.ToString());
    			}
    		}
    		//This should never occur but is required to prevent compile time error
    		return "Error action not found";	
    }
    
    public bool GetIsDefault()
    {
		foreach(KeyValuePair<string, KeyCode> binding in KeyBindings)
		{
			if (KeyBindings[binding.Key] != defKeyBindings[binding.Key])
			{
				//Returns false if a binding is not its default value.
				return false;
			}
		}
		//Returns true if all binding are the default values.
    	return true;
    }
}

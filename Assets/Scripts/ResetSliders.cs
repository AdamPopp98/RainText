using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;


public class ResetSliders : MonoBehaviour
{
	void Awake()
	{
		//Sets 
		SetLock();	
	}
	
	//Resets all setting values and adjusts sliders accordingly.
	public void Reset()
	{
		//Resets the values in the settings dictionary back to the default values.
		StoreSliderSettings.Instance.ResetSettings();
		
		GameObject parent = transform.parent.gameObject;
		
		//children is one less than the childCount because the reset button is the last child.
		int children = parent.gameObject.transform.childCount - 1;
		
		//Slider UI values are then adjusted to match updated values.
		for (int i = 0; i < children; i++)
		{
			GameObject current = parent.transform.GetChild(i).GetChild(1).gameObject;
			current.gameObject.GetComponent<SliderScript>().ResetSetting();
		}
		SetLock();
	}
	
	//Makes the button interactable only when all slider values are the default values.
	public void SetLock()
	{
		/*
		This one line of code is encapsulated in a method because 
		it is called externally when a slider value is changed.
		*/
		this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = !StoreSliderSettings.Instance.GetIsDefault();
	}
}

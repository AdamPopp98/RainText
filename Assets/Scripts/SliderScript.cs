using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
	public string settingToChange;
	public string sliderName;
	public string unitType;
	public TextMeshProUGUI sliderTitle;
	public TextMeshProUGUI currentValue;
	private Slider slider;
	
	void Awake()
	{
		slider = this.gameObject.GetComponent<Slider>();
		sliderTitle.text = sliderName;
		float settingVal = StoreSliderSettings.Instance.GetSetting(settingToChange);
		slider.value = (unitType == "%") ? settingVal * 100.0F : settingVal;
	}
	
	void Start()
	{
		ChangeValue();		
	}
	
	public void ResetSetting()
	{
		float settingVal = StoreSliderSettings.Instance.GetSetting(settingToChange);
		slider.value = (unitType == "%") ? settingVal * 100.0F : settingVal;
		currentValue.text = slider.value.ToString() + unitType;
	}
	
    
    public void ChangeValue()
    {
    	currentValue.text = slider.value.ToString() + unitType;
    	float val = (unitType == "%") ? slider.value * 0.01F : slider.value;
    	StoreSliderSettings.Instance.ChangeSetting(settingToChange, val);
    }
}

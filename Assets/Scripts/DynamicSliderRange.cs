using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicSliderRange : MonoBehaviour
{
	public bool dynamicMin;
	public Slider otherSlider;
	private Slider thisSlider;
    // Start is called before the first frame update
    void Awake()
    {
    	thisSlider = this.gameObject.GetComponent<Slider>();        	        
    }

    // Update is called once per frame
    void Update()
    {
    	if (dynamicMin)
    	{
    		ChangeMin();
    	}
    	else
    	{
    		ChangeMax();
    	}
    }
    
    private void ChangeMin()
    {
        if (otherSlider.value > thisSlider.value)
    	{
    		thisSlider.value = otherSlider.value;
    	}
    	if (otherSlider.value != thisSlider.minValue)
    	{
    		thisSlider.minValue = otherSlider.value;
    	}
    }
    
    private void ChangeMax()
    {
    	if (otherSlider.value < thisSlider.value)
    	{
    		thisSlider.value = otherSlider.value;
    	}
    	if (otherSlider.value != thisSlider.maxValue)
    	{
    		thisSlider.maxValue = otherSlider.value;
    	}    	
    }
}

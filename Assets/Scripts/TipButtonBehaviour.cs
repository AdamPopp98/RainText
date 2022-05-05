using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipButtonBehaviour : MonoBehaviour
{
	public string content;
	public TextMeshProUGUI tipTitle;
	public TextMeshProUGUI tipText;
	private string buttonTitle;
	
	void Awake()
	{
		buttonTitle = this.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
		if (buttonTitle == "Player Movement")
		{
			UpdateTipWindow();
		}
	}
    
    public void UpdateTipWindow()
    {
    	tipTitle.text = buttonTitle;
    	tipText.text = content;
    }
}

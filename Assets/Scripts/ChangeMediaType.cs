using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ChangeMediaType : MonoBehaviour
{
	public TextMeshProUGUI currentSource;
	public TextMeshProUGUI currentPreview;
	void Awake()
	{
		ChangeType();		
	}
	public void ChangeType()
	{
		currentPreview.text = String.Format("Enter the {0} you want to use...", currentSource.text);
	}
}

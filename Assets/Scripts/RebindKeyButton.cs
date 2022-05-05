using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RebindKeyButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
	public bool resetBindings;
	public string action;
	public TextMeshProUGUI label;
	public TextMeshProUGUI message;
	private bool listening;
	private bool locked;
	private KeyCode newBinding;
	
	void Awake()
	{
		SetLock(false);
		listening = false;
		label.text = action;	
	}
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!locked && !resetBindings)
		{
			message.text = RebindKeys.Instance.PreviewBinding(action);
		}
		else if (!locked)
		{
			message.text = "Click to reset key bindings.";
		}
	}
	
	public void OnSelect(BaseEventData eventData)
	{
		if (!locked && !resetBindings)
		{
			message.text = RebindKeys.Instance.PreviewBinding(action);
		}
		else if (!locked)
		{
			message.text = "Click to reset key bindings.";
		}
	}
	
	void Update()
	{
		if (listening)
		{
			foreach(KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
		    {
				if(Input.GetKeyDown(vkey))
				{
					newBinding = vkey;
					listening = false;
					message.text = RebindKeys.Instance.ChangeBinding(action, vkey);
					SetLocks(false);
				}
			}
		}
	}
	
	public void Rebind()
	{
		if (!locked)
		{
			SetLocks(true);
			message.text = String.Format("Ready to rebind: {0}...", action);
			listening = true;			
		}
	}
	
	public void ResetBindings()
	{
		if (!locked)
		{
			message.text = RebindKeys.Instance.ResetKeyBindings();
			SetLock(true);
		}		
	}
	
	private void SetLocks(bool isLocked)
	{
		GameObject parent = transform.parent.gameObject;
		int children = parent.gameObject.transform.childCount;
		for (int i = 0; i < children; i++)
		{
			RebindKeyButton buttonScript = parent.gameObject.transform.GetChild(i).gameObject.GetComponent<RebindKeyButton>();
			buttonScript.SetLock(isLocked);
		}
	}
	
	public void SetLock(bool isLocked)
	{
		if (!resetBindings)
		{
			locked = isLocked;
			this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = !locked;
			return;
		}
		locked = RebindKeys.Instance.GetIsDefault();
		this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = !locked;
	}
	
	public bool GetLocked()
	{
		return locked;
	}
}

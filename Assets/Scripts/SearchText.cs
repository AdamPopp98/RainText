using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SearchText : MonoBehaviour
{
	public TextMeshProUGUI currentSource;
	public TextMeshProUGUI currentTitle;
	public TextMeshProUGUI errorMessage;
	public GameObject loadIcon;
	public string UrlPrefix;
	
	void Awake()
	{
		errorMessage.text = "";
    	loadIcon.gameObject.SetActive(false);		
	}
	
    public void Search()
    {
    	if (currentTitle.text == "")
    	{
    		errorMessage.text = "Error no query was entered.";
    		return;
    	}
    	string medium = currentSource.text.Split(' ')[0];
    	string title = currentTitle.text;
    	currentTitle.text = "";
    	errorMessage.text = "";
    	string URL = String.Format("{0}/Request/{1}/{2}", UrlPrefix, medium, title);
    	Debug.Log(URL);
    	loadIcon.gameObject.SetActive(true);
    	StartCoroutine(MakeApiRequest(URL));
    }
    
    private IEnumerator MakeApiRequest(string Url)
    {
    	using (UnityWebRequest request = UnityWebRequest.Get(Url))
    	{
    	    request.SetRequestHeader("Content-Type", "application/json");
    	    yield return request.SendWebRequest();
    	    JsonResponse response = JsonUtility.FromJson<JsonResponse>(request.downloadHandler.text);    	    
    	    if (response.status == "ERROR")
    	    {
    	    	loadIcon.gameObject.SetActive(false);
    	    	errorMessage.text = response.message;
    	    }
    	    else
    	    {
    	    	//UpdateInputFile(response.message);
    	    	WriteToFile((string)response.message);
    	    	loadIcon.gameObject.SetActive(false);    	    		    	
    	    }
    	}
    }
    
    private void UpdateInputFile(string inputText)
    {
    	inputText = FormatText(inputText);
    	WriteToFile(inputText);
    }
    
    private void WriteToFile(string text)
    {
    	Directory.CreateDirectory(Application.streamingAssetsPath + "/text_files");
    	string filePath = Application.streamingAssetsPath + "/text_files/input_file.txt";
    	File.WriteAllText(filePath, text);   	
    	    	    	
    }
    
    private string FormatText(string text)
    {
    	string[] inputArray = text.Split(new string[] {"\n", " ", "|"}, StringSplitOptions.None);
    	string[] charsToRemove = new string[] { "\n", "\r", " "};
    	foreach (string c in charsToRemove)
    	{
			for (int i = 0; i < inputArray.Length; i++)
			{
				inputArray[i] = inputArray[i].Replace(c, string.Empty);
			}
		}
		text = "";
		for (int i = 0; i < inputArray.Length; i++)
		{
			if (inputArray[i] != string.Empty)
			{
				text = String.Format("{0}{1} ", text, inputArray[i]);
			}			
		}
		return text; 
    }
    
    private class JsonResponse
    {
    	public string status;
    	public string message;
    }    
}

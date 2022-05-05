using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class ParseWords : MonoBehaviour
{
	public static GameObject Instance;
	public GameObject WordMeteor;
	public string[] inputText;
	public int currentIndex;
	int maxWords;
	float deactivateTime;
	int numWords;
	
	void Awake()
	{
		maxWords = (int)StoreSliderSettings.Instance.GetSetting("NumWords");
	    currentIndex = 0;
    	string filepath = Application.streamingAssetsPath + "/text_files/input_file.txt";
    	string words = File.ReadAllText(filepath);
    	inputText = words.Split(new string[] {"\n", " ", "|"}, StringSplitOptions.None);
    	numWords = inputText.Length;
	}

    // Update is called once per frame
    void Update()
    {
    	if (this.transform.childCount < maxWords)
    	{
    		GenerateMeteors();
    	}
		UpdateMeteors();
    }
    
    bool NotAWord(string word)
    {
    	switch(word)
    	{
    		case ",":
    		case "\n":
    		case " ":
    		case ".":
    		case "!":
    		case "?":
    		case ":":
    		case ";":
    			return true;
    		default:
    			return false;
    	}
    }
    
    string GetNextWord()
    {
    	if (currentIndex >= numWords)
	{
		return "Victory!";
	}
    	while (inputText[currentIndex] == "" || inputText[currentIndex] == "-")
    	{
    		currentIndex++;
    		if (currentIndex >= numWords)
    		{
    			return "Victory!";
    		}
    	}
    	string nextWord = inputText[currentIndex];
    	currentIndex++;
    	return nextWord;    
    }
    
    void GenerateMeteors()
    {
    	while (this.transform.childCount < maxWords)
    	{
		GameObject meteor = Instantiate(WordMeteor);
		meteor.transform.SetParent(this.transform);
		meteor.GetComponent<MeteorMovement>().GenerateWord(GetNextWord());
    	}
    }
    
    void UpdateMeteors()
    {
         int children = transform.childCount;
         for (int i = 0; i < children; i++)
         {
         	GameObject meteor = transform.GetChild(i).gameObject;
         	Vector3 pos = meteor.transform.position;
		if (pos.z != -5)
		{
			pos.z = -5;
			meteor.GetComponent<MeteorMovement>().GenerateWord(GetNextWord());
		}
         }
    }
    
    public void DouseMeteors()
    {
    	int children = transform.childCount;
    	for (int i = 0; i < children; i++)
    	{
    		GameObject meteor = transform.GetChild(i).gameObject;
    		meteor.GetComponent<MeteorMovement>().Douse();    		
    	}
    }
    
    public void FreezeMeteors()
    {
        int children = transform.childCount;
    	for (int i = 0; i < children; i++)
    	{
    		GameObject meteor = transform.GetChild(i).gameObject;
    		meteor.GetComponent<MeteorMovement>().enabled = false;
    	}
    	Invoke("UnfreezeMeteors", 3.0F);
    }
    
    private void UnfreezeMeteors()
    {
    	int children = transform.childCount;
    	for (int i = 0; i < children; i++)
    	{
    		GameObject meteor = transform.GetChild(i).gameObject;
    		meteor.GetComponent<MeteorMovement>().enabled = true;
    	}    
    }
    
    public void SetNumMeteors(int num)
    {
    	maxWords = num;
    }
    
}

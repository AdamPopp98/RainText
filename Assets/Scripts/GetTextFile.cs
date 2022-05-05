using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetTextFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	Directory.CreateDirectory(Application.streamingAssetsPath + "/text_files/");
    	string fileName = Application.streamingAssetsPath + "/text_files/" + "input_file.txt";
    	string importFrom = "Assets/input_file.txt";
    	File.WriteAllText(fileName, File.ReadAllText(importFrom));
    }
}

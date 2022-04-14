using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Logger : MonoBehaviour
{
    public static Logger Instance {get; private set; }

    public Text loggerText; 
    public int maxLines; 

    int currentLines; 

    void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);  
		}
        else {
            Instance = this;   
		}
	}

    public void AppendText(string text) {
        currentLines += 1; 

        if(currentLines > maxLines) {
            // -- remove first line 
            string[] lines = loggerText.text.Split("\n").Skip(1).ToArray(); 

            Debug.Log(lines.Length); 
            // -- start fresh 
            loggerText.text = ""; 

            // -- add back all other lines 
            foreach(string line in lines) {
                loggerText.text += "\n" + line; 
			}

            // -- add new line 
            loggerText.text += "\n" + text; 
		}
        else {
            loggerText.text += "\n" + text;
		}

	}
}

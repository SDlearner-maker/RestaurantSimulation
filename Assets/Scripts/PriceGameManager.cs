using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceGameManager : MonoBehaviour
{
	public Text outputText;
	
    // Start is called before the first frame update
    void Start()
    {
        outputText.text = "How are you";
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	public void SetText(string result)
	{
		outputText.text = result;
	}
}

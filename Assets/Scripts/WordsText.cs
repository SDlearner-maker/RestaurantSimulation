using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class WordsText : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject makeorder;
    public Text CustomerDialog;
    public GameObject customerdialogbox;
    [HideInInspector]
    public string order="", order1="", order2="";  
    
    public void TakeInput(string dialog, string intent)
    {
        customerdialogbox.SetActive(true);
        CustomerDialog.text = dialog;
        //makeorder.SetActive(true);

        if (intent == "q1")
        {
            ConvertToWords(dialog);
            makeorder.SetActive(true);
        }
        
    }
    public void ConvertToWords(string s)
    {
        
        
        string[] textSplit = s.Split();

        int prev= Array.IndexOf(textSplit, "want");
        int endloop = 0;
        int mid = 0;
        
        if (Array.Exists(textSplit, x => x == "and"))
        {
            mid = Array.IndexOf(textSplit, "and");
            endloop = mid;

            for (int i = mid + 1; i < textSplit.Length; i++)
            {
                order2 = order2 + " " + textSplit[i];
            }
           
            if (order2.Contains(".")) { order2 = order2.Replace(".", ""); }
        }
        else { endloop = textSplit.Length; order2 = ""; }

        for (int i=prev+1; i<endloop; i++)
        {
            order1= order1+" "+textSplit[i];
        }
        if (order1.Contains(".")) { order1 = order1.Replace(".", ""); }
        
        Debug.Log("order1= "+order1+ " order2= " + order2);
        gamemanager.GetComponent<Billing>().BillofCustomer(order1, order2);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*if (Array.Exists(textSplit, x => x == "cups"))
        {
            int cupsof = Array.IndexOf(textSplit, "cups");
            textSplit[cupsof + 2] = char.ToUpper(textSplit[cupsof + 2][0]) + textSplit[cupsof + 2].Substring(1);
        }
        else if (Array.Exists(textSplit, x => x == "cup"))
        {
            int cupof = Array.IndexOf(textSplit, "cup");
            textSplit[cupof + 2] = char.ToUpper(textSplit[cupof + 2][0]) + textSplit[cupof + 2].Substring(1);
        }
        else { textSplit[textSplit.Length - 1] = char.ToUpper(textSplit[textSplit.Length - 1][0]) + textSplit[textSplit.Length - 1].Substring(1); }*/

    //Debug.Log(textSplit[i]);            
    //if (i > 0) { if (textSplit[i-1] == "want") { menu.Add(textSplit[i]); } }
    //if(i< textSplit.Length-1) { if (textSplit[i + 1] == "and") { menu.Add(textSplit[i]); } }
    //Debug.Log("text:" + word); 
}

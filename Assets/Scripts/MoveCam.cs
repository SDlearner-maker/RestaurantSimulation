using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour
{
    public GameObject myCam;
    public GameObject KitchenCam;
    public GameObject GotoKitchenButton;
    private Vector3 originalPosition;
    public GameObject customerdialogbox;
    public GameObject calcinstructions;
    public GameObject doneallbutton;
    //public GameObject kitinstructionsbox;
    //public Text kitinstructtions;
    [HideInInspector]
    public bool gotooriginal=false;
    public bool gotokitchen = false;

    // Start is called before the first frame update
    void Start()
    {
        myCam.SetActive(true);
        KitchenCam.SetActive(false);
        GotoKitchenButton.SetActive(false);        
        calcinstructions.SetActive(false);
        doneallbutton.SetActive(false);
        //kitinstructionsbox.SetActive(false);

        originalPosition = myCam.transform.position;
    }

    public void Decisiontooriginal(bool goyes)
    {
        gotooriginal = goyes;
    }
    public void MoveCamtoOriginal()
    {
        //myCam.transform.position = originalPosition;
        gotooriginal = true;
        if (gotooriginal == true) { myCam.transform.position = originalPosition; }
        Debug.Log(gotooriginal);
        GotoKitchenButton.SetActive(true);
        calcinstructions.SetActive(false);
        
    }

    public void SetKitchenUIinactive()
    {
        doneallbutton.SetActive(false);
        //kitinstructionsbox.SetActive(false);

    }
    public void Movecamerathere()
    {
        var computerPosition = new Vector3(125, 6, -484);
        myCam.transform.position = computerPosition;

        calcinstructions.SetActive(true);
        customerdialogbox.SetActive(false);        
    }

    /*
    public void SeeGoKitchenButton()
    {
        GotoKitchenButton.SetActive(true);
    }*/
    public void Movetokitchen()
    {
        KitchenCam.SetActive(true);
        //doneallbutton.SetActive(true);
        //kitinstructionsbox.SetActive(true);
        myCam.SetActive(false);
        GotoKitchenButton.SetActive(false);        

        //Quaternion kitchenRotation = Quaternion.Euler(0, 90, 0);
        //var kitchenPosition = new Vector3(58, -15, -676);
        //myCam.transform.rotation = kitchenRotation;
        //myCam.transform.position = kitchenPosition;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}

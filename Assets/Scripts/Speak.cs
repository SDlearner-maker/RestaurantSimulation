using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speak : MonoBehaviour
{
    public GameObject script;
    public GameObject greetbutton;
    public GameObject greetawaybutton;
    public Text instructionforuser;
    public GameObject customerdialogbox;
    private GameObject currentbutton;
    private string english;
    private string cantonese;
    [HideInInspector]
    public Animator anim;
    public bool walk;
    // Start is called before the first frame update
    void Start()
    {
        customerdialogbox.SetActive(false);
        greetbutton.SetActive(true);
        //StartCoroutine(StartCaptureAfterTime(0, 4)); //Start capture after 0s and stop capture after 5s
        /*
        anim = GetComponent<Animator>();
        walk = true;
        */
    }

    // Update is called once per frame

    public void StartRec()
    {        
        StartCoroutine(StartCaptureAfterTime(0, 4));

        if (greetbutton.active == true) { 
            greetbutton.SetActive(false);
            currentbutton = greetbutton;
            //Debug.Log(currentbutton.name);
            //UnityEngine.UI.Button btn = greetbutton.GetComponent<UnityEngine.UI.Button>();
            //btn.onClick.AddListener(TaskOnClick);
        }
        if (greetawaybutton.active == true)
        {
            greetawaybutton.SetActive(false);
            currentbutton = greetawaybutton;
            //Debug.Log(currentbutton.name);
        }
        //Debug.Log("Clicked");
    }

    public string TaskOnClick()
    {
        //Debug.Log("You have clicked the button!");
        return currentbutton.name;
    }
    void Update()
    {
        /*
        if (walk)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }*/
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Left")
        {
            transform.Rotate(0f, -90f, 0.0f);
        }

        if (other.gameObject.name == "Stop")
        {
            walk = false;
            anim.Play("Idle");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Left")
        {
            Destroy(other.gameObject);
            StartCoroutine(StartCaptureAfterTime(0,4)); //Start capture after 0s and stop capture after 5s
        }
    } 
    */

    public IEnumerator StartCaptureAfterTime(float startTime, float Endtime)
    {
        yield return new WaitForSeconds(startTime);

        script.GetComponent<MicrophoneCapture>().StartCapture();
        StartCoroutine(StopCaptureAfterTime(Endtime));
    }

    public IEnumerator StopCaptureAfterTime(float Endtime)
    {
        yield return new WaitForSeconds(Endtime);

        script.GetComponent<MicrophoneCapture>().StopCapture();

        if (currentbutton == greetbutton) { 
        english = "Let the customer read the order for sometime... wait for around 10 seconds...";
        cantonese = "客人正在看餐單，請等待大概 10 秒。";
        instructionforuser.text = english + "\n" + cantonese;
        }
    }

}

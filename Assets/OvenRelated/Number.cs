using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour {
    
    // Text Panel
    private Text Enter_text;
    public Text Panel;
    // Number button
    public Button[] BtnNumber;
    // Start
    public Button BtnStart;
    // Clear
    public Button BtnClear;
    // For door
    public GameObject Door;
    public Button BtnOpen;
    public Button BtnClose;
    public bool d_open =false;
    public bool d_close = true;
    //　Start Countdown
    public bool count = false;
	
	public int operated = 0;
    // For Light
    private GameObject Lamp;
    // For sound
    public AudioClip pushsound;
    public AudioClip opensound;
    public AudioClip finishsound;
    public AudioClip cancelsound;
    public AudioClip startsound;
    AudioSource audioSource;
    // For TurnTable
    public bool turn=false;
    // For Button
    public bool pressed=false;

    // Use this for initialization
    void Start () {
        Panel.text="00:00";
        Door=GameObject.Find("Bar");
        Lamp=GameObject.Find("L_Cube");
        audioSource = GetComponent<AudioSource>();
    }

    string entertime;
    float totalmSeconds= 10f;

    // Update is called once per frame
    void Update () {
        entertime = Panel.text;
        string[]  Stime = entertime.Split(':');
        //Debug.Log(Stime[1]);
        string str_sec = Stime[1];
        string str_min = Stime[0];
        if(count){
            Lamp.GetComponent<Renderer>().material.SetColor("_Color",new Color32(191,49,25,255));
            Lamp.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(191,49,25)*0.016f);
            Lamp.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            Lamp.GetComponent<Light>().enabled= true;
            int totalSeconds = int.Parse(str_sec);
            int totalMinutes = int.Parse(str_min);
            totalmSeconds -= Time.deltaTime;
            //Debug.Log(totalmSeconds);
            if(0f >= totalmSeconds && 0 >= totalSeconds && 0 >= totalMinutes){
                audioSource.PlayOneShot(finishsound);
                count=false;
                turn=false;
            }else if(0f >= totalmSeconds && 0 >= totalSeconds){
                totalMinutes -= 1;
                totalSeconds = 59;
                //totalmSeconds = 10f;
                totalmSeconds = 2f;
                Panel.text= totalMinutes.ToString("D2")+ ":" + totalSeconds.ToString("D2");
            }else if(0f >= totalmSeconds){
                totalSeconds -=1;
                //totalmSeconds = 10f;
                totalmSeconds = 2f;
                Panel.text= totalMinutes.ToString("D2")+ ":" + totalSeconds.ToString("D2");
                //Debug.Log(totalSeconds);
            }
            else{
                totalmSeconds -= Time.deltaTime;
                Panel.text= totalMinutes.ToString("D2")+ ":" + totalSeconds.ToString("D2");
                //Debug.Log(totalmSeconds);
            }
        }
//        string entertime = Panel.text;
//        string[]  Stime = entertime.Split(':');
//        string str_sec = Stime[1];
//        string str_min = Stime[0];
//        if(count!=false){
//            int old_seconds = int.Parse(str_sec);
//            float total_time = int.Parse(str_min)*60 + old_seconds;
//            total_time -= Time.deltaTime;
//            int int_time = (int)total_time;
//            int minutes = int_time / 60;
//            int seconds = int_time - (minutes * 60); 
//            Debug.Log(int_time);
//            Debug.Log(minutes);
//            Debug.Log(seconds);
//            if(seconds != old_seconds){
//                Panel.text= minutes.ToString("D2")+ ":" + seconds.ToString("D2");
//            }else if(total_time <= 0f){
//                count=false;
//                Panel.text = "00:00";
//            }else{
//                return;
//            }
//        }
        if(!count){
            Lamp.GetComponent<Renderer>().material.color = new Color32(255,255,255,255);
            Lamp.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
            Lamp.GetComponent<Light>().enabled=false;
        }
    }

    // Number button
    public void InputNumber(Text number){
        string first = Panel.text.Substring(1,1);
        string second = Panel.text.Substring(3,1);
        string third = ":";
        string fourth = Panel.text.Substring(4,1);
        Panel.text = first + second + third+ fourth + number.text;
        audioSource.PlayOneShot(pushsound);
        //Debug.Log(Panel.text);
    }

    // Start button
    public void InputStart(int Itemvalue){
        string st_min = Panel.text.Substring(0,2);
        string st_sec = Panel.text.Substring(3,2);
        int int_min=int.Parse(st_min);
        int int_sec=int.Parse(st_sec);
        int total_time = int_min*60 + int_sec;
        if (d_open==true||Panel.text=="00:00"){
            Debug.Log("Door Opened!!");
        }else if(int_sec>59||int_min>60){
            Debug.Log("Invalid Time!!");
        }else if(total_time!=Itemvalue){
            Debug.Log("Wrong Input Time!!");
        }else{
            count=true;
            turn=true;
            pressed=true;
			operated++;
            audioSource.PlayOneShot(startsound);
        }      
    }

    // Clear button
    public void InputClear(Text clear){
        count=false;
        turn=false;
        audioSource.PlayOneShot(cancelsound);
        Panel.text = "00:00";
    }
    
    // Open button
    public void InputOpen(Text open){
        Vector3 originalposition = Door.transform.position;
        Quaternion originalrotation = Door.transform.rotation;
        Door.GetComponent<DoorClose>().ActualStatus(originalposition, originalrotation);

        audioSource.PlayOneShot(opensound);
        d_open = true;
        turn=false;
    }

    // Close button
    public void InputClose(){
        d_close = false;
        Door.GetComponent<DoorClose>().CloseDoor(d_close);
    }
}
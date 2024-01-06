using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject obj_s;
    Number script;
    bool pressed;
    int action;
    // Start is called before the first frame update
    void Start()
    {
        //obj_s=GameObject.Find("Canvas");
        script = obj_s.GetComponent<Number>(); 
        action = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pressed = script.pressed;
        if(pressed){
            if(action >= 200){
                script.pressed=false;
            }else if(action >=100){
                //Debug.Log("Start");
                this.transform.Translate(0f,0f,-0.03f* Time.deltaTime);
                action += 1;
            }else{
            this.transform.Translate(0f,0f,0.03f* Time.deltaTime);
            action += 1;
            //Debug.Log("pressed");
            }
        }
    }
}

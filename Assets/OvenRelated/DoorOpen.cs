using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private float speed = 150f;
    public GameObject obj_s;
    private Number script;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        //obj_s=GameObject.Find("Canvas");
        script = obj_s.GetComponent<Number>();
    }
    void Update()
    {   
        open = script.d_open;
		bool count = script.count;
		
		if(!count){
			int Kakudo = (int)transform.localEulerAngles.y;
			if(open == true && Kakudo<91){
				float step = speed * Time.deltaTime;
				
				transform.localRotation  = Quaternion.RotateTowards(transform.localRotation , Quaternion.Euler(0, 90f, 0), step);
			}
		}
    }
}

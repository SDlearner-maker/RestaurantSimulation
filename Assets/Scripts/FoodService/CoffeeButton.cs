using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeButton : MonoBehaviour
{
    private float countDown = 0.2f;
    private bool pressed = false;
	
	public GameObject drinkGameManager;
	
	[SerializeField]
	private int drinkType = 0;
	
	private int pushPull = 0;
	
	private Vector3 buttonPos;
	
    void Start()
	{
		buttonPos = transform.localPosition;
    }

    void Update()
	{
		//Debug.Log("posx : "+ transform.localPosition.x);
        if(pressed == true){
            if (pushPull == 0){
                transform.localPosition += Vector3.right * 0.25f * Time.deltaTime;
				if (transform.localPosition.x > -0.465f){
					pushPull = 1;
				}
            }
            else if (pushPull == 1){
                transform.localPosition += Vector3.left * 0.25f * Time.deltaTime;
				if (transform.localPosition.x < -0.495f){
					pushPull = 0;
					pressed = false;
					drinkGameManager.SendMessage("ButtonPressed", drinkType);
				}
            }
        }
		

    }
	
	public void OnMouseDown(){
        pressed = true;
		Debug.Log("ButtonPressed");
    }
}

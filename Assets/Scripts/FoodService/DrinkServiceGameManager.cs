using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DrinkServiceGameManager : MonoBehaviour
{
	public Text instructionkitchen;
	// Customer Object
	public GameObject customer;
	public GameObject makefoodbutton;
	public GameObject instructionfoodbox;
	public GameObject instructiondrinkbox;
	public Text instructionfood;
	public GameObject doneallbutton;

	// Customer Order
	private string orderdrink;

	[HideInInspector]
	public bool onpaddone=false;
	[HideInInspector]
	public bool drinkpoured=false; 
	
	// Ray
	private Ray ray;
    private RaycastHit hitAll;
	
	// GameObject
	public GameObject coffeePad;
	public GameObject cokePad;
	public GameObject milkPad;
	
	public GameObject cupPrefab;
	
	// Status, Can reset by resetStatus() function
	private bool isDrinkCorrect = false;
	private GameObject currentDrinkObject;
	private string english;
	private string cantonese;
	
	private bool isDragging = false;
	private bool isFinished = false;
	
	private int servedIndex = 0;
	[HideInInspector]
	public int qty = 0;
	private int doneserve = 0;

	[HideInInspector]
	public bool placedontray=false;

	[HideInInspector]
	public bool done = false;

	// Start is called before the first frame update
	void Start()
    {
		//orderdrink = "Coffee";
		//if (orderdrink == "Ice tea") { orderdrink = "Milk"; }
		makefoodbutton.SetActive(false);
		instructiondrinkbox.SetActive(false);

		//instructionfoodbox.SetActive(false);
		//customer.GetComponent<GotoKitchens>().instructionkitchenbox.SetActive(false);
	}

	public int ReturnServed()
	{
		return doneserve;
	}
	public void DoneOrNot()
    {
		if (servedIndex < qty)
		{
            if (orderdrink == "Milk")
            {
				english = "Now repeat the process " + (qty - servedIndex).ToString() + " more times. Therefore, place a cup on the " + "Ice Tea" + " pad again.";
				cantonese = "現在重複該過程多" + (qty - servedIndex).ToString() + "次。再次將杯子放在 " + "Ice Tea" + "墊子上";
			}
            else
            {
				english = "Now repeat the process " + (qty - servedIndex).ToString() + " more times. Therefore, place a cup on the " + orderdrink + " pad again.";
				cantonese = "現在重複該過程多" + (qty - servedIndex).ToString() + "次。再次將杯子放在 " + orderdrink + "墊子上";

			}
			
			instructionkitchen.text = english + "\n" + cantonese;
			Debug.Log(instructionkitchen.text);
		}
		else if (servedIndex == qty)
		{
			string food= customer.GetComponent<GotoKitchens>().foodorder.text;
			if (food == "None")
			{
				english = "Great job! You have finished all the orders. Now click on 'Done' button to go to counter.";
				cantonese= "很棒！你已完成了所有訂單。現在點擊 'Done' 鍵去櫃檯。";
				instructionkitchen.text = english + "\n" + cantonese;

				doneallbutton.SetActive(true);

				Debug.Log(instructionkitchen.text);
			}
            else
            {
				english = "Great job! You have finished all the drink orders. Now click on 'Make Food' button to make sandwich.";
				cantonese= "很棒！你已完成了所有飲品的訂單。現在點擊 'Make Food' 鍵然後開始製作三明治。";
				instructionkitchen.text = english + "\n" + cantonese;
				Debug.Log(instructionkitchen.text);
				makefoodbutton.SetActive(true);
			}
		}
	}

	public void MakeFoodNow()
    {
		instructionfoodbox.SetActive(true);
		makefoodbutton.SetActive(false);
		instructiondrinkbox.SetActive(false);

		english = "Place the Bread from the bread box on the cutting board.";
		cantonese= "現在從面包盒取出面包，然後放上菜板上。";
		instructionfood.text = english + "\n" + cantonese;
		
    }

	public void MakeAnything(string sceneanother)
	{
		SceneManager.LoadScene(sceneanother); // call RandomSandwich scene
	}
	public void ReadytoMakeDrink()
	{		
		string odrink = customer.GetComponent<GotoKitchens>().drinkorder.text; //gets the drink order from customer

		if (odrink != "None") {
			orderdrink = odrink;
			qty = Int32.Parse(customer.GetComponent<GotoKitchens>().drinkqty.text);
			if (orderdrink == "Ice tea") { orderdrink = "Milk"; }
			instructiondrinkbox.SetActive(true);
			
            if (orderdrink == "Milk")
            {
				english = "Place cup on the " + "Ice Tea" + " pad.";
				cantonese = "將杯子放在" + "Ice Tea" + " 墊上";
			}
            else
            {
				english = "Place cup on the " + orderdrink + " pad.";
				cantonese = "將杯子放在" + orderdrink + " 墊上";
			}			
			instructionkitchen.text = english + "\n" + cantonese;
		}
		//if (orderdrink == "Ice tea") { orderdrink = "Milk"; }
		//comment out if want to test only food scene
	}

	void Update()
    {
		if (servedIndex == qty) { done = true; } else { done = false; }
		//orderdrink = "Coffee";
		//orderdrink= customer.GetComponent<GotoKitchens>().drinkorder.text;//gets the drink order from customer
		//comment out if want to test only food scene

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hitAll))
		{
			// If user hold the left mouse button
			if (Input.GetMouseButton(0))
			{
				// If user hold the left mouse button and the mouse position is Bread Food Box, then create a bread prefab which will follow the user mouse until user release the button.
				if (hitAll.collider.gameObject.tag == "Cup" && isDragging == false)
				{
					currentDrinkObject = Instantiate(cupPrefab, hitAll.point, Quaternion.identity);
					GameObject.Find("FoodServiceGameManager").SendMessage("setIsDragging", true);
					isDragging = true;
				}
				
				if ((string.Equals(hitAll.collider.gameObject.name, "Coke") || string.Equals(hitAll.collider.gameObject.name, "Coffee") || string.Equals(hitAll.collider.gameObject.name, "Milk")) && isDragging == false)
				{
					currentDrinkObject = hitAll.collider.gameObject;
					GameObject.Find("FoodServiceGameManager").SendMessage("setIsDragging", true);
					isDragging = true;
				}
				
				if (isDragging == true && currentDrinkObject != null)
				{
					currentDrinkObject.transform.position = hitAll.point;
					currentDrinkObject.layer = 2;
				}
			}
			else
			{
				isDragging = false;
				GameObject.Find("FoodServiceGameManager").SendMessage("setIsDragging", false);
				// Only avaliable for the ham, egg, cheese, bread object.
				// When user release button, if the object is on the cutting board, then object position is the cutting board.
				// If there is other object on the cutting board already, then place on top of that object.
				if (hitAll.collider.gameObject.tag == "CoffeePad" && currentDrinkObject != null && hitAll.collider.gameObject.transform.childCount == 0 && CheckDrinkOrder("Coffee", orderdrink) && servedIndex < qty)
				{
					currentDrinkObject.transform.position = hitAll.collider.gameObject.transform.position;
					currentDrinkObject.name = "Coffee";
					currentDrinkObject.transform.SetParent(hitAll.collider.gameObject.transform);
					currentDrinkObject.layer = 0;

					onpaddone = true;
					english ="Press the button of " + currentDrinkObject.name + ".";
					cantonese= "按下" + currentDrinkObject.name + " 的按鈕裝滿杯子.";
					instructionkitchen.text = english + "\n" + cantonese;
					Debug.Log(instructionkitchen.text);
					//Debug.Log("Drinkname "+ currentDrinkObject.name+" "+onpaddone);
				}
				else if (hitAll.collider.gameObject.tag == "CokePad" && currentDrinkObject != null && hitAll.collider.gameObject.transform.childCount == 0 && CheckDrinkOrder("Coke", orderdrink) && servedIndex < qty)
				{
					currentDrinkObject.transform.position = hitAll.collider.gameObject.transform.position;
					currentDrinkObject.name = "Coke";
					currentDrinkObject.transform.SetParent(hitAll.collider.gameObject.transform);
					currentDrinkObject.layer = 0;

					onpaddone = true;
					english = "Press the button of " + currentDrinkObject.name + ".";
					cantonese = "按下" + currentDrinkObject.name + " 的按鈕裝滿杯子.";
					instructionkitchen.text = english + "\n" + cantonese;
					Debug.Log(instructionkitchen.text);
					//Debug.Log("Drinkname "+ currentDrinkObject.name+" "+onpaddone);
				}
				else if (hitAll.collider.gameObject.tag == "MilkPad" && currentDrinkObject != null && hitAll.collider.gameObject.transform.childCount == 0 && CheckDrinkOrder("Milk", orderdrink) && servedIndex<qty)
				{
					currentDrinkObject.transform.position = hitAll.collider.gameObject.transform.position;
					currentDrinkObject.name = "Milk";
					currentDrinkObject.transform.SetParent(hitAll.collider.gameObject.transform);
					currentDrinkObject.layer = 0;

					onpaddone = true;
					english = "Press the button of " + "Ice Tea" + ".";
					cantonese = "按下" + "Ice Tea" + " 的按鈕裝滿杯子.";
					instructionkitchen.text = english + "\n" + cantonese;
					Debug.Log(instructionkitchen.text);
					//Debug.Log("Drinkname "+ currentDrinkObject.name+" "+onpaddone);
				}
				else if (hitAll.collider.gameObject.tag == "Tray" && isFinished == true && currentDrinkObject != null)
				{
					currentDrinkObject.transform.position = hitAll.collider.gameObject.transform.GetChild(0).gameObject.transform.position + new Vector3((0.3f*80)-(0.15f*80*servedIndex), 0, 0.25f*80);
					currentDrinkObject.transform.SetParent(null);

					placedontray = true;
					doneserve += 1;
					DoneOrNot();
					resetStatus();
				}

				else
				{
					if (currentDrinkObject != null)
					{
						
						if (string.Equals(currentDrinkObject.name, "Coke"))
						{
							currentDrinkObject.transform.position = cokePad.transform.position;
							currentDrinkObject.transform.SetParent(cokePad.transform);
							currentDrinkObject.layer = 0;
						}
						else if (string.Equals(currentDrinkObject.name, "Coffee"))
						{
							currentDrinkObject.transform.position = coffeePad.transform.position;
							currentDrinkObject.transform.SetParent(coffeePad.transform);
							currentDrinkObject.layer = 0;
						}
						else if (string.Equals(currentDrinkObject.name, "Milk"))
						{
							currentDrinkObject.transform.position = milkPad.transform.position;
							currentDrinkObject.transform.SetParent(milkPad.transform);
							currentDrinkObject.layer = 0;
						}
						/*
						if (string.Equals(currentDrinkObject.name, "Coke") || string.Equals(currentDrinkObject.name, "Coffee") || string.Equals(currentDrinkObject.name, "Milk"))
						{
							currentDrinkObject.transform.position = GameObject.Find("CuttingBoard").gameObject.transform.GetChild(0).gameObject.transform.position;
							currentDrinkObject.transform.SetParent(null);
							currentDrinkObject.layer = 0;
						} */
						else
						{
							Destroy(currentDrinkObject);
						} 
					} 
				}
				
				currentDrinkObject = null;
			}
				
		}
    }
	
	public bool CheckDrinkOrder(string drink, string orderDrink)
	{
		bool isCorrect = false;
		
		if (string.Equals(drink, orderDrink))
		{
			isCorrect = true;
		}
		
		return isCorrect;
	}
	
	public void ButtonPressed(int drinkType)
	{
		switch(drinkType){			
			case 1:
				if(coffeePad.transform.childCount != 0)
				{
					if (coffeePad.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.localPosition.y == 0)
					{
						coffeePad.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.Translate(0, 0.244f*80, 0);
						isFinished = true;
						servedIndex += 1;

						drinkpoured = true;
						english = "Good! You have made the drink. Place the cup on the tray now.";
						cantonese= "非常好！ 你已經做好了一杯飲品。 現在將杯子放在托盤上.";
						instructionkitchen.text = english + "\n" + cantonese;
						Debug.Log(instructionkitchen.text);
						//Debug.Log("button pressed coffee"+" "+ drinkpoured);
					}
				}
				break;
			case 2:
				if(cokePad.transform.childCount != 0)
				{
					if (cokePad.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.localPosition.y == 0)
					{
						cokePad.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.Translate(0, 0.244f*80, 0);
						isFinished = true;
						servedIndex += 1;

						drinkpoured = true;
						english = "Good! You have made the drink. Place the cup on the tray now.";
						cantonese = "非常好！ 你已經做好了一杯飲品。 現在將杯子放在托盤上.";
						instructionkitchen.text = english + "\n" + cantonese;
						Debug.Log(instructionkitchen.text);
						//Debug.Log("button pressed coke" + " " + drinkpoured);
					}
				}
				break;
			case 3:				
				if(milkPad.transform.childCount != 0)
				{
					if (milkPad.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.localPosition.y == 0)
					{
						milkPad.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.Translate(0, 0.244f*80, 0);
						isFinished = true;
						servedIndex += 1;
						Debug.Log(servedIndex);

						drinkpoured = true;
						english = "Good! You have made the drink. Place the cup on the tray now.";
						cantonese = "非常好！ 你已經做好了一杯飲品。 現在將杯子放在托盤上.";
						instructionkitchen.text = english + "\n" + cantonese;
						Debug.Log(instructionkitchen.text);
						//Debug.Log("button pressed milk" + " " + drinkpoured);
					}
				}
				break;
			default:
				break;
		}
	}
	
	public void setIsDragging(bool isDrag)
	{
		isDragging = isDrag;
	}
	
	public void resetStatus()
	{		
		isDrinkCorrect = false;
		isDragging = false;
		isFinished = false;
	}
	
}

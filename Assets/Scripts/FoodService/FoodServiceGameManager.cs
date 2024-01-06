using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoodServiceGameManager : MonoBehaviour
{
	public Text instructionkitchen;
	// Customer Object
	public GameObject customer;
	public GameObject instructionfoodbox;
	public Text instructionfood;
	public GameObject doneallbutton;

	// Customer Order
	private string orderfood;
	
	// Ray
	private Ray ray;
    private RaycastHit hitAll;
	
	// GameObject
	public GameObject oven;
	
	public GameObject hamPrefab;
	public GameObject eggPrefab;
	public GameObject cheesePrefab;
	public GameObject breadPrefab;
	
	public GameObject hamFoodBoxCover;
	public GameObject eggFoodBoxCover;
	public GameObject cheeseFoodBoxCover;
	public GameObject breadFoodBoxCover;
	
	// The Sandwich Object
	public GameObject finishedSandwich;
	public GameObject servedSandwich;
	private bool isNeedBake = false;
	
	// Status, Can reset by resetStatus() function
	public float rotationSpeed = 5;
	
	private bool isDragging = false;
	private GameObject currentFoodObject;
	
	private bool isFinished = false;
		
	private int isDraggingName = 0;
	
	private int index = 0;
	private int servedIndex = 0;
	private bool isStepCorrect = false;
	private string english;
	private string cantonese;

	[HideInInspector]
	public int qty = 0;
	
	// Sandwich Data
	private int[] hamSandwichStep = new int[]{4, 3, 4};
	private int[] eggSandwichStep = new int[]{4, 2, 4};
	private int[] cheeseSandwichStep = new int[]{4, 1, 4};

	[HideInInspector]
	public bool done = false;

	// Start is called before the first frame update
	void Start()
    {
		instructionfoodbox.SetActive(false);
		// Pretend get order
		//orderfood = "Ham sandwich";	
		//resetStatus();
    }

	//method not in use
	public int ReturnServed()
	{
		return servedIndex;
	}

	public void DoneOrNot()
	{
		if (servedIndex < qty)
		{
			english = "Click on 'C' button to close oven. Now repeat the process " + (qty - servedIndex).ToString() + " more times. Therefore place a Bread again on the cutting board.";
			cantonese= "按下烤箱的 'C' 鍵來關閉。現在重複步驟多" + (qty - servedIndex).ToString() + "次。現在請將麵包再放在菜板上。";
			instructionfood.text = english + "\n" + cantonese;
			Debug.Log(instructionfood.text);
		}
		else if (servedIndex == qty)
		{
			english = "Great job! You have finished all the orders. Click on 'C' button to close oven. Then click on 'Done' button to go to counter.";
			cantonese= "恭喜！ 你已完成製作所有食物。 按 'C' 鍵來關閉烤箱。然後點擊 ' Done ' 按鈕去櫃檯。";
			instructionfood.text = english + "\n" + cantonese;

			doneallbutton.SetActive(true);

			Debug.Log(instructionfood.text);
		}
	}

	public void MakeAnything(string sceneanother)
	{
		SceneManager.LoadScene(sceneanother); // call RandomSandwich scene
	}
	public void ReadytoCook()
    {
		
		string ofood = customer.GetComponent<GotoKitchens>().foodorder.text; //gets the food order from customer
		
		string[] orderfoodfood = ofood.Split();
		//orderfood = "";

		if (orderfoodfood[0] != "None")
		{
			qty = Int32.Parse(customer.GetComponent<GotoKitchens>().foodqty.text);
			if (Array.Exists(orderfoodfood, x => x == "sandwich"))
			{
				int index = Array.IndexOf(orderfoodfood, "sandwich");
				orderfoodfood[index] = char.ToUpper(orderfoodfood[index][0]) + orderfoodfood[index].Substring(1);
			}

			orderfood = orderfoodfood[0] + " " + orderfoodfood[1];

			string drinkany= customer.GetComponent<GotoKitchens>().drinkorder.text;
			if (drinkany=="None")
            {
				//if no drink order then just activate food ialog box
				instructionfoodbox.SetActive(true);
				english = "Place the Bread from the bread box on the cutting board.";
				cantonese= "現在從面包盒取出面包，然後放上菜板上。";
				instructionfood.text = english + "\n" + cantonese;
			}
			
		} 

		//Debug.Log(orderfood);
	}
	// Update is called once per frame

	void Update()
    {		
        if (servedIndex == qty) { done = true; } else { done = false; }
		// If the sandwich is made with correct step, then allows to bake
		if (index == 3)
		{
			isNeedBake = true;

			//instructionfood.text = "Place the sandwich inside the oven.";
			Debug.Log("Keep sandwich inside the oven.");
		}
		
		//Debug.Log(orderfood);
		//Debug.Log(index);
		//Debug.Log(isStepCorrect);
		// finishedSandwich is the parent of all sandwich element.
		// If the sandwich is not yet prepared, then the sandwich is not able to be move.
		
		if (isNeedBake == false)
		{
			finishedSandwich.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			finishedSandwich.GetComponent<BoxCollider>().enabled = true;
		}
		
		// If the sandwich is prepared and be baked, then the sandwich is ready to go.
		if (oven.gameObject.transform.GetChild(3).GetComponent<Number>().operated > 0)
		{
			isFinished = true;

			english = "Count till 10 seconds and then press 'E' button. Then press 'O' button and place the sandwich on the tray.";
			cantonese= "等待 10 秒，然後按 'E' 鍵，然後按 'O' 鍵，把三明治放在托盤上。"; 
			instructionfood.text = english + "\n" + cantonese;
			Debug.Log("Wait for 10 seconds");
		}
		
		// Use ray to track user mouse.
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hitAll))
		{
			// If user hold the left mouse button
			if (Input.GetMouseButton(0))
			{
				//Debug.Log(hitAll.collider.gameObject.name);
				// If user hold the left mouse button and the mouse position is Bread Food Box, then create a bread prefab which will follow the user mouse until user release the button.
				if (hitAll.collider.gameObject.tag == "BreadFoodBox" && isDragging == false && index < 3 && servedIndex<qty)
				{
					currentFoodObject = Instantiate(breadPrefab, hitAll.point, Quaternion.identity);
					isDragging = true;
					isDraggingName = 4;
					isStepCorrect = CheckSandwichStep(index, isDraggingName, orderfood);
					
					GameObject.Find("DrinkServiceGameManager").SendMessage("setIsDragging", true);

					//Debug.Log("dragged bread");
				}
				
				// If user hold the left mouse button and the mouse position is Ham Food Box, then create a ham prefab which will follow the user mouse until user release the button.
				if (hitAll.collider.gameObject.tag == "HamFoodBox" && isDragging == false && index < 3 && servedIndex < qty)
				{
					currentFoodObject = Instantiate(hamPrefab, hitAll.point, Quaternion.identity);
					isDragging = true;
					isDraggingName = 3;
					isStepCorrect = CheckSandwichStep(index, isDraggingName, orderfood);
					
					GameObject.Find("DrinkServiceGameManager").SendMessage("setIsDragging", true);

					//Debug.Log("dragged ham");
				}
				
				// If user hold the left mouse button and the mouse position is Egg Food Box, then create a egg prefab which will follow the user mouse until user release the button.
				if (hitAll.collider.gameObject.tag == "EggFoodBox" && isDragging == false && index < 3 && servedIndex < qty)
				{
					currentFoodObject = Instantiate(eggPrefab, hitAll.point, Quaternion.identity);
					isDragging = true;
					isDraggingName = 2;
					isStepCorrect = CheckSandwichStep(index, isDraggingName, orderfood);
					
					GameObject.Find("DrinkServiceGameManager").SendMessage("setIsDragging", true);

					//Debug.Log("dragged egg");
				}
				
				// If user hold the left mouse button and the mouse position is Cheese Food Box, then create a cheese prefab which will follow the user mouse until user release the button.
				if (hitAll.collider.gameObject.tag == "CheeseFoodBox" && isDragging == false && index < 3 && servedIndex < qty)
				{
					currentFoodObject = Instantiate(cheesePrefab, hitAll.point, Quaternion.identity);
					isDragging = true;
					isDraggingName = 1;
					isStepCorrect = CheckSandwichStep(index, isDraggingName, orderfood);
					
					GameObject.Find("DrinkServiceGameManager").SendMessage("setIsDragging", true);

					//Debug.Log("dragged cheese");
				}
				
				// If user hold the left mouse button and the mouse position is the sandwich, then the whole sandwich will follow the user mouse until user release the button.
				if (hitAll.collider.gameObject.tag == "FinishedSandwich" && isDragging == false && index == 3)
				{
					currentFoodObject = finishedSandwich;
					isDragging = true;
					isDraggingName = 0;

					GameObject.Find("DrinkServiceGameManager").SendMessage("setIsDragging", true);

					//instructionfood.text = "Sandwich will be ready after 10 seconds, so count till 10.";
					Debug.Log("Sandwich will be done after 10 seconds");					
				}
				
				// If there is an object being dragged, set the position to be the mouse position.
				if (isDragging == true && currentFoodObject != null)
				{
					currentFoodObject.transform.position = hitAll.point;
					currentFoodObject.layer = 2;
				}
			}
			else
			{
				isDragging = false;
				GameObject.Find("DrinkServiceGameManager").SendMessage("setIsDragging", false);
				
				// Get Oven Door status.
				bool isDoorOpen = oven.gameObject.transform.GetChild(3).GetComponent<Number>().d_open;
								
				// Only avaliable for the ham, egg, cheese, bread object.
				// When user release button, if the object is on the cutting board, then object position is the cutting board.
				// If there is other object on the cutting board already, then place on top of that object.
				if (hitAll.collider.gameObject.tag == "CuttingBoard" && currentFoodObject != null && isStepCorrect == true && isDraggingName != 0)
				{
					index += 1;
					currentFoodObject.transform.position = hitAll.collider.gameObject.transform.GetChild(0).gameObject.transform.position + new Vector3(0, 0.03f * 80 *index, 0);
					currentFoodObject.transform.parent = finishedSandwich.transform;

					//if (currentFoodObject.name == "Bread(Clone)" && index==1)
					if(index==1)
					{
						english = "Good! Now place " + orderfood.Split()[0] + " on top of Bread.";
						cantonese = "好的！ 現在把" + orderfood.Split()[0] + "放在面包上。";
						instructionfood.text = english + "\n" + cantonese;
						Debug.Log("Item placed "+ currentFoodObject+" "+index);
					}
					else if(index == 2)
                    {
						english = "Place Bread on top.";
						cantonese = "把面包放上頂部。";
						instructionfood.text = english + "\n" + cantonese;
						Debug.Log("Item placed " + currentFoodObject + " " + index);
					}
					else if(index == 3)
					{
						english = "Now Press 'O' button on oven to open and place sandwich inside it.";
						cantonese= "現在按下烤箱上的 'O' 鍵打開並將三明治放入烤箱裏.";
						instructionfood.text = english + "\n" + cantonese;
						Debug.Log("Item placed " + currentFoodObject + " " + index);
					}
					//Debug.Log("Correct object placed is " + currentFoodObject);					
				}
				// Only avaliable for the whole Sandwich Object.
				// When user release button, if the sandwich is on the tray, then check the sandwich is baked or not.
				// If yes, sandwich is ready to serve; if no, sandwich need to be baked.
				else if (hitAll.collider.gameObject.tag == "Tray" && currentFoodObject != null && isNeedBake == true && isDraggingName == 0)
				{
					if (isFinished == true)
					{
						currentFoodObject.transform.position = hitAll.collider.gameObject.transform.GetChild(0).gameObject.transform.position + new Vector3(0, 0.09f * 80 * servedIndex, 0);
						servedIndex += 1;

						Debug.Log("Sandwich on tray");
						DoneOrNot();
						resetStatus();
					}
					else{
						currentFoodObject.transform.position = GameObject.Find("CuttingBoard").gameObject.transform.GetChild(0).gameObject.transform.position;
						currentFoodObject.layer = 0;
					}
				}
				// Only avaliable for the whole Sandwich Object.
				// When user release button, if the sandwich is on the Oven, place the sandwich inside the oven if the oven door is opened, else place back to the cutting board.
				else if (hitAll.collider.gameObject.tag == "Oven" && currentFoodObject != null && isNeedBake == true && isDraggingName == 0)
				{
					if (isDoorOpen == true)
					{
						currentFoodObject.transform.position = oven.gameObject.transform.GetChild(0).gameObject.transform.position;
						currentFoodObject.layer = 0;
						oven.gameObject.transform.GetChild(0).gameObject.layer = 2;

						english = "Sandwich is inside the oven, press 'C' button on the oven. Then press '1' and '0' and then 'S' button.";
						cantonese= "三明治現在在烤箱裡面了。按下烤箱的 'C' 鍵，然後按下 '1' 和 '0' 然後按下 'S' 鍵。";
						instructionfood.text = english + "\n" + cantonese;
						Debug.Log("Sandwich is in oven, now press 'C' button");
					}
					else
					{
						currentFoodObject.transform.position = GameObject.Find("CuttingBoard").gameObject.transform.GetChild(0).gameObject.transform.position;
						currentFoodObject.layer = 0;
						oven.gameObject.transform.GetChild(0).gameObject.layer = 0;
					}
					
				}
				// If the whole sandwich is drop on nothing, the sandwich will be placed back to the cutting board.
				// If the ham, egg, cheese, and bread drop on nothing, then delete object.
				else
				{
					if (isDraggingName == 0 && currentFoodObject != null){
						currentFoodObject.transform.position = GameObject.Find("CuttingBoard").gameObject.transform.GetChild(0).gameObject.transform.position;
						currentFoodObject.layer = 0;
						oven.gameObject.transform.GetChild(0).gameObject.layer = 0;
					}
					else
					{
						Destroy(currentFoodObject);
					}
				}
				
				currentFoodObject = null;
			}
		}
		
		// Food Box Cover Rotation
		if(isStepCorrect == false && isDraggingName != 0)
		{
			switch(isDraggingName){
				case 1:
					cheeseFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(cheeseFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
					break;
				case 2:
					eggFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(eggFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
					break;
				case 3:
					hamFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(hamFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
					break;
				case 4:
					breadFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(breadFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
					break;
				default:
					Debug.Log("Error Obj");
					break;
			}
			
		}
		else
		{	
			//if(isStepCorrect == true){
				cheeseFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(cheeseFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);
				eggFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(eggFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);
				hamFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(hamFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);
				breadFoodBoxCover.transform.localRotation  = Quaternion.RotateTowards(breadFoodBoxCover.transform.localRotation , Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);
			//}
		}
    }
	
	//this method is not in use
	public void Orders()
	{
		orderfood = customer.GetComponent<Billing>().OrderGiveFood();
	}
	
	// Check the sandwich is made with the correct step
	private bool CheckSandwichStep(int indexNum, int isDraggingNameNum, string orderfoodStr)
	{
		bool isTrue = false;
		if (string.Equals(orderfoodStr, "Ham Sandwich"))
		{
			if (isDraggingNameNum == hamSandwichStep[indexNum])
			{
				isTrue = true;
			}
		}
		
		if (string.Equals(orderfoodStr, "Egg Sandwich"))
		{
			if (isDraggingNameNum == eggSandwichStep[indexNum])
			{
				isTrue = true;
			}
		}
		
		if (string.Equals(orderfoodStr, "Cheese Sandwich"))
		{
			if (isDraggingNameNum == cheeseSandwichStep[indexNum])
			{
				isTrue = true;
			}
		}
		
		return isTrue;
	}
	
	// Reset the status.
	public void resetStatus()
	{
		oven.gameObject.transform.GetChild(0).gameObject.layer = 0;
		oven.gameObject.transform.GetChild(3).GetComponent<Number>().operated = 0;
	
		int childNum = finishedSandwich.gameObject.transform.childCount;
		if (finishedSandwich.gameObject.transform.childCount != 0){
			for (int i = 0; i < childNum; i++)
			{
				finishedSandwich.gameObject.transform.GetChild(0).gameObject.transform.parent = servedSandwich.transform;
			}
		}
		
		finishedSandwich.transform.position = GameObject.Find("CuttingBoard").gameObject.transform.GetChild(0).gameObject.transform.position;;
		finishedSandwich.gameObject.layer = 0;
		
		isNeedBake = false;
		isDragging = false;
		isFinished = false;
		isDraggingName = 0;
		index = 0;
		isStepCorrect = false;
	}
	
	public void setIsDragging(bool isDrag)
	{
		isDragging = isDrag;
	}
}

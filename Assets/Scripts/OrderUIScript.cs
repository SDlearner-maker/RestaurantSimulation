using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class OrderUIScript : MonoBehaviour
{
	public Text instructions;
	public Text calcinstructions;
	public GameObject customer;
	public GameObject makeOrderUI;
	public GameObject makeOrderButton;
	public Text makeOrderUI_ObjectText;
	public Text quantityText;

	private int[] selectedList = new int[1];
	private int currentSelectedNum = 0;

	private int[] quantityList = new int[1];
	private int currentQuantity = 0;
	private int customercash = 0;
	private string english;
	private string cantonese;

	// QuantityUI Object related
	public GameObject quantityUI;
	public GameObject OrderListUI;
	public Text quantityUI_ObjectText;
	public Text quantityTestDisplayText;
	public Text quantityRightWrongText;
	public Text fooditemordered;
	public Text fooditemqty;
	public Text fooditemprice;
	public Text fooditemmultipliedprice;
	public Text drinkitemordered;
	public Text drinkitemqty;
	public Text drinkitemprice;
	public Text drinkitemmultipliedprice;
	public Text totalcalc;
	public Text totalbilldisplayedonchangeUI;
	//public Text grandtotalled;

	private int[] quantityTextDisplayList = new int[1];
	private int currentQuantityTextDisplay = 0;
	
	// CheckOutUI Object related
	public GameObject checkOutUI;
	public GameObject hundrednote;
	///public GameObject CheckOutPrefab;

	private GameObject[] checkOutItemList;
	public Sprite[] spritesfooddrink;
	public Image foodimage;
	public Image drinkimage;
	Dictionary<string, int> orderslist = new Dictionary<string, int>();

	public Text checkOutTestDisplayText;
	public Text checkOutRightWrongText;
	
	private int totalPrice = 0;
	private int currentChange = 0;
	private int currentdisplayedchange=0;

	// ChangeUI Object related
	public GameObject changeUI;
	
	public Text changeTestDisplayText;
	public Text changeRightWrongText;
	public GameObject takecashcustomer;

	// Common Global Var
	private int[] tempList;
	private string orderdrink;
	private string orderfood;

	private string drinks;
	private string foods;

	private int quantitydrink;
	private int quantityfood;

	// Start is called before the first frame update
	void Start()
	{
		makeOrderUI_ObjectText.text = "Selected: ";
		quantityUI_ObjectText.text = "Quantity: ";

		english = "Hello welcome to game! Greet your first customer, click on 'Greet Now' button.";
		cantonese = "你好！歡迎你加入我們的團隊！有客人進店，點擊 'Greet Now' 鍵來和你的第一位客人打招呼啦！";
		instructions.text = english + "\n" + cantonese;

		makeOrderUI.SetActive(false);
		makeOrderButton.SetActive(false);
		takecashcustomer.SetActive(false);		
		quantityUI.SetActive(false);		
		checkOutUI.SetActive(false);		
		changeUI.SetActive(false);
		OrderListUI.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{		

		if (makeOrderUI.activeSelf == true)
		{
			if (selectedList[0] != 0)
			{
				ShowTextSelectedMenu(quantityList, quantityText);
			}
		}

		if (quantityUI.activeSelf == true)
		{
			ShowTextSelected(quantityTextDisplayList, quantityTestDisplayText);
		}
		
		if (checkOutUI.activeSelf == true)
		{
			ShowTextSelected(quantityTextDisplayList, checkOutTestDisplayText);
		}
		
		if (changeUI.activeSelf == true)
		{
			changeTestDisplayText.text = (customercash-currentChange).ToString(); //got to change this
			//changeTestDisplayText.text = currentdisplayedchange.ToString();
		}
	}

	// Greeting and Order
	public void GreetingAndOrder()
	{
		makeOrderButton.SetActive(true);
		english = "Click on 'Make Bill' button to go to computer screen and fill in orders.";
		cantonese = "點擊 'Make Bill' 鍵，進入電腦畫面，為客人點餐。"; 
		instructions.text = english + "\n" + cantonese;
	}

	// Tool Function =============================================================================================================================
	// Resize the Array and Transform the contents //empty array in the start, where size is 0, if want to add, then expand te size
	//created bigger array to replace empty array. if array.contains something, increase size as per number of inputs. 
	//array of newer length
	public int[] Resize(int arrayLength, int[] integerArray)
	{
		if (integerArray.Length != 0)
		{
			tempList = integerArray;
		}

		integerArray = new int[arrayLength];

		if (integerArray.Length != 0)
		{
			for (int i = 0; i < tempList.Length; i++)
			{
				integerArray[i] = tempList[i];

				if (i + 1 > integerArray.Length - 1)
				{
					break;
				}
			}
		}

		return integerArray;
	}

	// Update the selected object, use string to output on the quantityUI 
	public void ShowTextSelected(int[] integerArray, Text textUI)
	{
		string str = "";
		foreach (int i in integerArray)
		{
			str = str + i.ToString();
		}
        
		textUI.text = str;
	}

	// Update the selected object, use string to output on the menuUI
	public void ShowTextSelectedMenu(int[] integerArray, Text textUI)
	{
		string str = "Quantity:";
		foreach (int i in integerArray)
		{
			str = str + "\n" + i.ToString();
		}

		textUI.text = str;
	}

	public int ArrayToInt(int[] integerArray)
	{
		int num = 0;
		for (int i = integerArray.Length; i > 0; i--)
		{
			num += integerArray[i - 1] * (int)Mathf.Pow(10, integerArray.Length - i);
		}

		return num;
	}
	
	public void Orders()
	{
		quantitydrink = 0; quantityfood = 0;
		drinks = ""; foods = "";

		drinks = customer.GetComponent<Billing>().OrderGiveDrink();
		foods = customer.GetComponent<Billing>().OrderGiveFood();

		quantitydrink += Int32.Parse(drinks.Substring(drinks.IndexOf('/') + 1));
		orderdrink = drinks.Substring(0, drinks.IndexOf('/'));
        if (orderdrink != "") { orderslist.Add(orderdrink, quantitydrink); }

		quantityfood += Int32.Parse(foods.Substring(foods.IndexOf('/') + 1));
		orderfood = foods.Substring(0, foods.IndexOf('/'));
        if (orderfood != "") { orderslist.Add(orderfood, quantityfood); }

		customer.GetComponent<GotoKitchens>().ListOrders(orderfood, orderdrink, quantityfood, quantitydrink);
	}

	// End Tool Function =========================================================================================================================

	// UI Related Function =======================================================================================================================
	// Show OrderUI
	public void MakeOrder()
	{
		Orders();
		makeOrderUI.SetActive(true);
		quantityUI.SetActive(false);
		makeOrderButton.SetActive(false);
		english = "Click on the items that customer has ordered.";
		cantonese = "按下客人所選擇的食物。"; 
		calcinstructions.text = english + "\n"+cantonese;
	}
	
	// Show QuantityUI and Get the Latest Selected Object
	public void Quantity(int index, string name)
	{
		makeOrderUI.SetActive(false);
		quantityUI.SetActive(true);
		quantityUI_ObjectText.text = name; 		
	}
	
	public void EnterQuantity()
	{
		string qtydrink = quantitydrink.ToString();
		string qtyfood = quantityfood.ToString();
		string qty = "";

        if (quantityUI_ObjectText.text == orderdrink) { qty = qtydrink; }
		else if (quantityUI_ObjectText.text == orderfood) { qty = qtyfood; }

		if (quantityTestDisplayText.text != "0" && (quantityTestDisplayText.text== qty))
		{
			currentQuantity += 1;
			quantityList = Resize(currentQuantity, quantityList);
			quantityList[currentQuantity - 1] = ArrayToInt(quantityTextDisplayList);
			makeOrderUI_ObjectText.text = makeOrderUI_ObjectText.text + "\n" + quantityUI_ObjectText.text; 

			quantityTextDisplayList = new int[1];
			currentQuantityTextDisplay = 0;

			makeOrderUI.SetActive(true);
			quantityUI.SetActive(false);

            if (orderslist.Count == 0) { 
				english = "Great! All orders have been filled, now press the 'Finish Order' button to make bill.";
				cantonese = "做得好！！輸入了全部訂單就點擊 'Finish Order' 鍵，移動至結帳環節。"; 
				calcinstructions.text = english + "\n" + cantonese;
			}
            else
            {
				english = "Good work, now finish the rest of the orders too, add oil!";
				cantonese = "做得很漂亮！現在我們來完成餘下的訂單！"; 
				calcinstructions.text= english + "\n" + cantonese;
			}
		}
        else
		{
			Debug.Log("Enter value>0, entered value: "+ quantityTestDisplayText.text+ " correct value: "+qty);
			quantityRightWrongText.text = "Try Again";
			english = "The correct quantity is: " + qty.ToString()+" Press delete (<-) button to clear and then enter the correct value.";
			cantonese = "這次客人需要的數量是：" + qty.ToString() + "。" + "點擊 (<-) 鍵來取消，然後重新輸入正確數量。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void QuantityBack()
	{
		if (currentSelectedNum > 1)
		{
			currentSelectedNum -= 1;
			selectedList = Resize (currentSelectedNum, selectedList);
		} else {
			currentSelectedNum = 0;
			selectedList = Resize (1, selectedList);
			selectedList[0] = 0;
		}
		
		quantityTextDisplayList = new int[1];
		currentQuantityTextDisplay = 0;
		
		makeOrderUI.SetActive(true);
		quantityUI.SetActive(false);
	}

	public void CheckOut()
	{
		if (currentSelectedNum != 0){

			if (orderslist.Count == 0)
			{
				english = "Add the prices to get the total amount in the bill and press 'Enter' button.";
				cantonese = "輸入價錢，以計算總額然後點擊 'Enter' 鍵。";
				calcinstructions.text = english + "\n" + cantonese;

				checkOutUI.SetActive(true);
				makeOrderUI.SetActive(false);
				quantityUI.SetActive(false);

				Debug.Log("All orders done");

				fooditemordered.text = orderfood;
				fooditemqty.text = quantityfood.ToString();

				drinkitemordered.text = orderdrink;
				drinkitemqty.text = quantitydrink.ToString();

				int foodpriceofitem = 0; int drinkpriceofitem = 0; int imageindexfood = 0; int imageindexdrink = 0;
				
				switch (orderfood)
				{
					case "Cheese sandwich":
						foodpriceofitem = 20;
						imageindexfood = 0;
						break;
					case "Egg sandwich":
						foodpriceofitem = 10;
						imageindexfood = 1;
						break;
					case "Ham sandwich":
						foodpriceofitem = 30;
						imageindexfood = 2;
						break;
					case "":
						imageindexfood = 6;
						break;
				}

				foodimage.sprite = spritesfooddrink[imageindexfood];
				fooditemprice.text = "$" + (foodpriceofitem).ToString();
				fooditemmultipliedprice.text = "$" + (foodpriceofitem).ToString() + "*" + (quantityfood).ToString() + "=" + (foodpriceofitem * quantityfood).ToString();
			
				
				switch (orderdrink)
				{
					case "Coke":
						drinkpriceofitem = 15;
						imageindexdrink = 3;
						break;
					case "Ice tea":
						drinkpriceofitem = 5;
						imageindexdrink = 4;
						break;
					case "Coffee":
						drinkpriceofitem = 10;
						imageindexdrink = 5;
						break;
					case "":
						imageindexdrink = 6;
						break;
				}
								
				drinkimage.sprite = spritesfooddrink[imageindexdrink];
				drinkitemprice.text = "$" + (drinkpriceofitem).ToString();
				drinkitemmultipliedprice.text = "$" + (drinkpriceofitem).ToString()+"*"+ (quantitydrink).ToString()+"="+(drinkpriceofitem * quantitydrink).ToString();

				totalPrice = (foodpriceofitem * quantityfood) + (drinkpriceofitem * quantitydrink);

				totalcalc.text= "Sum total= " + "$" + (foodpriceofitem * quantityfood).ToString() + "+" + "$"+ (drinkpriceofitem * quantitydrink).ToString();
				//grandtotalled.text = (totalPrice).ToString();

			}
			
			else { 
				Debug.Log("Fill the orders first");
				english = "Some orders are left, please finish all the orders.";
				cantonese = "現在還有些訂單未曾完成，請繼續進行工作。";
				calcinstructions.text = english + "\n" + cantonese;
			}
		}
	}
	
	public void EnterCheckOut()
	{
		int userInput = ArrayToInt(quantityTextDisplayList);

		quantityTextDisplayList = new int[1];
		currentQuantityTextDisplay = 0;
		
		if (userInput == totalPrice)
		{
			Debug.Log("Correct");
			//checkOutRightWrongText.text = "Correct";

			int min = 1;
			int max = 4;
			Random random = new Random();
			int number = 0;
			while (!((number * 100) >= totalPrice))
			{
				number = random.Next(min, max);
			}
			
			Debug.Log("Random number: " + number);

			/*
			int x = 200, y = 100, z = -20;
			for (int i = 0; i < number; i++)
			{
				GameObject note = Instantiate(hundrednote, new Vector3(x * (i + 1), y, z), Quaternion.identity) as GameObject;
			}*/
			customercash = 100 * number;
			Debug.Log("Customer paid: " + customercash);

			takecashcustomer.SetActive(true);
			english = "Good work! Now take the cash from customer by clicking on 'Take Cash' button.";
			cantonese = "幹得好！現在點擊 'Take Cash' 鍵以從客人接收現金。";
			calcinstructions.text = english + "\n" + cantonese;

			//checkOutUI.SetActive(false);
			//Change(); //calls the change UI
		}
		else 
		{
			Debug.Log("Wrong");
			//checkOutRightWrongText.text = "Try Again";
			english = "Correct answer is something else. Try again by pressing the delete (<-) button and enter the correct answer...";
			cantonese = "輸入的金額好像不正確。點擊 (<-) 鍵後，請重新輸入正確答案。"; 
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void TakeCash()
    {
		makeOrderUI.SetActive(false);
		quantityUI.SetActive(false);
		checkOutUI.SetActive(false);

		GameObject[] hundreds = GameObject.FindGameObjectsWithTag("hundrednote");

		foreach(var n in hundreds) { Destroy(n); }
		//Destroy(GameObject.Find("hundred(Clone)"));

		Change();
		totalbilldisplayedonchangeUI.text = totalbilldisplayedonchangeUI.text + totalPrice.ToString();
		takecashcustomer.SetActive(false);

		english = "Click on the currency values to return that amount to the customer so that you will have only " + totalPrice.ToString() + ".";
		cantonese = "這次總額為" + totalPrice.ToString() + "。" + "我們現在點擊港幣圖案，把找續的零錢返回給客人。";
		calcinstructions.text = english + "\n" + cantonese;
	}
	public void Change()
	{		
		changeUI.SetActive(true);
	}
	public void EnterChange()
	{
		if ((customercash-currentChange) == totalPrice)
		{
			Debug.Log("Correct");
			changeRightWrongText.text = "Correct";
			customer.GetComponent<MoveCam>().MoveCamtoOriginal();
			OrderListUI.SetActive(true);
			changeUI.SetActive(false);
			
			english = "Great progress! Now time to cook, click on 'Go to Kitchen' button to go to the kitchen area to prepare orders.";
			cantonese = "厲害！ 現在是時候為客人製作套餐了，點擊 'Go To Kitchen' 鍵進入廚房區準備訂單。"; 
			instructions.text= english + "\n" + cantonese;
		}
        else
        {
			Debug.Log("Wrong");
			//changeRightWrongText.text = "Try Again";
			english = "You can only have $" + totalPrice.ToString() + " exactly with you.";
			cantonese = "這次你只可以收取 $ " + totalPrice.ToString() + "。"; 
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void RetryChange()
	{
		currentChange = 0;
	}
	// End UI Related Function ===================================================================================================================

	
	// Button Clicked Function ===================================================================================================================
	// Egg sandwich = 1, Cheese sandwich = 2, Ham sandwich = 3, Ice Tea = 4, Coffee = 5, Coke = 6
	// When egg was selected
	public void SelectEgg()
	{
		int index = 1;
		string name = "Egg sandwich";
		
		if (orderfood == name && orderslist.ContainsKey(name))
		{			
			Quantity(index, name);

			currentSelectedNum += 1; //how many objects at start no. of obj=0, then ++1
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 1; //list: do -1, else out of index

			orderslist.Remove(name);

			english = "Good! Now enter the quantity of the selected item and press 'Enter' button.";
			cantonese = "好！現在輸入客人所需的食物數量，然後點擊 'Enter' 鍵"; 
			calcinstructions.text = english + "\n" + cantonese;
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderfood);
			if (orderfood != "")
			{
				english = "Food item ordered is: " + orderfood + ". Click on the correct food item.";
				cantonese = "這次客人所選的食品是: " + orderfood + "。" + "點擊正確食品來完成訂單。"; 
				calcinstructions.text = english + "\n" + cantonese;
			}
			else
			{
				english = "There is only drink item ordered...";
				cantonese = "只訂購了飲品…";
				calcinstructions.text = english + "\n" + cantonese;
			}

		}
	}

	// When Sandwich was selected
	public void SelectCheese()
	{
		int index = 2;
		string name = "Cheese sandwich";

		if (orderfood == name && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 2;

			orderslist.Remove(name);

			english = "Good! Now enter the quantity of the selected item and press 'Enter' button.";
			cantonese = "好！現在輸入客人所需的食物數量，然後點擊 'Enter' 鍵";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderfood);
			if (orderfood != "")
			{
				english = "Food item ordered is: " + orderfood + ". Click on the correct food item.";
				cantonese = "這次客人所選的食品是: " + orderfood + "。" + "點擊正確食品來完成訂單。";
				calcinstructions.text = english + "\n" + cantonese;
			}
			else
			{
				english = "There is only drink item ordered...";
				cantonese = "只訂購了飲品…";
				calcinstructions.text = english + "\n" + cantonese;
			}

		}
	}

	// When Ham sandwich was selected
	public void SelectHam()
	{
		int index = 3;
		string name = "Ham sandwich";

		if (orderfood == name && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 3;

			orderslist.Remove(name);

			english = "Good! Now enter the quantity of the selected item and press 'Enter' button.";
			cantonese = "好！現在輸入客人所需的食物數量，然後點擊 'Enter' 鍵";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderfood);
			if (orderfood != "")
			{
				english = "Food item ordered is: " + orderfood + ". Click on the correct food item.";
				cantonese = "這次客人所選的食品是: " + orderfood + "。" + "點擊正確食品來完成訂單。";
				calcinstructions.text = english + "\n" + cantonese;
			}
			else
			{
				english = "There is only drink item ordered...";
				cantonese = "只訂購了飲品…";
				calcinstructions.text = english + "\n" + cantonese;
			}

		}
	}

	// When IceTea was selected
	public void SelectIceTea()
	{
		int index = 4;
		string name = "Ice tea";
		
		if (name == orderdrink && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			Debug.Log("Correct");
			
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 4;

			orderslist.Remove(name);

			english = "Good! Now enter the quantity of the selected item and press 'Enter' button.";
			cantonese = "好！現在輸入客人所需的食物數量，然後點擊 'Enter' 鍵"; 
			calcinstructions.text = english + "\n" + cantonese;
		}

		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderdrink);
			if (orderdrink != "")
			{
				english = "Drink item ordered is: " + orderdrink + ".";
				cantonese = "這次客人所選擇的飲品是: " + orderdrink + "。";
				calcinstructions.text = english + "\n" + cantonese;
			}
			else
			{
				english = "There is only food item ordered...";
				cantonese = "只訂購了食品……";
				calcinstructions.text = english + "\n" + cantonese;
			}
		}
	}

	// When Coffee was selected
	public void SelectCoffee()
	{
		int index = 5;
		string name = "Coffee";
		
		if (name == orderdrink && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			Debug.Log("Correct");
			
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 5;

			orderslist.Remove(name);

			english = "Good! Now enter the quantity of the selected item and press 'Enter' button.";
			cantonese = "好！現在輸入客人所需的食物數量，然後點擊 'Enter' 鍵";
			calcinstructions.text = english + "\n" + cantonese;
		}

		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderdrink);
			if (orderdrink != "")
			{
				english = "Drink item ordered is: " + orderdrink + ".";
				cantonese = "這次客人所選擇的飲品是: " + orderdrink + "。";
				calcinstructions.text = english + "\n" + cantonese;
			}
			else
			{
				english = "There is only food item ordered...";
				cantonese = "只訂購了食品……";
				calcinstructions.text = english + "\n" + cantonese;
			}
		}

	}

	// When Milk was selected
	public void SelectCoke()
	{
		int index = 6;
		string name = "Coke";
		
		if (name == orderdrink && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			Debug.Log("Correct");
			
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 6;

			orderslist.Remove(name);

			english = "Good! Now enter the quantity of the selected item and press 'Enter' button.";
			cantonese = "好！現在輸入客人所需的食物數量，然後點擊 'Enter' 鍵";
			calcinstructions.text = english + "\n" + cantonese;
		}

		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderdrink);
			if (orderdrink != "")
			{
				english = "Drink item ordered is: " + orderdrink + ".";
				cantonese = "這次客人所選擇的飲品是: " + orderdrink + "。";
				calcinstructions.text = english + "\n" + cantonese;
			}
			else
			{
				english = "There is only food item ordered...";
				cantonese = "只訂購了食品……";
				calcinstructions.text = english + "\n" + cantonese;
			}
		}

	}

	// When 9 was selected
	public void Num_9()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 9;
	}

	// When 8 was selected
	public void Num_8()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 8;
	}

	// When 7 was selected
	public void Num_7()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 7;
	}

	// When 6 was selected
	public void Num_6()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 6;
	}

	// When 5 was selected
	public void Num_5()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 5;
	}

	// When 4 was selected
	public void Num_4()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 4;
	}

	// When 3 was selected
	public void Num_3()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 3;
	}

	// When 2 was selected
	public void Num_2()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 2;
	}

	// When 1 was selected
	public void Num_1()
	{
		currentQuantityTextDisplay += 1;
		quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		quantityTextDisplayList[currentQuantityTextDisplay - 1] = 1;
	}

	// When 0 was selected
	public void Num_0()
	{
		if (quantityTextDisplayList[0] != 0)
		{
			currentQuantityTextDisplay += 1;
			quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
			quantityTextDisplayList[currentQuantityTextDisplay - 1] = 0;
		}
	}

	public void DeleteNum()
	{
		if (currentQuantityTextDisplay > 1)
		{
			currentQuantityTextDisplay -= 1;
			quantityTextDisplayList = Resize(currentQuantityTextDisplay, quantityTextDisplayList);
		}
		else
		{
			currentQuantityTextDisplay = 0;
			quantityTextDisplayList = Resize(1, quantityTextDisplayList);
			quantityTextDisplayList[0] = 0;
		}
	}
	
	public void BankNote100()
	{
		int c = currentChange + 100;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;

		}
        else { 
			currentChange += 100;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void BankNote50()
	{
		int c = currentChange + 50;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else { 
			currentChange += 50;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void BankNote20()
	{
		int c= currentChange + 20;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else { 
			currentChange += 20;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void BankNote10()
	{
		int c = currentChange + 10;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else { 
			currentChange += 10;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void BankNote5()
	{
		int c = currentChange + 5;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else { 
			currentChange += 5;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void BankNote2()
	{
		int c = currentChange + 2;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else { 
			currentChange += 2;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	
	public void BankNote1()
	{
		int c = currentChange + 1;
		if ((customercash - c) < totalPrice) { 
			currentChange += 0;
			english = "The cash should be exactly $" + totalPrice.ToString() + ".";
			cantonese = "現收款現金需等於" + totalPrice.ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
		else { 
			currentChange += 1;
			english = "Good going! Go ahead. Cash left: $" + (customercash - c).ToString() + ".";
			cantonese = "加油！ 請繼續！ 餘下款額 : " + (customercash - c).ToString() + "。";
			calcinstructions.text = english + "\n" + cantonese;
		}
	}
	// End Button Clicked Function ===============================================================================================================
}

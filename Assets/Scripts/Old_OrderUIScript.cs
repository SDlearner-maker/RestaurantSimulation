using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
public class OrderUIScript : MonoBehaviour
{
	public GameObject customer;
	// MakeOrderUI Object related
	public GameObject makeOrderUI;
	public GameObject makeOrderButton;
	public Text makeOrderUI_ObjectText;
	public Text quantityText;

	private int[] selectedList = new int[1];
	private int currentSelectedNum = 0;

	private int[] quantityList = new int[1];
	private int currentQuantity = 0;

	// QuantityUI Object related
	public GameObject quantityUI;
	public Text quantityUI_ObjectText;
	public Text quantityTestDisplayText;
	public Text quantityRightWrongText;

	private int[] quantityTextDisplayList = new int[1];
	private int currentQuantityTextDisplay = 0;

	// CheckOutUI Object related
	public GameObject checkOutUI;
	public GameObject CheckOutPrefab;

	private GameObject[] checkOutItemList;

	//List<string> orderslist = new List<string>();
	Dictionary<string, int> orderslist = new Dictionary<string, int>();

	public Text checkOutTestDisplayText;
	public Text checkOutRightWrongText;

	private int totalPrice = 0;
	private int currentChange = 0;

	// ChangeUI Object related
	public GameObject changeUI;

	public Text changeTestDisplayText;
	public Text changeRightWrongText;

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

		makeOrderUI.SetActive(false);
		makeOrderButton.SetActive(true);

		quantityUI.SetActive(false);

		checkOutUI.SetActive(false);

		changeUI.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

		if (makeOrderUI.activeSelf == true)
		{
			if (selectedList[0] != 0)
			{
				//ShowTextSelected(selectedList, makeOrderUI_ObjectText);
				ShowTextSelected(quantityList, quantityText);
			}
		}

		if (quantityUI.activeSelf == true)
		{
			//if (quantityTextDisplayList[0] != 0)
			//{
			ShowTextSelected(quantityTextDisplayList, quantityTestDisplayText);
			//}
		}

		if (checkOutUI.activeSelf == true)
		{
			ShowTextSelected(quantityTextDisplayList, checkOutTestDisplayText);
		}

		if (changeUI.activeSelf == true)
		{
			changeTestDisplayText.text = (totalPrice - currentChange).ToString();
		}
	}

	// Greeting and Order
	public void GreetingAndOrder()
	{
		makeOrderButton.SetActive(true);
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
		string str = "";
		foreach (int i in integerArray)
		{
			str = str + " " + i.ToString();
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
		/*
		orderdrink= customer.GetComponent<Billing>().OrderGiveDrink();
		orderfood= customer.GetComponent<Billing>().OrderGiveFood();
		Debug.Log(orderdrink); 

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

		//orderdrink = drinks;

		//Debug.Log("Order: " + orderdrink+ "Quantity: "+ quantitydrink); 

	}

	// End Tool Function =========================================================================================================================

	// UI Related Function =======================================================================================================================
	// Show OrderUI
	public void MakeOrder()
	{
		Orders();
		makeOrderUI.SetActive(true);
		quantityUI.SetActive(false);
	}

	// Show QuantityUI and Get the Latest Selected Object
	public void Quantity(int index, string name)
	{
		makeOrderUI.SetActive(false);
		quantityUI.SetActive(true);
		quantityUI_ObjectText.text = name;
		//makeOrderUI_ObjectText.text = makeOrderUI_ObjectText.text + " " + name; 

		//string drink=
		/*
		switch(index){
			case 1:
				quantityUI_ObjectText.text = "Burger";				
				break;
			case 2:
				quantityUI_ObjectText.text = "Sandwich";
				break;
			case 3:
				quantityUI_ObjectText.text = "Bread";
				break;
			case 4:
				quantityUI_ObjectText.text = "cup of iced Tea";
				break;
			case 5:
				quantityUI_ObjectText.text = "Coffee";
				break;
			case 6:
				quantityUI_ObjectText.text = "Milk";
				break;
			default:
				quantityUI_ObjectText.text = "Error Object";
				makeOrderUI_ObjectText.text = "";
				break;
		}
		if (orderdrink!= name) { Debug.Log("Wrong order, try again"); }
	}

	public void EnterQuantity()
	{
		string qtydrink = quantitydrink.ToString();
		string qtyfood = quantityfood.ToString();
		string qty = "";

		if (quantityUI_ObjectText.text == orderdrink) { qty = qtydrink; }
		else if (quantityUI_ObjectText.text == orderfood) { qty = qtyfood; }

		if (quantityTestDisplayText.text != "0" && (quantityTestDisplayText.text == qty))
		{
			currentQuantity += 1;
			quantityList = Resize(currentQuantity, quantityList);
			quantityList[currentQuantity - 1] = ArrayToInt(quantityTextDisplayList);
			makeOrderUI_ObjectText.text = makeOrderUI_ObjectText.text + " " + quantityUI_ObjectText.text;

			quantityTextDisplayList = new int[1];
			currentQuantityTextDisplay = 0;

			makeOrderUI.SetActive(true);
			quantityUI.SetActive(false);
		}
		else
		{
			Debug.Log("Enter value>0, entered value: " + quantityTestDisplayText.text + " correct value: " + qty);
			quantityRightWrongText.text = "Try Again";
		}
	}

	public void QuantityBack()
	{
		if (currentSelectedNum > 1)
		{
			currentSelectedNum -= 1;
			selectedList = Resize(currentSelectedNum, selectedList);
		}
		else
		{
			currentSelectedNum = 0;
			selectedList = Resize(1, selectedList);
			selectedList[0] = 0;
		}

		quantityTextDisplayList = new int[1];
		currentQuantityTextDisplay = 0;

		makeOrderUI.SetActive(true);
		quantityUI.SetActive(false);
	}

	public void CheckOut()
	{
		if (currentSelectedNum != 0)
		{

			if (orderslist.Count == 0)
			{
				checkOutUI.SetActive(true);
				makeOrderUI.SetActive(false);
				quantityUI.SetActive(false);

				Debug.Log("All orders done");
			}
			else { Debug.Log("Fill the orders first"); }

			for (int i = 0; i < selectedList.Length; i++)
			{
				GameObject canvas = GameObject.Find("Canvas");
				GameObject go = Instantiate(CheckOutPrefab, new Vector3(canvas.GetComponent<RectTransform>().rect.width / 2, checkOutUI.GetComponent<RectTransform>().rect.height - 100 - (i * 30), 0), Quaternion.identity) as GameObject;
				go.transform.SetParent(checkOutUI.transform);
				GameObject nameText = go.gameObject.transform.GetChild(0).gameObject;
				GameObject perPriceText = go.gameObject.transform.GetChild(1).gameObject;
				GameObject quantText = go.gameObject.transform.GetChild(2).gameObject;
				GameObject totalText = go.gameObject.transform.GetChild(3).gameObject;

				switch (selectedList[i])
				{
					case 1:
						nameText.GetComponent<Text>().text = "Burger";
						break;
					case 2:
						nameText.GetComponent<Text>().text = "Sandwich";
						break;
					case 3:
						nameText.GetComponent<Text>().text = "Bread";
						break;
					case 4:
						nameText.GetComponent<Text>().text = "Milk";
						break;
					case 5:
						nameText.GetComponent<Text>().text = "Ice Tea";
						break;
					case 6:
						nameText.GetComponent<Text>().text = "Coffee";
						break;
				}

				quantText.GetComponent<Text>().text = quantityList[i].ToString();

				int objectTotalPrice = 0;
				switch (selectedList[i])
				{
					case 1:
						perPriceText.GetComponent<Text>().text = "$10";
						objectTotalPrice = quantityList[i] * 10;
						break;
					case 2:
						perPriceText.GetComponent<Text>().text = "$5";
						objectTotalPrice = quantityList[i] * 5;
						break;
					case 3:
						perPriceText.GetComponent<Text>().text = "$2";
						objectTotalPrice = quantityList[i] * 2;
						break;
					case 4:
						perPriceText.GetComponent<Text>().text = "$12";
						objectTotalPrice = quantityList[i] * 12;
						break;
					case 5:
						perPriceText.GetComponent<Text>().text = "$15";
						objectTotalPrice = quantityList[i] * 15;
						break;
					case 6:
						perPriceText.GetComponent<Text>().text = "$20";
						objectTotalPrice = quantityList[i] * 20;
						break;
				}
				totalText.GetComponent<Text>().text = objectTotalPrice.ToString();
				totalPrice += objectTotalPrice;
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
			checkOutRightWrongText.text = "Correct";
			Change();
		}
		else
		{
			Debug.Log("Wrong");
			checkOutRightWrongText.text = "Try Again";
		}
	}


	public void Change()
	{
		makeOrderUI.SetActive(false);
		quantityUI.SetActive(false);
		checkOutUI.SetActive(false);
		changeUI.SetActive(true);
	}

	public void EnterChange()
	{
		if ((totalPrice - currentChange) == 0)
		{
			Debug.Log("Correct");
			changeRightWrongText.text = "Correct";
		}
		else
		{
			Debug.Log("Wrong");
			changeRightWrongText.text = "Try Again";
		}
	}

	public void RetryChange()
	{
		currentChange = 0;
	}
	// End UI Related Function ===================================================================================================================


	// Button Clicked Function ===================================================================================================================
	// Burger = 1, Sandwich = 2, Bread = 3, Ice Tea = 4, Coffee = 5, Milk = 6
	// When Burger was selected
	public void SelectBurger()
	{
		int index = 1;
		string name = "Burger";
		//Orders();

		//foreach(string w in orderslist) { Debug.Log(w); }
		//if (orderslist.Contains("Burger")) { Debug.Log("ordercontained"); }

		if (orderfood == name && orderslist.ContainsKey(name))
		{

			Quantity(index, name);

			currentSelectedNum += 1; //how many objects at start no. of obj=0, then ++1
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 1; //list: do -1, else out of index

			orderslist.Remove(name);
			/*
			if (orderslist.Count == 0) { Debug.Log("All orders done"); }
			else
			{
				foreach (string w in orderslist) { Debug.Log("itemleft: " + w); }
			} 

		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderfood);
		}
	}

	// When Sandwich was selected
	public void SelectSandwich()
	{
		int index = 2;
		string name = "Sandwich";
		//Orders();

		if (orderfood == name && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 2;

			orderslist.Remove(name);
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderfood);
		}
	}

	// When Bread was selected
	public void SelectBread()
	{
		int index = 3;
		string name = "Bread";
		//Orders();

		if (orderfood == name && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 3;

			orderslist.Remove(name);
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderfood);
		}

	}

	// When IceTea was selected
	public void SelectIceTea()
	{
		int index = 4;
		string name = "Ice tea";
		//Orders();

		if (name == orderdrink && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			Debug.Log("Correct");

			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 4;

			orderslist.Remove(name);
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderdrink);
		}

	}

	// When Coffee was selected
	public void SelectCoffee()
	{
		int index = 5;
		string name = "Coffee";
		//Orders();

		if (name == orderdrink && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			Debug.Log("Correct");

			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 5;

			orderslist.Remove(name);
		}
		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderdrink);
		}

	}

	// When Milk was selected
	public void SelectMilk()
	{
		int index = 6;
		string name = "Milk";
		//Orders();

		if (name == orderdrink && orderslist.ContainsKey(name))
		{
			Quantity(index, name);
			Debug.Log("Correct");

			currentSelectedNum += 1;
			selectedList = Resize(currentSelectedNum, selectedList);
			selectedList[currentSelectedNum - 1] = 6;

			orderslist.Remove(name);
		}

		else
		{
			Debug.Log("selected: " + name + " ordered: " + orderdrink);
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
		currentChange += 100;
	}

	public void BankNote50()
	{
		currentChange += 50;
	}

	public void BankNote20()
	{
		currentChange += 20;
	}

	public void BankNote10()
	{
		currentChange += 10;
	}

	public void BankNote5()
	{
		currentChange += 5;
	}

	public void BankNote2()
	{
		currentChange += 2;
	}

	public void BankNote1()
	{
		currentChange += 1;
	}
	// End Button Clicked Function ===============================================================================================================
} */



using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoKitchens : MonoBehaviour
{
    public GameObject orderslistUI;    
    public Text foodorder;
    public Text foodqty;
    public Text drinkorder;
    public Text drinkqty;    
    public GameObject foodobj;
    public GameObject drinkobj;
    public GameObject mainscreencam;
    public GameObject kitchenscreencam;
    public GameObject greetawaybutton;
    private string english;
    private string cantonese;
    private int ffqty;
    private int ddqty;

    public GameObject[] foodprefab;
    public GameObject drinkprefab;
    public GameObject trayprefab;

    //public GameObject instructionkitchenbox;
    //public GameObject ok;

    private string item;

    private string food;
    private string drink;
    private bool fooddoneyes=false;
    private bool drinkdoneyes = false;
    
    Dictionary<string, string> TheorderList = new Dictionary<string, string>();
    public void ListOrders(string fooditem, string drinkitem, int fqty, int dqty)
    {
        if (fooditem != "")
        {
            foodorder.text = fooditem;
            foodqty.text = fqty.ToString();
            TheorderList.Add(foodorder.text, foodqty.text);
            food = fooditem;
            ffqty = fqty;
        }
        else
        {
            foodorder.text = "None";
            foodqty.text = "None";
            ffqty = 0;
        }
                
        if (drinkitem != "")
        {
            drinkorder.text = drinkitem;
            drinkqty.text = dqty.ToString();
            TheorderList.Add(drinkorder.text, drinkqty.text);
            drink = drinkitem;
            ddqty = dqty;
        }
        else
        {
            drinkorder.text = "None";
            drinkqty.text = "None";
            ddqty = 0;
        }

        /*
        foodorder.text = fooditem;
        drinkorder.text = drinkitem;
        foodqty.text = fqty.ToString();
        drinkqty.text = dqty.ToString();
        orderslistUI.SetActive(true); */

    }

    /*
    public void GiveDrinkInstructions()
    {       
        
        instructionkitchen.text = "Place cup on the " + drink + " pad.\n";
        //Debug.Log(drinkobj.GetComponent<DrinkServiceGameManager>().onpaddone+" onpaddone");
        /*
        if (drinkobj.GetComponent<DrinkServiceGameManager>().onpaddone == true)
        {
            instructionkitchen.text = "Press the button of " + drink + ".\n";
            Debug.Log(instructionkitchen.text);
        }
        
        if (drinkobj.GetComponent<DrinkServiceGameManager>().drinkpoured == true)
        {
            instructionkitchen.text = "Good! You have made the drink. Place the cup on the tray now\n";
            Debug.Log(instructionkitchen.text);

            int drinkserved = drinkobj.GetComponent<DrinkServiceGameManager>().ReturnServed();
            int qtydrink = drinkobj.GetComponent<DrinkServiceGameManager>().qty;

            if (drinkobj.GetComponent<DrinkServiceGameManager>().placedontray == true)
            {
                item = "drink";
                Check(drinkserved, qtydrink);
                Debug.Log("Placed on tray "+item);
               
                if (served < qty)
                {
                    instructionkitchen.text = "Now repeat the process " + (qty - served).ToString() + " more times. Click on 'OK' button to continue";
                    Debug.Log(instructionkitchen.text);
                    ok.SetActive(true);
                    ProceedOk();
                    //GiveDrinkInstructions();
                }
                else if (served == qty)
                {
                    instructionkitchen.text = "Great job! You have finished all the orders. Now click on 'Done' button to go to counter.";
                    Debug.Log(instructionkitchen.text);
                }
            }
        } 

    }*/

    /*
    public void ProceedOk()
    {
        ok.SetActive(false);

        if(item == "drink")
        {
            GiveDrinkInstructions(); 
        }

        else if (item == "food")
        {
            GiveFoodInstructions();
        }
    }*/

    /*
    public void Check(int served, int qty)
    {
        if (served < qty)
        {
            instructionkitchen.text = "Now repeat the process " + (qty - served).ToString() + " more times. Click on 'OK' button to continue";
            Debug.Log(instructionkitchen.text);
            ok.SetActive(true);
            ProceedOk();
            //GiveDrinkInstructions();
        }
        else if (served == qty)
        {
            instructionkitchen.text = "Great job! You have finished all the orders. Now click on 'Done' button to go to counter.";
            Debug.Log(instructionkitchen.text);
        }
    }*/

    /*
    public void GiveFoodInstructions()
    {
       
        int foodserved = foodobj.GetComponent<FoodServiceGameManager>().ReturnServed();
        int qtyfood = Int32.Parse(foodqty.text);
        string foodinmiddle = food.Split()[0];
        instructionkitchen.text = "Drag and drop the bread from the 'Bread' box on the cutting board";
        instructionkitchen.text = "Drag and drop the " + foodinmiddle + " from the " + foodinmiddle + " box on the cutting board";
        instructionkitchen.text = "Press 'C' button on the oven to close the oven";
        instructionkitchen.text = "Press the buttons '1' and then '0' on the oven to set the time for 10 seconds.";
        instructionkitchen.text = "Press the 'S' button on the oven to start the oven and count till 10.";
        instructionkitchen.text = "Press on 'E' button on the oven to end the heating process. Then press on 'O' to open the oven";
        instructionkitchen.text = "Good! You have made the sandwich. Drag the sandwich to the tray. Then press on 'O' to close the oven";
        instructionkitchen.text = "Now repeat the process " + "" + "times";

    }
    public void GiveInstructions()
    {
        //string instr;
        //string instr = this.GetComponent<MoveCam>().kitinstructtions.text;
        if (drink != null && food == null)
        {
            //make drink only
            GiveDrinkInstructions();
        }

        else if (drink == null && food != null)
        {
            GiveFoodInstructions();
        }
        else if (drink != null && food != null)
        {
            //make drink first then food
            GiveDrinkInstructions();                      
        }
    }*/
       
    public void CheckAlldone()
    {
        //if (foodorder.text!= null)
        
        if (food != null)
        {
            fooddoneyes = foodobj.GetComponent<FoodServiceGameManager>().done;
            if (fooddoneyes == true)
            {
                Debug.Log("Food done");
                Debug.Log(fooddoneyes);
                TheorderList.Remove(foodorder.text);
            }
            //kitchenscreencam.SetActive(false);
            //mainscreencam.SetActive(true);
        }

        //if (drinkorder.text!= null)
        if (drink != null)
        {
            drinkdoneyes = drinkobj.GetComponent<DrinkServiceGameManager>().done;
            if (drinkdoneyes == true)
            {
                Debug.Log("Drink done"); //drink is done
                Debug.Log(drinkdoneyes);
                TheorderList.Remove(drinkorder.text);
            }
            //kitchenscreencam.SetActive(false);
            //mainscreencam.SetActive(true);
        }
    }

    public void Comeback()
    {
        CheckAlldone();
        foreach (var v in TheorderList)
        {
            Debug.Log(v);
        }
        if (TheorderList.Count==0)
        {
            kitchenscreencam.SetActive(false);
            orderslistUI.SetActive(false);
            mainscreencam.SetActive(true);
            this.GetComponent<MoveCam>().SetKitchenUIinactive();
        }

        GameObject foodinst = null;
        int xfood = -190, yfood = -48, zfood = -444;
        switch (foodorder.text)
        {
            case "Cheese sandwich":
                foodinst = foodprefab[0];
                break;
            case "Egg sandwich":
                foodinst = foodprefab[1];
                break;
            case "Ham sandwich":
                foodinst = foodprefab[2];
                break;
        }

        for (int i = 0; i < ffqty; i++)
        {
            GameObject instantiatedfoodobj = Instantiate(foodinst, new Vector3(xfood, yfood, zfood), Quaternion.identity) as GameObject;
            yfood+= 7;
        }
        
        int xdrink = -172, ydrink = -54, zdrink = -420; 

        for (int i = 0; i < ddqty; i++)
        {
            GameObject instantiateddrinkobj = Instantiate(drinkprefab, new Vector3(xdrink, ydrink, zdrink), Quaternion.identity) as GameObject;
            zdrink -= 10;
        }

        GameObject instantiatedtrayobj = Instantiate(trayprefab, new Vector3(-190, -55, -420), Quaternion.identity) as GameObject;

        Debug.Log(TheorderList.Count);
        greetawaybutton.SetActive(true);

        foodobj.GetComponent<FoodServiceGameManager>().instructionfoodbox.SetActive(false);
        drinkobj.GetComponent<DrinkServiceGameManager>().instructiondrinkbox.SetActive(false);
        english = "Congratulations! You have finished preparing all the orders. Now click on 'Final Greet' button and say: 'Please collect your order, thank you!'";
        cantonese = "恭喜！ 你已完成所有訂單的準備工作。 現在點擊 'Final Greet' 鍵並說：'Please collect your order, thank you!'";
        this.GetComponent<OrderUIScript>().instructions.text = english + "\n" + cantonese;
    }    
    public void MakeFood(string scene) //no need but never comment out
    {
        SceneManager.LoadScene(scene);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        foodorder.text = "";
        drinkorder.text = "";
        foodqty.text = "";
        drinkqty.text = "";

        greetawaybutton.SetActive(false);
        //ok.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Billing : MonoBehaviour
{
    [HideInInspector]
    public string drink, food;
    [HideInInspector]
    public int quantitydrink, quantityfood;

    public void BillofCustomer(string ordercustomer1, string ordercustomer2)
    {
        drink = ""; food = "";
        quantitydrink = 0; quantityfood = 0;
        string customerorderdrink = "";
        string customerorderfood = "";

        int drinklen = 0;
        int foodlen = 0;
        string[] orderdrink= { };
        string[] orderfood= { };

        if (ordercustomer1.Contains("cup"))
        {
            customerorderdrink = ordercustomer1;
            customerorderfood = ordercustomer2;
        }
        else  
        {
            customerorderdrink = ordercustomer2;
            customerorderfood = ordercustomer1;            
        }

        orderdrink = customerorderdrink.Trim().Split(); drinklen = orderdrink.Length;
        orderfood = customerorderfood.Trim().Split(); foodlen = orderfood.Length;
        Debug.Log(orderfood[0]);        

        if (Array.Exists(orderdrink, x => x == "cups") && (orderdrink!=null))
        {
            int cupsof = Array.IndexOf(orderdrink, "cups");
            orderdrink[cupsof + 2] = char.ToUpper(orderdrink[cupsof + 2][0]) + orderdrink[cupsof + 2].Substring(1);
        }
        else if (Array.Exists(orderdrink, x => x == "cup") && (orderdrink != null))
        {
            int cupof = Array.IndexOf(orderdrink, "cup");
            orderdrink[cupof + 2] = char.ToUpper(orderdrink[cupof + 2][0]) + orderdrink[cupof + 2].Substring(1);
        }

        if (orderfood[0]!="")
        {
            orderfood[orderfood.Length - 2] = char.ToUpper(orderfood[orderfood.Length - 2][0]) + orderfood[orderfood.Length - 2].Substring(1);
        }

        else { Debug.Log("No food order"); }
        string[] numbers = { "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        if (orderdrink[0] == "a" || orderdrink[0] == "one") { 
            quantitydrink = 1;
        }

        else if (numbers.Contains(orderdrink[0]))
        {
            orderdrink[1] = (orderdrink[1]).Remove((orderdrink[1]).Length - 1, 1);            
            quantitydrink = Array.IndexOf(numbers, orderdrink[0]) + 2;
        }

        if (orderfood[0] == "a" || orderfood[0] == "one") { 
            quantityfood = 1;             
        }

        else if (numbers.Contains(orderfood[0]))
        {
            orderfood[foodlen-1] = (orderfood[foodlen- 1]).Remove((orderfood[foodlen - 1]).Length - 1, 1);
            if (orderfood[foodlen - 1] == "sandwiche") { orderfood[foodlen - 1] = (orderfood[foodlen - 1]).Remove((orderfood[foodlen - 1]).Length - 1, 1); }
            quantityfood = Array.IndexOf(numbers, orderfood[0]) + 2;
        }

        for (int i=3; i<orderdrink.Length; i++)
        {
            drink = drink + " " + orderdrink[i]; 
        }
        for (int i = 1; i < orderfood.Length; i++)
        {
            food = food + " " + orderfood[i];
        }

        drink= drink + "/" + quantitydrink.ToString();
        food = food +  "/" + quantityfood.ToString();

        drink = drink.ToString();
        food = food.ToString();

        drink = drink.Trim();
        food = food.Trim();

        int x = Int32.Parse(drink.Substring(drink.IndexOf('/') + 1));
        string y = drink.Substring(0, drink.IndexOf('/'));

        int xx = Int32.Parse(food.Substring(food.IndexOf('/') + 1));
        string yy = food.Substring(0, food.IndexOf('/'));

        Debug.Log("Orderdrink: " + drink);
        Debug.Log("quantity: " + x + "item: " + y);
        Debug.Log("Orderfood: " + food);
        Debug.Log("quantity: " + xx + "item: " + yy);
    }

    public string OrderGiveDrink() { return drink; } 
    public string OrderGiveFood() { return food; }
    // Start is called before the first frame update
    void Start()
    {
        OrderGiveDrink();
        OrderGiveFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public GameObject customer;
    //string customerorderdrink = customer.GetComponent<WordsText>().order1;
    //string customerorderfood = customer.GetComponent<WordsText>().order2;
}

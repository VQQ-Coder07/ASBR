using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numere : MonoBehaviour
{
    public int[] numbers = {-4, 0, 6, 9};
    private string value;

    private protected void Start()
    {
        foreach (int i in numbers)
        {
            if(i <= 0)
            {
                if(value != "pozitive" && value != "mixte")
                {
                    value = "negative";
                }
                else if(value == "pozitive")
                {
                    value = "mixte";
                }
            }
            else if(i >= 0)
            {
                if(value != "negative" && value != "mixte")
                {
                    value = "pozitive";
                }
                else if(value == "negative")
                {
                    value = "mixte";
                }
            }
        }
        Debug.Log("Numerele tale sunt " + value + " !");
    }
}
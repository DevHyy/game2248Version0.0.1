using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
   /* public static string GetNumberAbbreviation(this int number)
    {
        string result = "";
        if (number >= 1000000)
            result = (number / 1000000) + "M";
        else if (number >= 1000)
            result = (number / 1000) + "K";
        else
        {
            result = number.ToString();
        }
        return result;
    }

    public static string GetNumberAbbreviation(this ulong number)
    {
        string result = "";
        if (number >= Mathf.Pow(10, 12))
            result = Mathf.RoundToInt(number / Mathf.Pow(10, 12)) + "a";
        else if (number >= 1000000000)
            result = Mathf.RoundToInt((number / 1000000000)) + "B";
        else if (number >= 1000000)
            result = Mathf.RoundToInt(number / 1000000) + "M";
        else if (number >= 1000)
            result = Mathf.RoundToInt(number / 1000) + "K";
        else
        {
            result = number.ToString();
        }
        return result;
    }*/
}

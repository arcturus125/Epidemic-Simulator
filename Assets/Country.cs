using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CountryName", menuName = "Country", order = 1)]
public class Country : ScriptableObject
{
    public string countryName;
    public int population;
}

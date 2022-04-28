using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    public GameObject startingCountry;
    public Virus playerVirus;

    public GameObject startingCountry2;
    public Virus playerVirus2;

    // Start is called before the first frame update
    void Start()
    {
        startingCountry.GetComponent<CountryComponent>().InfectCountry(playerVirus);
        if(playerVirus2 != null)
            startingCountry2.GetComponent<CountryComponent>().InfectCountry(playerVirus2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

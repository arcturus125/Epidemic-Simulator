using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    public GameObject startingCountry;
    public Virus playerVirus;

    // Start is called before the first frame update
    void Start()
    {
        startingCountry.GetComponent<CountryComponent>().InfectCountry(playerVirus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

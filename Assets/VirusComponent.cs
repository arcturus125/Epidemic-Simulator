using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusComponent : MonoBehaviour
{
    static Color defaultColour = new Color(209,219,221);
    const int StartInfectionNumber = 10;
    const float timestep = 0.1f;

    public Country countryReference;
    public CountryComponent countryComponent;
    public Virus virus;

    [SerializeField] int susceptible;
    [SerializeField] int infected;
    [SerializeField] int killed;
    [SerializeField] int immune;

    int tick = 0;

    public Queue<int> infectedBuffer = new Queue<int>();
    SpriteRenderer renderer;

    public bool beginInfection = false;
    public void BeginInfection()
    {
        Infect(StartInfectionNumber);
    }


    private void Start()
    {
        susceptible = countryReference.population;
        InvokeRepeating("Tick", timestep, timestep);
        renderer = GetComponent<SpriteRenderer>();
        if (beginInfection)
        {
            beginInfection = false;
            BeginInfection();
        }
    }
    //private void Update()
    //{
    //    Tick();
    //}

    void UpdateGraphics()
    {
        if (susceptible > 0 && infected == 0)
        {
            renderer.color = Color.green;
            return;
        }

        int total = infected + killed;
        float percent = total / (float)countryReference.population;
        renderer.color = Color.white * (1-percent) + (virus.colour * percent);

        if(susceptible == 0 && infected == 0)
        {
            //renderer.color = Color.green;
            float tempImmune = immune;
            if (tempImmune <= 0) tempImmune = 1;
            percent = killed / (float)tempImmune;
            renderer.color = new Color(1 - percent, 1 - percent, 1 - percent,1) + (new Color(0,0,percent,1));
        }
    }

    public void Tick()
    {
        //if (stop)
        //{
        //    if(infected <= 0)
        //        return;
        //}

        int numberToInfect = Mathf.RoundToInt( Random.Range(0,infected * virus.R) );
        Infect(numberToInfect);
        //infectedBuffer.Enqueue(numberToInfect);

        if (tick > virus.incubationPeriod)
        {
            //Defect(infectedBuffer.Dequeue());
            Defect(Mathf.RoundToInt( infected * 0.2f) +1);
        }


        // the higher the percentage of population infected, the more chance of transferring virus to another country
        float rng = Random.Range(0.0f,1.0f);
        float popInfectedPercentage = infected / (float) (countryReference.population - killed);
        if (rng < popInfectedPercentage * 2)
        {

            Country c = countryComponent.GetRandomNeighbor();
            CountryComponent otherCountry = c.gameObject.GetComponent<CountryComponent>();
            if (!otherCountry.DoesCountryContainVirus(virus))
                otherCountry.InfectCountry(virus);
            else
                otherCountry.ReinfectCountry(virus);
        }

        UpdateGraphics();
        tick++;
    }

    void Infect(int numberToInfect)
    {
        if (susceptible > numberToInfect)
        {
            susceptible -= numberToInfect;
            infected += numberToInfect;
        }
        else
        {
            infected += susceptible;
            susceptible = 0;
        }
    }
    void Defect(int numberToDefect)
    {
        float rng = Random.Range(0.0f, 1.0f);
        //death
        if (rng < virus.fatalityRate)
        {
            Kill(numberToDefect);
        }
        //no death
        else
        {
            rng = Random.Range(0.0f, 1.0f);
            if (rng < virus.chanceOfRecurrance)
            {
                // person may become infected again
                if (infected > numberToDefect)
                {
                    infected -= numberToDefect;
                    susceptible += numberToDefect;
                }
                else
                {
                    susceptible += infected;
                    infected = 0;
                }
            }
            else
            {
                // person become completely immune to reinfection of this virus
                Immunize(numberToDefect);
            }
        }
    }
    void Kill(int numberToKill)
    {
        if (infected > numberToKill)
        {
            infected -= numberToKill;
            killed += numberToKill;
        }
        else
        {
            killed += infected;
            infected = 0;
        }
    }
    void Immunize(int numberToImmunize)
    {
        if (infected > numberToImmunize)
        {
            infected -= numberToImmunize;
            immune += numberToImmunize;
        }
        else
        {
            immune += infected;
            infected = 0;
        }
    }
}

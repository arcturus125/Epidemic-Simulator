
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Test : MonoBehaviour
{


    public void Run()
    {
        using (StreamReader parser = new StreamReader(@"C:\Users\jackg\Downloads\csvData.csv"))
        {

            while (!parser.EndOfStream)
            {
                //Processing row
                string line = parser.ReadLine();
                string[] fields = line.Split(',');
                string name = fields[0];
                int pop = (int)float.Parse(fields[1]) * 1000;

                Debug.Log(name);

                Country count = ScriptableObject.CreateInstance<Country>();
                count.countryName = name;
                count.population = pop;

                AssetDatabase.CreateAsset(count, $"Assets/Resources/{name}.asset");
                AssetDatabase.SaveAssets();

            }
        }


        Country c = ScriptableObject.CreateInstance<Country>();
    }
}

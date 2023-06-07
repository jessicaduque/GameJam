using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Lingua", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Port()
    {
        PlayerPrefs.SetInt("Lingua", 0);
    }
    public void Eng()
    {
        PlayerPrefs.SetInt("Lingua", 1);
    }

}

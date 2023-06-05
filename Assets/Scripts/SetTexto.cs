using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTexto : MonoBehaviour
{
    [SerializeField]
    private string en_Text;

    [SerializeField]
    private string pt_Text;


    void Update()
    {
        if(PlayerPrefs.GetInt("Lingua") == 0)
        {
            GetComponent<Text>().text = pt_Text;
        }
        else if(PlayerPrefs.GetInt("Lingua") == 1)
        {
            GetComponent<Text>().text = en_Text;
        }
    }
}

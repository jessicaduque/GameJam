using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarFadesImagem : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GetComponentInChildren<Fade>().FadeIn();
    }

    void Update()
    {
        VoltarEDesligarPanel();
    }

    void VoltarEDesligarPanel()
    {
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            GetComponentInChildren<Fade>().FadeOut();
        }
    }

}

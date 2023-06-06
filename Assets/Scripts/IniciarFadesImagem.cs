using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarFadesImagem : MonoBehaviour
{
    public GameObject imagemParaFadePanel;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        imagemParaFadePanel.SetActive(true);
        imagemParaFadePanel.GetComponent<Fade>().FadeIn();
    }


    void Update()
    {
        VoltarEDesligarPanel();
    }

    void VoltarEDesligarPanel()
    {
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            imagemParaFadePanel.GetComponent<Fade>().FadeOut();
        }
        if (!imagemParaFadePanel.activeSelf)
        {
            this.enabled = false;
        }
    }

}

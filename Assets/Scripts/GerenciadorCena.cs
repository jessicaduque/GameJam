using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorCena : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjetoInicial;
    private GameObject TelaPretaPanel;

    void Start()
    {
        TelaPretaPanel = GameObject.FindGameObjectWithTag("TelaPretaPanel");

        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2)
        {
            TelaPretaPanel.GetComponent<TelaPretaFade>().enabled = true;
            TelaPretaPanel.GetComponent<TelaPretaFade>().FadeOut();
        }
    }

    void Update()
    {
        if (ObjetoInicial != null)
        {
            if (!ObjetoInicial.GetComponent<InteracaoParaFalas>().enabled)
            {
                ObjetoInicial.GetComponent<InteracaoParaFalas>().enabled = true;
                ObjetoInicial = null;
            }
            
        }
    }

    void TrancarMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}

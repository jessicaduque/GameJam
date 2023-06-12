using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorCena : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjetoInicial;

    void Start()
    {
        
    }

    void Update()
    {
        if (ObjetoInicial != null)
        {
            if (!ObjetoInicial.GetComponent<InteracaoParaFalas>().enabled)
            {
                ObjetoInicial.GetComponent<InteracaoParaFalas>().enabled = true;
                Destroy(this.gameObject);
            }
            
        }
    }

    void TrancarMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorCena : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjetoInicial;

    void Start()
    {
        ObjetoInicial.GetComponent<InteracaoParaFalas>().enabled = true;
        // Deletar lingua depois
        PlayerPrefs.SetInt("Lingua", 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorCena : MonoBehaviour
{
    private GameObject Player;
    [SerializeField]
    private GameObject ObjetoInicial;

    void Start()
    {
        ObjetoInicial.GetComponent<InteracaoParaFalas>().enabled = true;
        // Deletar lingua depois
        PlayerPrefs.SetInt("Lingua", 0);
    }

    void Update()
    {
        
    }

}

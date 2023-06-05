using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorCena : MonoBehaviour
{
    private bool visualizouPosters;
    private GameObject Player;
    [SerializeField]
    private GameObject Lixeira;
    [SerializeField]
    private GameObject Posters;

    void Start()
    {
        Lixeira.GetComponent<InteracaoParaFalas>().enabled = true;
        visualizouPosters = false;
        PlayerPrefs.SetInt("Lingua", 0);
    }

    void Update()
    {
        
    }

    public void fezAlgumaParte(int num)
    {
        if(num == 0)
        {
            Posters.GetComponent<InteracaoParaFalas>().enabled = true;
            visualizouPosters = true;
        }
    }
}

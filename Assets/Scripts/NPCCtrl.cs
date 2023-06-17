using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float posInicial;
    [SerializeField] private float posAtual;
    [SerializeField] private float posFinal;

    private string direcao = "Esquerda";

    // Update is called once per frame
    void Update()
    {
        posAtual = transform.position.x;
        
        if(posAtual > posFinal && direcao == "Esquerda")
        {
            transform.position = new Vector2(transform.position.x - vel * Time.deltaTime, transform.position.y);
        }
        else
        {
            direcao = "Direita";
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector2(transform.position.x + vel * Time.deltaTime, transform.position.y);
        }

        if (posAtual < posInicial && direcao == "Direita")
        {
            transform.position = new Vector2(transform.position.x + vel * Time.deltaTime, transform.position.y);
        }
        else
        {
            direcao = "Esquerda";
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector2(transform.position.x - vel * Time.deltaTime, transform.position.y);
        }



    }
}

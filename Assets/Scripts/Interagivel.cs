using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interagivel : MonoBehaviour
{
    private GameObject Player;
    [SerializeField]
    private GameObject ImageToShowPanel;
    [SerializeField]
    private GameObject IndicateInteractionPanel;
    [SerializeField]
    private float DistanciaParaInteracao;
    private bool podeInteragir;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        podeInteragir = false;
    }

    void Update()
    {
        if (podeInteragir)
        {
            IndicateInteractionPanel.SetActive(false);
            Interacao();
            podeInteragir = false;
            
        }
        else
        {
            if (ChecarSePodeInteragir())
            {
                podeInteragir = true;
            }
            else
            {
                ChecarSePodeInteragir();
            }
        }
    }

    bool ChecarSePodeInteragir()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) <= DistanciaParaInteracao)
        {
            if (!ImageToShowPanel.activeSelf && !GetComponent<InteracaoParaFalas>().enabled)
            {
                IndicateInteractionPanel.SetActive(true);
            }
            else
            {
                IndicateInteractionPanel.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            IndicateInteractionPanel.SetActive(false);
            return false;
        }
    }

    void Interacao()
    {
        IndicateInteractionPanel.SetActive(false);
        ImageToShowPanel.SetActive(true);
        Player.GetComponent<Personagem>().PrenderPersonagem();
    }
}

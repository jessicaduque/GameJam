using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleMusicaInicial : MonoBehaviour
{
    private GameObject Player;
    public AudioSource AuSource;
    public AudioClip musicaFundoLixo;
    public AudioClip musicaFundoFeira;
    public Vector3 LimiteLixoFeira;
    bool trocou = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AuSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.x > LimiteLixoFeira.x && AuSource.clip == musicaFundoLixo)
        {
            AuSource.clip = musicaFundoFeira;
            trocou = true;
        }
        else if(Player.transform.position.x <= LimiteLixoFeira.x && AuSource.clip == musicaFundoFeira)
        {
            AuSource.clip = musicaFundoLixo;
            trocou = true;
        }

        Troca();
    }

    void Troca()
    {
        if(trocou)
        {
            AuSource.Play();
            trocou = false;
        }
    }

 
}

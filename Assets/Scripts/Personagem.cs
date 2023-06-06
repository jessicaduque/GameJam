using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    private Rigidbody2D Corpo;
    private Animator Anim;
    private bool estaNoChao;
    private bool movimentoPermitido;
    private bool recebeuInputMover;
    private bool recebeuInputPular;
    private string lado;
    [SerializeField]
    private float velAndar;
    private float velFinal = 0.0f;
    [SerializeField]
    private float forcaPulo;
    private bool interagindo;

    public GameObject maisPerto = null;

    [SerializeField] 
    private Vector3 PosInicial;

    [SerializeField]
    GameObject IndicationInteractionPanel;

    void Start()
    {
        estaNoChao = true;
        movimentoPermitido = true;
        interagindo = false;

        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.position = PosInicial;
    }

    void Update()
    {
        ReceberInputs();

        AnimacaoAndar();

        PertoDeInteragivel();
    }

    private void FixedUpdate()
    {
        if(movimentoPermitido && recebeuInputMover && recebeuInputPular)
        {
            Movimento(velFinal, lado);
            Pular();
            recebeuInputPular = false;
        }
        else if (movimentoPermitido && recebeuInputMover)
        {
            Movimento(velFinal, lado);
        }
        else if (movimentoPermitido && recebeuInputPular)
        {
            Pular();
            recebeuInputPular = false;
        }
        else if (!recebeuInputMover)
        {
            Corpo.velocity = new Vector3(0, Corpo.velocity.y, 0);
        }
    }

    void AnimacaoAndar()
    {
        if (movimentoPermitido)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") != 0) && (!Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Run") == 0))
            {
                Anim.SetBool("Correndo", false);
                Anim.SetBool("Andando", true);

            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") != 0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Run") > 0))
            {
                Anim.SetBool("Andando", false);
                Anim.SetBool("Correndo", true);
            }
            else
            {
                Anim.SetBool("Correndo", false);
                Anim.SetBool("Andando", false);
            }

        }
    }
    void ReceberInputs()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") > 0)
        {
            lado = "esquerda";
            recebeuInputMover = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") < 0)
        {
            lado = "direita";
            recebeuInputMover = true;
        }
        else
        {
            lado = "";
            recebeuInputMover = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Run") > 0)
        {
            velFinal = velAndar * 2;
        }
        else
        {
            velFinal = velAndar;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            recebeuInputPular = true;
        }
    }

    void Movimento(float velocidade, string lado)
    {
        if (lado == "direita")
        {
            Corpo.velocity = new Vector3(velocidade, Corpo.velocity.y, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (lado == "esquerda")
        {
            Corpo.velocity = new Vector3(-velocidade, Corpo.velocity.y, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
    void Pular()
    {
        if (estaNoChao)
        {
            Corpo.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            estaNoChao = false;
        }
    }

    void PertoDeInteragivel()
    {
        maisPerto = null;

        GameObject[] Interagiveis;
        Interagiveis = GameObject.FindGameObjectsWithTag("InteragivelFalas");

        foreach (GameObject interagivel in Interagiveis)
        {
            if(Vector2.Distance(transform.position, interagivel.transform.position) <= 4)
            {
                maisPerto = interagivel;
            }
        }

        if (maisPerto != null && !interagindo && (!maisPerto.GetComponent<InteracaoParaFalas>().jaFoi || maisPerto.GetComponent<InteracaoParaFalas>().temImagemParaMostrar))
        {
            if (!maisPerto.GetComponent<InteracaoParaFalas>().podeRepetir && !maisPerto.GetComponent<InteracaoParaFalas>().temImagemParaMostrar)
            {
                maisPerto.GetComponent<InteracaoParaFalas>().enabled = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    maisPerto.GetComponent<InteracaoParaFalas>().enabled = true;
                }
                IndicationInteractionPanel.SetActive(true);
            }
          
        }
        else
        {
            IndicationInteractionPanel.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Chao")
        {
            estaNoChao = true;
        }
    }

    public void PrenderPersonagem()
    {
        movimentoPermitido = false;
        Corpo.velocity = new Vector3(0, Corpo.velocity.y, 0);
        Anim.SetBool("Correndo", false);
        Anim.SetBool("Andando", false);
    }
    public void DesprenderPersonagem()
    {
        movimentoPermitido = true;
    }
    public void ComecouInteracao()
    {
        interagindo = true;
    }

    public void AcabouInteracao()
    {
        interagindo = false;
    }
}

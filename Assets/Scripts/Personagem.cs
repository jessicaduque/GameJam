using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    private Rigidbody2D Corpo;
    private Animator Anim;
    private bool estaNoChao;
    private bool movimentoPermitido;
    private bool cooldownPularAtivado = false;
    private float cooldownPuloTempo = 0.0f;
    private bool recebeuInputMover;
    private bool recebeuInputPular;
    private string lado;
    [SerializeField]
    private float velAndar;
    private float velFinal = 0.0f;
    [SerializeField]
    private float forcaPulo;
    private bool interagindo;
    private bool deuExitPe = false;
    private bool deuEnterELiberado = false;

    public GameObject maisPerto = null;

    public Vector3 PosInicial;

    private GameObject IndicationInteractionPanel;

    void Start()
    {
        estaNoChao = true;
        movimentoPermitido = true;
        interagindo = false;

        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.position = PosInicial;

        IndicationInteractionPanel = GameObject.FindGameObjectWithTag("PainelInteracao");
    }

    void Update()
    { 

        ReceberInputs();

        AnimacaoAndar();

        PertoDeInteragivel();

        if (cooldownPularAtivado)
        {
            CooldownPulo();
        }
    }

    private void FixedUpdate()
    {
        if (movimentoPermitido && recebeuInputMover && recebeuInputPular)
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
            Corpo.velocity = new Vector2(0, Corpo.velocity.y);
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

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            recebeuInputPular = true;
        }
    }

    void Movimento(float velocidade, string lado)
    {
        if (lado == "direita")
        {
            Corpo.velocity = new Vector2(velocidade, Corpo.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (lado == "esquerda")
        {
            Corpo.velocity = new Vector2(-velocidade, Corpo.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
    void Pular()
    {
        if (estaNoChao && !cooldownPularAtivado)
        {
            Corpo.velocity = new Vector2(Corpo.velocity.x, forcaPulo);
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
            if(Vector2.Distance(transform.position, interagivel.transform.position) <= 2f)
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
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    maisPerto.GetComponent<InteracaoParaFalas>().enabled = true;
                }
                IndicationInteractionPanel.transform.GetChild(0).gameObject.SetActive(true);
            }
          
        }
        else
        {
            IndicationInteractionPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Chao")
        {
            estaNoChao = true;
        }
        else if (colidiu.gameObject.tag == "Plataforma")
        {
            if (deuEnterELiberado)
            {
                estaNoChao = true;
            }
            else
            {
                estaNoChao = false;
            }
        }
        else
        {
            estaNoChao = false;
        }
    }


    private void OnTriggerExit2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Plataforma")
        {
            estaNoChao = false;
            deuExitPe = true;
            deuEnterELiberado = false;
        }
        else
        {
            deuExitPe = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D colidiu)
    {
        if (colidiu.gameObject.tag == "Plataforma")
        {
            if (deuExitPe)
            {
                deuEnterELiberado = true;
                deuExitPe = false;
            }
            else
            {
                deuEnterELiberado = false;
            }
        }
    }

    public void PrenderPersonagem()
    {
        movimentoPermitido = false;
        Corpo.velocity = new Vector2(0, Corpo.velocity.y);
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

    public void CooldownPulo()
    {
        cooldownPuloTempo += Time.deltaTime;
        if(cooldownPuloTempo > 0.1f)
        {
            cooldownPularAtivado = false;
            cooldownPuloTempo = 0.0f;
        }
        else
        {
            cooldownPularAtivado = true;
        }
    }

}

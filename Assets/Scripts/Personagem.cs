using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    private Rigidbody2D Corpo;
    private Animator Anim;
    private bool estaNoChao;
    private bool movimentoPermitido;
    [SerializeField]
    private float velAndar;
    [SerializeField]
    private float forcaPulo;

    [SerializeField] 
    private Vector3 PosInicial;

    [SerializeField]
    GameObject IndicationInteractionPanel;

    void Start()
    {
        estaNoChao = true;
        movimentoPermitido = true;

        Corpo = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.position = PosInicial;
    }

    void Update()
    {
        if (movimentoPermitido)
        {
            Movimento();
            Pular();
        }
    }

    void Movimento()
    {
        float velocidade;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Run") > 0)
        {
            velocidade = velAndar * 2;
        }
        else
        {
            velocidade = velAndar;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Anim.SetBool("Andar", true);
        }
        else
        {
            Anim.SetBool("Andar", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") > 0)
        {
            transform.position = new Vector3(transform.position.x - (velocidade * Time.deltaTime), transform.position.y, transform.position.z);
            //transform.localScale = new Vector2(-1, 1);
            transform.localScale = new Vector3(1, 1, 1);
            //lado = "Esquerda";
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") < 0)
        {
            transform.position = new Vector3(transform.position.x + (velocidade * Time.deltaTime), transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1, 1, 1);
            //lado = "Direita";
        }

    }
    void Pular()
    {
        if (estaNoChao && (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            Corpo.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            estaNoChao = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "InteragivelFalas")
        {
            IndicationInteractionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "InteragivelFalas")
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

        if (colidiu.gameObject.tag == "InteragivelFalas")
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                IndicationInteractionPanel.SetActive(false);
                colidiu.gameObject.GetComponent<InteracaoParaFalas>().enabled = true;
            }
        }
    }

    public void PrenderPersonagem()
    {
        movimentoPermitido = false;
    }
    public void DesprenderPersonagem()
    {
        movimentoPermitido = true;
    }
}

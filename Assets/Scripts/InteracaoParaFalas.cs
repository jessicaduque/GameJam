using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteracaoParaFalas : MonoBehaviour
{
    [SerializeField] private List<string> en_falas = new List<string>();
    [SerializeField] private List<string> pt_falas = new List<string>();
    [SerializeField] private List<int> falantesDasFalas = new List<int>();
    [SerializeField] private List<string> falantes = new List<string>();
    [SerializeField] private List<Sprite> imagensFalantes = new List<Sprite>();

    [SerializeField] private List<int> pausaAntesDeFala = new List<int>();
    [SerializeField] private List<float> temposDasPausasAntesDasFalas = new List<float>();

    private int numeroFala = 0;
    private float tempo = 0.0f;
    private float tempoLetras = 0.0f;
    private int letra = 1;
    private bool falasRodando;
    private GameObject Player;

    public GameObject DialoguePanel;
    public Text falaTexto;
    public Text NomeFalante_Text;
    public Image Falante_Image;

    public bool podeRepetir;
    public bool jaFoi = false;
    public bool temImagemParaMostrar;
    private bool mostrandoImagem;
    [SerializeField]
    public bool teleportarDepois;
    [SerializeField]
    public int cenaParaIr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Personagem>().PrenderPersonagem();
        falaTexto.text = "";
        falasRodando = true;
        if (podeRepetir)
        {
            numeroFala = 0;
        }

        if (temImagemParaMostrar)
        {
            mostrandoImagem = true;
            DialoguePanel.SetActive(false);
        }
        else
        {
            mostrandoImagem = false;
            DialoguePanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControleFalas();
    }

    void ScriptFalas()
    {
        List<string> falas = null;
        
        if (PlayerPrefs.GetInt("Lingua") == 0)
        {
            falas = pt_falas;
        }
        else if (PlayerPrefs.GetInt("Lingua") == 1)
        {
            falas = en_falas;
        }
        
        
        if(imagensFalantes[falantesDasFalas[numeroFala]] == null)
        {
            Falante_Image.enabled = false;
        }
        else
        {
            Falante_Image.enabled = true;
            Falante_Image.sprite = imagensFalantes[falantesDasFalas[numeroFala]];
        }
        
        NomeFalante_Text.text = falantes[falantesDasFalas[numeroFala]];

        LetrasUmPorUm(falas);

        CliqueDoMouse(falas);
        
    }

    void CliqueDoMouse(List<string> falas)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (letra != falas[numeroFala].Length + 1)
            {
                falaTexto.text = falas[numeroFala];
                letra = falas[numeroFala].Length + 1;
            }
            else
            {
                if (tempo > 0.4f)
                {
                    if (numeroFala != falas.Count)
                    {
                        tempoLetras = 0.0f;
                        letra = 1;
                        tempo = 0.0f;
                        numeroFala++;
                    }
                }
            }
        }
    }

    void LetrasUmPorUm(List<string> falas)
    {
        tempoLetras += Time.deltaTime;

        if (tempoLetras > 0.05 * letra && letra != falas[numeroFala].Length + 1)
        {
            falaTexto.text = falas[numeroFala].Substring(0, letra);
            letra++;
        }
    }

    void ControleFalas()
    {
        if (falasRodando)
        {
            tempo += Time.deltaTime;
        }

        if (mostrandoImagem)
        {
            Player.GetComponent<Personagem>().ComecouInteracao();
            GetComponent<IniciarFadesImagem>().enabled = true;
            if (!GetComponent<IniciarFadesImagem>().imagemParaFadePanel.activeSelf)
            {
                mostrandoImagem = false;
            }
        }
        else if(numeroFala == pt_falas.Count)
        {
            AcabouFalas();
        }
        else
        {
            Player.GetComponent<Personagem>().PrenderPersonagem();
            Player.GetComponent<Personagem>().ComecouInteracao();

            if (numeroFala == pausaAntesDeFala[numeroFala])
            {
                if (tempo >= temposDasPausasAntesDasFalas[numeroFala])
                {
                    ScriptFalas();
                    DialoguePanel.SetActive(true);
                }
                else
                {
                    falaTexto.text = "";
                    DialoguePanel.SetActive(false);
                }
            }
            else
            {
                DialoguePanel.SetActive(true);
                ScriptFalas();
            }
        }
    }

    void AcabouFalas()
    {
        if (teleportarDepois)
        {
            SceneManager.LoadScene(cenaParaIr);
        }
        else
        {
            DialoguePanel.SetActive(false);
            Player.GetComponent<Personagem>().DesprenderPersonagem();
            Player.GetComponent<Personagem>().AcabouInteracao();
            if (!podeRepetir)
            {
                jaFoi = true;
            }
            this.enabled = false;
        }
    }

    public void MostrouImagem()
    {
        mostrandoImagem = false;
    }

}

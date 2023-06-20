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
    [SerializeField] private List<string> en_falantes = new List<string>();
    [SerializeField] private List<string> pt_falantes = new List<string>();
    [SerializeField] private List<Sprite> imagensFalantes = new List<Sprite>();

    [SerializeField] private List<int> pausaAntesDeFala = new List<int>();
    [SerializeField] private List<float> temposDasPausasAntesDasFalas = new List<float>();

    private int numeroFala = 0;
    private float tempo = 0.0f;
    private float tempoLetras = 0.0f;
    private int letra = 1;
    private bool falasRodando;
    private GameObject Player;

    private GameObject TelaPretaPanel;
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
    [SerializeField]
    private GameObject ondeOlhar;

    // Objetos específicos (gambiarra da gambiarra - fazendo manualmente)
    [SerializeField]
    private GameObject DragonEyes;
    [SerializeField]
    private GameObject Escama;
    [SerializeField]
    private GameObject PortaPassada;
    [SerializeField]
    private GameObject Porta;
    [SerializeField]
    private GameObject PortaControlador;
    [SerializeField]
    private GameObject Mago;
    [SerializeField]
    private GameObject Anao;
    [SerializeField]
    private Sprite Vassoura;
    [SerializeField]
    private Sprite Idle;
    [SerializeField]
    private GameObject Cogumelo;
    [SerializeField]
    private GameObject Fada;
    [SerializeField]
    private GameObject Pedra;
    [SerializeField]
    private GameObject Maca;
    [SerializeField]
    private GameObject Elixir1;
    [SerializeField]
    private GameObject Elixir2;

    // Start is called before the first frame update
    void Start()
    {
        numeroFala = 0;
        TelaPretaPanel = GameObject.FindGameObjectWithTag("TelaPretaPanel");
    }

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Personagem>().PrenderPersonagem();
        OlharParaAlgo();
        falaTexto.text = "";
        falasRodando = true;
        if (podeRepetir)
        {
            numeroFala = 0;
        }

        if (temImagemParaMostrar)
        {
            mostrandoImagem = true;
        }
        else
        {
            mostrandoImagem = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControleFalas();
    }

    void OlharParaAlgo()
    {
        if(ondeOlhar != null)
        {
            if (ondeOlhar.transform.position.x > Player.transform.position.x)
            {
                Player.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                Player.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void ScriptFalas()
    {
        List<string> falas = null;
        List<string> falantes = null;

        if (PlayerPrefs.GetInt("Lingua") == 0)
        {
            falas = pt_falas;
            falantes = pt_falantes;
        }
        else if (PlayerPrefs.GetInt("Lingua") == 1)
        {
            falas = en_falas;
            falantes = en_falantes;
        }

        ControleDosObjetosEspecificos(falas);

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
            if(GetComponent<IniciarFadesImagem>() != null)
            {
                GetComponent<IniciarFadesImagem>().enabled = true;
                if (!GetComponent<IniciarFadesImagem>().imagemParaFadePanel.activeSelf)
                {
                    mostrandoImagem = false;
                }
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

    void ControleDosObjetosEspecificos(List<string> falas)
    {
        if (falas[numeroFala] == "EI!" || falas[numeroFala] == "HEY!")
        {
            LigarObjetosEspecificos("Dragão");
        }
        else if (falas[numeroFala] == "Você adquiriu uma escama da Senhora Dragão." || falas[numeroFala] == "You have acquired a scale from Ms. Dragon.")
        {
            LigarObjetosEspecificos("Escama");
        }
        else if (falas[numeroFala] == "Tá vendo essa porta atrás de mim? Ela sempre levará você ao caminho que precisará seguir." || falas[numeroFala] == "Ya see that door behind me? It will always take you to the path you’ll need to follow.")
        {
            LigarObjetosEspecificos("Porta");
            Mago.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (falas[numeroFala] == "VOCÊ!" || falas[numeroFala] == "YOU!")
        {
            Mago.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(falas[numeroFala] == "Agora vá!" || falas[numeroFala] == "Now go!")
        {
            Mago.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (falas[numeroFala] == "Enfim! É só seguir pela mesma porta que você foi anteriormente." || falas[numeroFala] == "Anyways! Just go through the same door from before." || falas[numeroFala] == "Fique atento." || falas[numeroFala] == "Be aware.")
        {
            LigarObjetosEspecificos("PortaPassada");
            LigarObjetosEspecificos("Porta");
        }
        else if (falas[numeroFala] == "Vê se não demora!" || falas[numeroFala] == "Try not to take long!" || falas[numeroFala] == "Encontre o Grande Pé de Feijão." || falas[numeroFala] == "Find the Big Beanstalk.")
        {
            LigarObjetosEspecificos("Porta");
        }
        else if (falas[numeroFala] == "EI! VOCÊ AÍ!" || falas[numeroFala] == "HEY! YOU THERE!")
        {
            LigarObjetosEspecificos("Anão");
        }
        else if (falas[numeroFala] == "Peraí peraí! O que você PENSA que está fazendo?" || falas[numeroFala] == "Wait wait! What do you THINK you’re doing?")
        {
            LigarObjetosEspecificos("Fada");
        }
        else if (falas[numeroFala] == "Você adquiriu o Cogumelo Sagrado." || falas[numeroFala] == "You have acquired the Sacred Mushroom.")
        {
            LigarObjetosEspecificos("Cogumelo");
        }
        else if(falas[numeroFala] == "Isso!" || falas[numeroFala] == "This!")
        {
            LigarObjetosEspecificos("Vassoura1");
        }
        else if(falas[numeroFala] == "Ah, então você faz isso…" || falas[numeroFala] == "Ah, so you do that…")
        {
            LigarObjetosEspecificos("Vassoura2");
        }
        else if (falas[numeroFala] == "Você adquiriu uma pedra." || falas[numeroFala] == "You have acquired a rock.")
        {
            LigarObjetosEspecificos("Pedra");
        }
        else if (falas[numeroFala] == "Você adquiriu uma Maçã Dourada." || falas[numeroFala] == "You have acquired a Golden Apple.")
        {
            LigarObjetosEspecificos("Maca");
        }
        else if (falas[numeroFala] == "Mas antes, eu gostaria de fazer um discurs-" || falas[numeroFala] == "But before that, I’d like to give a speech-")
        {
            LigarObjetosEspecificos("Elixir1");
        }
        else if (falas[numeroFala] == "..........")
        {
            LigarObjetosEspecificos("Elixir2");
        }
        else if (falas[numeroFala] == "Deu certo?" || falas[numeroFala] == "Did it work?")
        {
            LigarObjetosEspecificos("DesligarElixir");
        }


    }

    void LigarObjetosEspecificos(string objeto)
    {
        if(objeto == "Dragão")
        {
            DragonEyes.SetActive(true);
        }

        if(objeto == "Escama")
        {
            Escama.SetActive(false);
        }

        if (objeto == "Porta")
        {
            Porta.SetActive(true);
            PortaControlador.SetActive(true);
        }

        if (objeto == "PortaPassada")
        {
            PortaPassada.SetActive(false);
        }

        if (objeto == "Cogumelo")
        {
            Cogumelo.SetActive(false);
        }
        
        if(objeto == "Vassoura1")
        {
            Player.GetComponent<SpriteRenderer>().sprite = Vassoura;
            Player.GetComponent<Animator>().SetBool("Vassoura", true);
        }
        if (objeto == "Vassoura2")
        {
            Player.GetComponent<SpriteRenderer>().sprite = Idle;
            Player.GetComponent<Animator>().SetBool("Vassoura", false);
        }

        if (objeto == "Fada")
        {
            Fada.SetActive(true);
        }

        if (objeto == "Anão")
        {
            Anao.SetActive(true);
        }

        if (objeto == "Pedra")
        {
            Pedra.SetActive(false);
        }

        if (objeto == "Maca")
        {
            Maca.SetActive(false);
        }

        if (objeto == "Elixir1")
        {
            Elixir1.SetActive(true);
        }

        if (objeto == "Elixir2")
        {
            Elixir1.SetActive(false);
            Elixir2.SetActive(true);

        }

        if (objeto == "DesligarElixir")
        {
            Elixir2.SetActive(false);
        }

    }

    void AcabouFalas()
    {
        if (teleportarDepois)
        {
            TelaPretaPanel.GetComponent<TelaPretaFade>().enabled = true;
            TelaPretaPanel.GetComponent<TelaPretaFade>().FadeIn(cenaParaIr);
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
            Player.GetComponent<Personagem>().CooldownPulo();
            this.enabled = false;
        }
    }

    public void MostrouImagem()
    {
        mostrandoImagem = false;
    }

}

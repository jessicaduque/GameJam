using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteracaoParaFalas : MonoBehaviour
{
    [SerializeField] private List<string> en_falas = new List<string>();
    [SerializeField] private List<string> pt_falas = new List<string>();
    [SerializeField] private List<int> falantesDasFalas = new List<int>();
    [SerializeField] private List<string> falantes = new List<string>();
    [SerializeField] private List<Sprite> imagensFalantes = new List<Sprite>();

    [SerializeField] private List<int> pausaAntesDeFala = new List<int>();
    [SerializeField] private List<float> temposDasPausasAntesDasFalas = new List<float>();

    private int numeroFala;
    private float tempo = 0.0f;
    private bool falasRodando;
    private GameObject Player;

    public GameObject DialoguePanel;
    public Text falaTexto;
    public Text NomeFalante_Text;
    public Image Falante_Image;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Personagem>().PrenderPersonagem();
        falaTexto.text = "";
        falasRodando = true;
        DialoguePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ControleFalas();
    }

    void ScriptFalas()
    {
        if(PlayerPrefs.GetInt("Lingua") == 0)
        {
            falaTexto.text = pt_falas[numeroFala];
        }
        else if (PlayerPrefs.GetInt("Lingua") == 1)
        {
            falaTexto.text = en_falas[numeroFala];
        }
        
        Falante_Image.sprite = imagensFalantes[falantesDasFalas[numeroFala]];
        NomeFalante_Text.text = falantes[falantesDasFalas[numeroFala]];

        // Falas
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (tempo > 0.4f)
            {
                if (numeroFala == 0)
                {
                    if (tempo > 1f)
                    {
                        tempo = 0.0f;
                        numeroFala++;
                    }
                }
                else
                {
                    tempo = 0.0f;
                    numeroFala++;
                }
            }
        }
    }

    void ControleFalas()
    {

        if (falasRodando)
        {
            tempo += Time.deltaTime;
        }

        if(numeroFala == pausaAntesDeFala[numeroFala])
        {
            if (tempo >= temposDasPausasAntesDasFalas[numeroFala])
            {
                ScriptFalas();
            }
            else
            {
                falaTexto.text = "";
            }
        }
        else if(numeroFala == pt_falas.Count)
        {
            AcabouFalas();
        }
        else
        {
            ScriptFalas();
        }
        /*
        if (numeroFala == 0 || numeroFala == 18)
        {
            Debug.Log(tempo);
            if (tempo >= 1f)
            {
                DialoguePanel.SetActive(true);
                ScriptFalas();
            }
        }
        else if (numeroFala == 17)
        {
            AcabouFalas();
        }
        else if (numeroFala == 21)
        {
            PlayerPrefs.SetInt("FASE2", 2);
            AcabouFalas();
        }
        else
        {
            ScriptFalas();
        }
        */
    }


    void AcabouFalas()
    {
        DialoguePanel.SetActive(false);
        Player.GetComponent<Personagem>().DesprenderPersonagem();
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private GameObject Player;
    private CanvasGroup canvasGroup;
    private  bool fadeIn = false;
    private bool fadeOut = false;
    [SerializeField]
    private int numParte;

    [SerializeField] float timeToFade;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        fadeOut = false;
    }

    void Update()
    {

        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    Player.GetComponent<Personagem>().DesprenderPersonagem();
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorCena>().fezAlgumaParte(numParte);
                    this.transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

}

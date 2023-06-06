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
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        fadeIn = false;
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
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GerenciadorCena>().fezAlgumaParte(numParte);
                    this.gameObject.SetActive(false);
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

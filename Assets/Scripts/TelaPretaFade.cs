using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPretaFade : MonoBehaviour
{
    private GameObject Player;
    private CanvasGroup canvasGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private int proxCena;


    [SerializeField] float timeToFade;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        canvasGroup = transform.GetComponentInChildren<CanvasGroup>();
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
                if (canvasGroup.alpha >= 1)
                {
                    SceneManager.LoadScene(proxCena);
                    fadeIn = false;
                }
            }
        }
        else
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    this.enabled = false;
                }
            }
        }
    }

    public void FadeIn(int cena)
    {
        fadeIn = true;
        proxCena = cena;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

}

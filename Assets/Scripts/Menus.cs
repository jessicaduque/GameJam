using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    private void Start()
    {
        // 0 para port, 1 para ing.
        PlayerPrefs.SetInt("Lingua", 0);
    }

    public void ComecarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void Recomeçar()
    {
        SceneManager.LoadScene(0);
    }

}

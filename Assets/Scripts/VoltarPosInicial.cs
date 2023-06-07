using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltarPosInicial : MonoBehaviour
{
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.transform.position = Player.GetComponent<Personagem>().PosInicial;
            Player.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeDeCura = 15;  
    private int tempoDeDestruicao = 5;

    private void Start()
    {
        Destroy(gameObject, tempoDeDestruicao);
    }

    private void OnTriggerEnter(Collider objetoColisor)
    {
        if (objetoColisor.tag == "Jogador")
        {
            objetoColisor.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
            Destroy(gameObject);
        }
    }
}

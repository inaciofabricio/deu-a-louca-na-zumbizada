using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbidy;

    private void Awake()
    {
        meuRigidbidy = GetComponent<Rigidbody>();
    }

    public void Movimentar (Vector3 direcao, float velocidade)
    {
        meuRigidbidy.MovePosition(meuRigidbidy.position + (direcao.normalized * velocidade * Time.deltaTime));
    }

    public void Rotacionar (Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbidy.MoveRotation(novaRotacao);
    }
}

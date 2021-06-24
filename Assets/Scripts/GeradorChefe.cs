using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesPossiveisDeGeracaoChefe;
    private Transform jogador;

    // Start is called before the first frame update
    void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.TextoAvisoChefeApareceu();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoMaisDistanteDoJogador = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform posicao in PosicoesPossiveisDeGeracaoChefe)
        {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaEntreJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreJogador;
                posicaoMaisDistanteDoJogador = posicao.position;
            }
        }

        return posicaoMaisDistanteDoJogador;
    }
}

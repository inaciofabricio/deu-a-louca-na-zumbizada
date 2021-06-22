using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorDeTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbisVivos = 2;
    private int quantidadeDeZumbiVivos;
    private float tempoProximoAumentoDeDificuldade = 15;
    private float contadorDeAumentoDificuldade;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorDeAumentoDificuldade = tempoProximoAumentoDeDificuldade;

        for(int i = 0; i < quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarUmNovoZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (possoGerarZumbisPelaDistancia == true && quantidadeDeZumbiVivos < quantidadeMaximaDeZumbisVivos)
        {
            contadorDeTempo += Time.deltaTime;

            if (contadorDeTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarUmNovoZumbi());
                contadorDeTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > contadorDeAumentoDificuldade)
        {
            quantidadeMaximaDeZumbisVivos++;
            contadorDeAumentoDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarUmNovoZumbi()
    {
        Vector3 posicaoCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);
            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.geradorDeZumbis = this;
        quantidadeDeZumbiVivos++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirquantidadeDeZumbiVivos()
    {
        quantidadeDeZumbiVivos--;
    }
}

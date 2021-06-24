using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{ 

    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGamerOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;
    public GameObject PocaDeSangue;

    private void Awake()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();

        //int geraTipoJogador = Random.Range(1, 23);
        //transform.GetChild(geraTipoJogador).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);
    }

    private void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);
        meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    private void OnTriggerEnter(Collider objetoColisor)
    {
        if (objetoColisor.tag == "Sangue")
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;

        if(statusJogador.Vida >= statusJogador.VidaInicial)
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }

        scriptControlaInterface.AtualizarSliderVidaJogador();

    }
}

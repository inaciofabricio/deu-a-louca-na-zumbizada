using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelGamerOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoTempoDeSobrevivenciaMaxima;
    private float tempoPontuacaoSalva;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalva = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        PainelGamerOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoTempoDeSobrevivencia.text = "Você sobrevivu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima();
    }

    void AjustarPontuacaoMaxima()
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalva)
        {
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalva);
        }
        atualizaPontuacaoMaxima();
    }

    void atualizaPontuacaoMaxima()
    {
        int minutos = (int)(tempoPontuacaoSalva / 60);
        int segundos = (int)(tempoPontuacaoSalva % 60);
        TextoTempoDeSobrevivenciaMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", minutos, segundos);
    }

    public void Reiniciar ()
    {
        SceneManager.LoadScene("game");
    }
}

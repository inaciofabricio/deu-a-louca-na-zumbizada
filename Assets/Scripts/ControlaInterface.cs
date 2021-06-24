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
    private int quantidadeDeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;
    public Text TextoAvisoChefe;
    public Text QuantidadeFinalDeZumbisMortos;
    public Text MelhorPontuacaoDeZumbisMortos;

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
        QuantidadeFinalDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);

        AjustarPontuacaoMaxima();
    }

    void AjustarPontuacaoMaxima()
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalva)
        {
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalva);
        }

        int zumbisMortos = PlayerPrefs.GetInt("zumbisMortos");
        if (quantidadeDeZumbisMortos > zumbisMortos)
        {
            PlayerPrefs.SetInt("zumbisMortos", quantidadeDeZumbisMortos);
        }

        atualizaPontuacaoMaxima();
    }

    void atualizaPontuacaoMaxima()
    {
        int minutos = (int)(tempoPontuacaoSalva / 60);
        int segundos = (int)(tempoPontuacaoSalva % 60);
        TextoTempoDeSobrevivenciaMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", minutos, segundos);
        MelhorPontuacaoDeZumbisMortos.text = string.Format("x {0}", PlayerPrefs.GetInt("zumbisMortos"));
    }

    public void Reiniciar ()
    {
        SceneManager.LoadScene("game");
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void AtualizarQuantidadeDeZumbisMortosChefe()
    {
        quantidadeDeZumbisMortos = quantidadeDeZumbisMortos + 10;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }

    public void TextoAvisoChefeApareceu()
    {
        StartCoroutine(DesaparecerTexto(2, TextoAvisoChefe));
    }

    IEnumerator DesaparecerTexto(float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;

        yield return new WaitForSeconds(1);

        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if (textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }

    }
}

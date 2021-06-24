using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoSair;

    private void Start()
    {
        #if UNITY_STANDEALONE || UNITY_EDITOR
            BotaoSair.SetActive(true);
        #endif
    }

    public void Jogar()
    {
        StartCoroutine(MudarCena("game"));
    }

    IEnumerator MudarCena(string name)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    public void Sair()
    {
        StartCoroutine(SairJogo());
    }

    IEnumerator SairJogo()
    {
        yield return new WaitForSeconds(0.3f);

        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

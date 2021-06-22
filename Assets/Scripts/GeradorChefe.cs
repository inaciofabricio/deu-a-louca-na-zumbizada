using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Instantiate(ChefePrefab, transform.position, Quaternion.identity);
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}

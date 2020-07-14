using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nomeInimigo : MonoBehaviour
{
    private Inimigo scriptInimigo;
    private TextMeshPro indicador;
    private string nomeDoInimigo;
    // Start is called before the first frame update
    void Start()
    {
        scriptInimigo = GetComponentInParent<Inimigo>();
        indicador = GetComponent<TextMeshPro>();
        

    }

    // Update is called once per frame
    void Update()
    {
        nomeDoInimigo = scriptInimigo.nome;
        indicador.text = nomeDoInimigo;
    }
}

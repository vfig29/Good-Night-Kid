using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mostrarTecla : MonoBehaviour
{
    public bool esquerda, direita, cima, baixo, interact, boost;
    private TextMeshProUGUI indicador;

    // Start is called before the first frame update
    void Start()
    {
        indicador = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cima == true)
        {
            indicador.text = Config.cima.ToString();
        }
        if (baixo == true)
        {
            indicador.text = Config.baixo.ToString();
        }
        if (esquerda == true)
        {
            indicador.text = Config.esquerda.ToString();
        }
        if (direita == true)
        {
            indicador.text = Config.direita.ToString();
        }
        if (interact == true)
        {
            indicador.text = Config.interact.ToString();
        }
        if (boost == true)
        {
            indicador.text = Config.boost.ToString();
        }
    }
}

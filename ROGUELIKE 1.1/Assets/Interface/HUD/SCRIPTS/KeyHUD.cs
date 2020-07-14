using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyHUD : MonoBehaviour
{
    private Atributos scriptAtributos;
    private int chaves;
    private TextMeshProUGUI indicador;

    // Start is called before the first frame update
    void Start()
    {
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        indicador = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        chaves = scriptAtributos.chaves;
        if (chaves < scriptAtributos.maximoDeChaves)
        {
            indicador.text = "x" + chaves;
        }
        else
        {
            indicador.text = "x" + scriptAtributos.maximoDeChaves;
        }
        if (chaves < 0)
        {
            indicador.text = "x" + 0;
        }




    }
}

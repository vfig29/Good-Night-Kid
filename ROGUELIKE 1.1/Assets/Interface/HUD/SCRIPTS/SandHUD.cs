using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SandHUD : MonoBehaviour
{
    private Atributos scriptAtributos;
    private int sand;
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
        sand = scriptAtributos.sand;
        if(sand < scriptAtributos.maximoDeSand)
        {
            indicador.text = "x" + sand;
        }
        else
        {
            indicador.text = "x" + scriptAtributos.maximoDeSand;
        }
        if (sand < 0)
        {
            indicador.text = "x" + 0;
        }




    }
}

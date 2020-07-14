using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudHP : MonoBehaviour
{
    private float hpAtual;
    private int coracoesHabilitados;
    public Image[] coracoesMAX;
    private int maximoDeHP;
    public Sprite cheio;
    public Sprite metade;
    public Sprite vazio;
    //
    private float escudoAtual;
    public Image[] escudoMAX;
    private int maximoDeEscudo;
    public Sprite escudocheio;
    public Sprite escudometade;
    //
    private Atributos scriptAtributos;

    void Start()
    {
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
    }

  
    void Update()
    {
        //Para os valores de maximoDeHP Ficarem inalteraveis no outro script, sempre com o valor do tamanho do vetor menos um.(o motivo de estar nesse script é utilizar o tamanho dos vetores como escudoMAX e coracoesMAX como referencia).
        scriptAtributos.maximoDeEscudo = escudoMAX.Length - 1;
        scriptAtributos.maximoDeHP = coracoesMAX.Length - 1;
        //
        hpAtual = scriptAtributos.hpAtual;
        escudoAtual = scriptAtributos.escudoAtual;
        coracoesHabilitados = scriptAtributos.coracoesHabilitados;










        for (int i = 0; i < coracoesMAX.Length; i++)
        {
            if (i < hpAtual)
            {
                coracoesMAX[i].sprite = cheio;
            }
            else if (i > hpAtual && hpAtual > i - 1)
            {
                coracoesMAX[i - 1].sprite = metade;
            }
            else
            {
                coracoesMAX[i].sprite = vazio;
            }

            if (i < coracoesHabilitados)
            {
                coracoesMAX[i].enabled = true;
            }
            else
            {
                coracoesMAX[i].enabled = false;
            }
        }
        //

        for (int i = 0; i < escudoMAX.Length; i++)
        {

            if (i < escudoAtual)
            {
                escudoMAX[i].sprite = escudocheio;
            }
            else if (i > escudoAtual && escudoAtual > i - 1)
            {
                escudoMAX[i - 1].sprite = escudometade;
            }

            if (i < escudoAtual)
            {
                escudoMAX[i].enabled = true;
            }
            else
            {
                escudoMAX[i].enabled = false;
            }

        }
    }
}

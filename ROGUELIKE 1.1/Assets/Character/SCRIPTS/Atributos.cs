using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos : MonoBehaviour
{
    [Header("Valores Fixos:")] //
    public int maximoDeEscudo;
    public int maximoDeHP;
    public float tempoEntreAtaques;
    public int maximoDeSand;
    public int maximoDeChaves;
    public bool interacting = false;
    [Header("Atributos Relacionados a Vida e Movimento:")] //
    public float hpAtual;
    public float escudoAtual;
    public int coracoesHabilitados;
    public float velocidadeDeMovimento;
    public float veloAux;
    public float boost;
    [Header("Atributos Relacionados a Ataque:")] //
    public float range;
    public float velocidadeDoProjetil;
    public float dano;
    public float knockBack;
    public float velocidadeDeAtaque;
    public int impactosDoProjetil;
    public bool isAttacking;
    public GameObject projetilAtaque;
    [Header("Atributos Relacionados a Itens:")] //
    public int idHabilidadeEquipada;
    public int[] idItensColetados;
    public int sand, chaves;
    public Zona zonaAtual;
    public Area areaAtual;

    void Start()
    {
        //
        //valores iniciais: 
        maximoDeEscudo = 20; // Evita bug, caso esse script seja carregado antes do HUDHP;
        maximoDeHP = 10; // Evita bug, caso esse script seja carregado antes do HUDHP;
        coracoesHabilitados = 3;
        hpAtual = 3f;
        escudoAtual = 0f;
        velocidadeDeMovimento = 10f;
        veloAux = velocidadeDeMovimento;
        velocidadeDeAtaque = 1f;
        velocidadeDoProjetil = 12f;
        impactosDoProjetil = 1;
        range = 6f;
        dano = 1f;
        knockBack = 1f;
        sand = 500;
        boost = 3f;
        chaves = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Config.interact))
        {
            interacting = true;

        }
        if (Input.GetKeyUp(Config.interact))
        {
            interacting = false;

        }
        //
        if (coracoesHabilitados > maximoDeHP)
        {
            coracoesHabilitados = maximoDeHP;
        }
        if (hpAtual > coracoesHabilitados)
        {
            hpAtual = coracoesHabilitados;
        }
        if (escudoAtual > maximoDeEscudo)
        {
            escudoAtual = maximoDeEscudo;
        }
        //Sistema de só poder ter escudo se tiver vida.
        if (escudoAtual > hpAtual)
        {
            escudoAtual = hpAtual;
        }

        //
        tempoEntreAtaques = 1 / (velocidadeDeAtaque);
        //
        //
        isAttacking = GetComponent<Ataque>().attack;

        if (isAttacking == true)
        {
            if (velocidadeDeMovimento == veloAux)
            {
                velocidadeDeMovimento = velocidadeDeMovimento * 50 / 100;
            }
        }
        else
        {
            velocidadeDeMovimento = veloAux;
        }

        //
        maximoDeSand = 999999; // Valor Fixo;
        if (sand > maximoDeSand)
        {
            sand = maximoDeSand;

        }
        if (sand < 0)
        {
            sand = 0;

        }
        //
        maximoDeChaves = 999999; // Valor Fixo;
        if (chaves > maximoDeChaves)
        {
            chaves = maximoDeSand;

        }
        if (chaves < 0)
        {
            chaves = 0;

        }

    }
}

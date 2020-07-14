using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    private float velocidade;
    private float tempoDeVida;
    private float dano;
    private float knockBack;
    private int impactosPossiveis;
    //
    private Atributos scriptAtributos;
    public ParticleSystem emissor;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {  
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        dano = scriptAtributos.dano;
        knockBack = scriptAtributos.knockBack;
        impactosPossiveis = scriptAtributos.impactosDoProjetil;
        
        velocidade = scriptAtributos.velocidadeDoProjetil;
        tempoDeVida = scriptAtributos.range / scriptAtributos.velocidadeDoProjetil;
        emissor.startLifetime = tempoDeVida;
        Invoke("DestruirProjetil", tempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        if (impactosPossiveis <= 0)
        {
            DestruirProjetil();
        }

        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }


    void DestruirProjetil()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);//(objeto com o efeito de destruicao)
        Particulas();
        Destroy(gameObject);
    }

    void Particulas()
    {
        if (emissor != null)
        {
            emissor.transform.parent = null;
        }


    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario") || collision.gameObject.CompareTag("Inimigo"))
        {
            print("Colidiu Projetil 1");
            DestruirProjetil();
        }
    }*/
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario") || collision.gameObject.CompareTag("Inimigo"))
        {
            print("Colidiu Projetil 2");
            DestruirProjetil();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario") || collision.gameObject.CompareTag("Inimigo"))
        {
            print("Colidiu Projetil 3");
            DestruirProjetil();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario") || collision.gameObject.CompareTag("Inimigo"))
        {
            print("Colidiu Projetil 3 trigger");
            DestruirProjetil();
        }
    }

    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cenario") || collision.gameObject.CompareTag("Entrada"))
        {
            print("Colidiu Projetil 2 trigger cenario");
            DestruirProjetil();
        }

        if (collision.gameObject.CompareTag("Inimigo"))
        {
            print("Colidiu Projetil 2 trigger inimigo");
            impactosPossiveis = impactosPossiveis - 1;
            //reduzir em 1 o numero de pancadas do projetil.
        }

    }

}

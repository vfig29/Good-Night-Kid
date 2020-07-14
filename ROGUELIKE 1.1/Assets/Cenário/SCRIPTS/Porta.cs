using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public bool temchave;
    public bool trancado;
    public bool principal = false;
    public int idSala;
    public Area areaPai;
    Atributos scriptAtributos;
    private SpriteRenderer renderizador;
    private Collider2D colisor;
    private Collider2D trigger;
    public Sprite comChave;
    public Sprite semChave;

    // Start is called before the first frame update
    void Start()
    {
        renderizador = GetComponent<SpriteRenderer>();
        colisor = GetComponent<BoxCollider2D>() as Collider2D;
        if(principal == true)
        {
            trigger = GetComponent<CircleCollider2D>() as Collider2D;
        }
        



    }

    // Update is called once per frame
    void Update()
    {
        if (scriptAtributos == null)
        {
            scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        }

        trancado = areaPai.dungeonInserida.salas[idSala].trancado;    



        if (trancado == true)
        {
            renderizador.sprite = comChave;
            colisor.enabled = true;
            if (principal == true)
            {
                trigger.enabled = true;
            }
            

        }
        if (trancado == false)
        {
            renderizador.sprite = semChave;
            colisor.enabled = false;
            if (principal == true)
            {
                trigger.enabled = false;
            }
            
        }

    }

    void OnTriggerStay2D(Collider2D objetoColidiu)
    {
        print(objetoColidiu);

        if (scriptAtributos.chaves > 0)
        {
            if (principal == true)
            {
                if (objetoColidiu.gameObject.tag == "Player")
                {
                    if (scriptAtributos.interacting == true)
                    {
                        areaPai.dungeonInserida.salas[idSala].trancado = !trancado;
                        scriptAtributos.chaves = scriptAtributos.chaves - 1;
                        scriptAtributos.interacting = false;

                    }
                }
            }
        }
        
        

    }

}

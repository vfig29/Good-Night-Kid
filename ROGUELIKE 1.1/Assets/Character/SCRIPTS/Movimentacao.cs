using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private Vector3 vel = Vector3.zero;
    private Animator playerAnimator;
    private bool walking;
    private bool colidindo = false;
    private float velocidadeInicial = 2;
    private float velocidadeAtual;
    private float velocidadeDeMovimento;
    float h, v;
    private Rigidbody2D corpo;
    //
    private Atributos scriptAtributos;
    private float intervalo = 0;

    void Start()
    {
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        walking = false;
        velocidadeAtual = velocidadeInicial;
        playerAnimator = GetComponent<Animator>();
        corpo = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (corpo.velocity == Vector2.zero)
        {
            //h = Input.GetAxisRaw("Horizontal");
            //v = Input.GetAxisRaw("Vertical");

            
            if (Input.GetKey(Config.cima))
            {
                transform.Translate(new Vector2(0, velocidadeAtual * Time.deltaTime));
                v = 1;
            }

            if (Input.GetKey(Config.baixo))
            {
                transform.Translate(new Vector2(0, -velocidadeAtual * Time.deltaTime));
                v = -1;
            }
            if (Input.GetKey(Config.direita))
            {
                transform.Translate(new Vector2(velocidadeAtual * Time.deltaTime, 0));
                h = 1;
            }
            if (Input.GetKey(Config.esquerda))
            {
                
                transform.Translate(new Vector2(-velocidadeAtual * Time.deltaTime, 0));
                h = -1;
            }

            if (!Input.GetKey(Config.esquerda) && !Input.GetKey(Config.direita))
            {
                h = 0;
            }
            if (!Input.GetKey(Config.cima) && !Input.GetKey(Config.baixo))
            {
                v = 0;
            }
            corpo.velocity = new Vector3(h / 20000, v / 20000, 0);
        }
        
    }

    void Update()
    {
        if (scriptAtributos.sand > 0)
        {
            if (Input.GetKey(Config.boost))
            {
                
                if (intervalo <= 0)
                {
                    scriptAtributos.sand = scriptAtributos.sand - 1;
                    
                    intervalo = 0.04f;//a cada 0.04 segundos desconta 1;
                }
                else
                {
                    intervalo = intervalo - Time.deltaTime;
                }
             velocidadeAtual = scriptAtributos.velocidadeDeMovimento + scriptAtributos.boost;
            }
        }



        if (corpo.velocity.x != 0.0f || corpo.velocity.y != 0.0f)
        {
            //h = Input.GetAxisRaw("Horizontal");
            //v = Input.GetAxisRaw("Vertical");
            
            if (Input.GetKey(Config.cima))
            {
                transform.Translate(new Vector2(0, velocidadeAtual * Time.deltaTime));
                v = 1;
            }

            if (Input.GetKey(Config.baixo))
            {
                transform.Translate(new Vector2(0, -velocidadeAtual * Time.deltaTime));
                v = -1;
            }
            if (Input.GetKey(Config.direita))
            {
                transform.Translate(new Vector2(velocidadeAtual * Time.deltaTime, 0));
                h = 1;
            }
            if (Input.GetKey(Config.esquerda))
            {
                transform.Translate(new Vector2(-velocidadeAtual * Time.deltaTime, 0));
                h = -1;
            }
            if (!Input.GetKey(Config.esquerda) && !Input.GetKey(Config.direita))
            {
                h = 0;
            }
            if (!Input.GetKey(Config.cima) && !Input.GetKey(Config.baixo))
            {
                v = 0;
            }
            corpo.velocity = new Vector3(h / 20000, v / 20000, 0);
        }
        



        velocidadeDeMovimento = scriptAtributos.velocidadeDeMovimento;

        

        
        //
        if (h != 0 || v != 0)
        {
            walking = true;
            //aceleração:
            velocidadeAtual = Vector3.SmoothDamp(new Vector3(velocidadeAtual, 0, 0), new Vector3(velocidadeDeMovimento, 0, 0), ref vel, 0.2f).x;
            //
        }
        else //else if (h == 0 && v == 0)
        {
            walking = false;
            velocidadeAtual = velocidadeInicial;
            //corpo.velocity = Vector3.zero;
        }

        playerAnimator.SetBool("walking", walking);

        //
        /*if ((corpo.velocity.x == 0 && corpo.velocity.y == 0))
        {
            velocidadeAtual = velocidadeInicial;
        }*/
        //
    }

    
}

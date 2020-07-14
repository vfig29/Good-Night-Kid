using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    private GameObject projetil;
    //
    public GameObject rightArm;
    private Animator rightArmAnimator;
    public GameObject leftArm;
    private Animator leftArmAnimator;
    private Animator kidAnimator;
    //
    public Transform maoDireita;
    public Transform maoEsquerda;
    public bool maoDaVez; //Uma mao começa em true e a outra em false
    public bool attack;
    private float tempoEntreAtaques;
    private float contadorEntreAtaques;
    //
    private Atributos scriptAtributos;

    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        kidAnimator = GetComponent<Animator>();
        //rightArmAnimator = rightArm.GetComponent<Animator>();
        //leftArmAnimator = leftArm.GetComponent<Animator>();
        maoDaVez = true;
        //mexi nisso:
        tempoEntreAtaques = scriptAtributos.tempoEntreAtaques;
        contadorEntreAtaques = tempoEntreAtaques;
    }

    // Update is called once per frame
    void Update()
    {
        tempoEntreAtaques = scriptAtributos.tempoEntreAtaques;
        projetil = scriptAtributos.projetilAtaque;
        //Ergue o braco:



        //Trigger ataque:
        //Se o contador for zero, autoriza o tiro.
        bool bracoErguido = rightArm.GetComponent<ArmPivot>().bracoErguido;
        if (contadorEntreAtaques <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //maoDaVez = !maoDaVez;
                /*
                if (maoDaVez == false)
                {
                    attack = true;
                    //contadorEntreAtaques = tempoEntreAtaques; // O contador retorna para o valor inicial, que é o tempo entre um ataque e outro.
                    //leftArmAnimator.SetBool("attack", attack);
                    //kidAnimator.SetBool("attack", attack);

                }
                if (maoDaVez == true)
                {
                    attack = true;
                    //contadorEntreAtaques = tempoEntreAtaques; // O contador retorna para o valor inicial, que é o tempo entre um ataque e outro.
                    //rightArmAnimator.SetBool("attack", attack);
                    //kidAnimator.SetBool("attack", attack);


                }
                */
                if (bracoErguido == true)
                {
                    spawnProjetil();
                    contadorEntreAtaques = tempoEntreAtaques;
                }

            }
        }
        else
        {
            //Reduz o contador, até que ele chegue em zero.
            contadorEntreAtaques = contadorEntreAtaques - Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            attack = false;
            contadorEntreAtaques = tempoEntreAtaques;
        }
        
    }

    public void spawnProjetil()
    {
        if (maoDaVez == false)
        {
            // a posicao do gatilho é a posicao do gameobject no qual esse script está acoplado. (poderia ter usado transform.position abaixo, também ja que esse script está no gatilho.).
            Instantiate(projetil, maoEsquerda.position, maoEsquerda.rotation); // Instancia o projetil, ele é lancado na posicao do gatilho, e na rotacao do eixo do gatilho.
            
        }
        if (maoDaVez == true)
        {
            // a posicao do gatilho é a posicao do gameobject no qual esse script está acoplado. (poderia ter usado transform.position abaixo, também ja que esse script está no gatilho.).
            Instantiate(projetil, maoDireita.position, maoDireita.rotation); // Instancia o projetil, ele é lancado na posicao do gatilho, e na rotacao do eixo do gatilho.
        }
    }
    public void AtaqueFalso()
    {
        attack = false;
        //rightArmAnimator.SetBool("attack", attack);
        //leftArmAnimator.SetBool("attack", attack);
        //kidAnimator.SetBool("attack", attack);
    }
}

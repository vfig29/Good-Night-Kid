using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPivot : MonoBehaviour
{

    public Transform player; //Object do Player.
    public float margemErro;
    public bool bracoErguido = false;
    // Start is called before the first frame update
    public float tempoDaAnimacao = 0.05f;
    public float temporizadorAnimacao;

    public GameObject sandEmissor;
    ParticleSystem emissor;

    void Start()
    {
        temporizadorAnimacao = tempoDaAnimacao;
        emissor = sandEmissor.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool attack = player.gameObject.GetComponent<Atributos>().isAttacking;
        if (attack == true)
        {
            if (player.localScale.x < 0)
            {
                margemErro = 115f;
            }
            else
            {
                margemErro = 64f;
            }

            Vector3 direcao = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotacaoZ = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotacaoZ + margemErro);
            sandEmissor.SetActive(true);
            //LigarEmissao();
            //
            if (temporizadorAnimacao < 0)
            {
                bracoErguido = true;
                temporizadorAnimacao = tempoDaAnimacao;
            }
            else
            {
                temporizadorAnimacao = temporizadorAnimacao - Time.deltaTime;
            }
            
        }
        else
        {
            //emissor de areia:
            //DesligarEmissao();
            sandEmissor.SetActive(false);


            //
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            print(transform.rotation);
            bracoErguido = false;

        }


    }

    public void LigarEmissao()
    {
        emissor.transform.parent = this.transform;
        emissor.emissionRate = 10;
        emissor.enableEmission = true;
    }

    public void DesligarEmissao()
    {
        emissor.transform.parent = null;
        emissor.emissionRate = 0;
        emissor.enableEmission = false;

    }


}

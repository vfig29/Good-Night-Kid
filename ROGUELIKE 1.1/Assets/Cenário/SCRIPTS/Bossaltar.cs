using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossaltar : MonoBehaviour
{
    public GameObject[] destinos;
    public GameObject objetoDestino;
    public Bossaltar scriptBossaltar;
    public bool entrada;
    public bool saida;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (objetoDestino == null)
        {

            destinos = GameObject.FindGameObjectsWithTag("altar");
            foreach (GameObject destino in destinos)
            {

                scriptBossaltar = destino.GetComponent<Bossaltar>();
                if (scriptBossaltar.entrada == true && this.gameObject != destino)
                {
                    objetoDestino = destino;

                }
                else if (scriptBossaltar.saida == true && this.gameObject != destino)
                {
                    objetoDestino = destino;

                }


            }
        }

    }
    //
    void OnTriggerStay2D(Collider2D objetoColidiu)
    {
        Atributos scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        if (objetoColidiu.gameObject.tag == "Player")
        {
            if (scriptAtributos.interacting == true)
            {
                objetoColidiu.transform.position = objetoDestino.transform.position + (new Vector3(0, 0, 0));
                Camera.main.transform.position = objetoColidiu.transform.position;
                scriptAtributos.interacting = false;
            }


        }

    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrada : MonoBehaviour
{
    public Zona zonaAtual;
    public Vetor2 areaOrigem;
    public Vetor2 areaDestino;
    public GameObject[] entradas;
    public GameObject entradaDestino;
    public Collider2D gatilhoPorta;
    private bool primeiraColisao = true;
    public Entrada scriptEntrada;
    public Atributos scriptAtributos;

    // Start is called before the first frame update
    void Start()
    {
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
    }

    // Update is called once per frame
    void Update()
    {
        zonaAtual = scriptAtributos.zonaAtual;

        //print("Origem:" + areaOrigem.x + "/" + areaOrigem.y + "Destino:" + areaDestino.x + "/" + areaDestino.y);
        if (entradaDestino == null)
        {

            entradas = GameObject.FindGameObjectsWithTag("Entrada");
            foreach (GameObject entrada in entradas)
            {

                scriptEntrada = entrada.GetComponent<Entrada>();
                if((scriptEntrada.areaDestino.x == this.areaOrigem.x && scriptEntrada.areaDestino.y == this.areaOrigem.y) && (scriptEntrada.areaOrigem.x == this.areaDestino.x && scriptEntrada.areaOrigem.y == this.areaDestino.y))
                {
                    entradaDestino = entrada;

                }

                
            }
        }



    }

    void OnTriggerEnter2D(Collider2D objetoColidiu)
    {
        if(objetoColidiu.gameObject.tag == "Player")
        {
            objetoColidiu.transform.position = entradaDestino.transform.position + (new Vector3(0,0,0));
            scriptAtributos.areaAtual = zonaAtual.zona[areaDestino.x, areaDestino.y];
            Camera.main.transform.position = objetoColidiu.transform.position;

        }

    }
}

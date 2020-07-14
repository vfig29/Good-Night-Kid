using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public GameObject salaPrefab;
    public GameObject[,] gridMap = new GameObject[Config.tamanhoMatrizZona.x, Config.tamanhoMatrizZona.y];
    public Canvas canvasHUD;
    public Zona zonaAtual;
    public Area areaAtual;
    //public Grafico scriptGrafico;
    public Salamap scriptSalamap;
    public Atributos scriptAtributos;
    public Area area;
    bool criado = false;
    public int maiorx = 0, maiory = 0, menorx = Config.tamanhoMatrizZona.x, menory = Config.tamanhoMatrizZona.y;
    float contadorPiscador;

    // Start is called before the first frame update
    void Start()
    {
        //scriptGrafico = FindObjectOfType(typeof(Grafico)) as Grafico;
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        zonaAtual = scriptAtributos.zonaAtual;     
        contadorPiscador = 0.8f;


    }

        
        

    // O controlador de visibilidade está no script pauseControlador
    void Update()
    {
        //scriptGrafico = FindObjectOfType(typeof(Grafico)) as Grafico;
        scriptAtributos = FindObjectOfType(typeof(Atributos)) as Atributos;
        zonaAtual = scriptAtributos.zonaAtual;
        areaAtual = scriptAtributos.areaAtual;
        Vector2 proporcaoTela = new Vector2(Screen.width*10/144, Screen.height/9)/100;
        Vector3 coordenadaInicial = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.75f, Screen.height * 0.75f));

        if(criado == false)
        {
            for (int i = 0; i < Config.tamanhoMatrizZona.x; i++)
            {
                for (int j = 0; j < Config.tamanhoMatrizZona.y; j++)
                {
                    if (zonaAtual.zona[i, j] != null)
                    {
                        if (maiorx < zonaAtual.zona[i, j].posicaoMatriz.x)
                        {
                            maiorx = zonaAtual.zona[i, j].posicaoMatriz.x;
                        }
                        if (maiory < zonaAtual.zona[i, j].posicaoMatriz.y)
                        {
                            maiory = zonaAtual.zona[i, j].posicaoMatriz.y;
                        }
                        if(menorx > zonaAtual.zona[i, j].posicaoMatriz.x)
                        {
                            menorx = zonaAtual.zona[i, j].posicaoMatriz.x;
                        }
                        if (menory > zonaAtual.zona[i, j].posicaoMatriz.y)
                        {
                            menory = zonaAtual.zona[i, j].posicaoMatriz.y;
                        }
                    }

                }

            }


            for (int i = 0; i < Config.tamanhoMatrizZona.x; i++)
            {
                for (int j = 0; j < Config.tamanhoMatrizZona.y; j++)
                {
                    Vector2 scale = new Vector2(canvasHUD.pixelRect.width / Screen.width, canvasHUD.pixelRect.height / Screen.height);
                    gridMap[i, j] = Instantiate(salaPrefab, new Vector3((900 - ( maiorx - 4.2f*(Config.tamanhoMatrizZona.x/2)))*proporcaoTela.x + (43*proporcaoTela.x * i), (1050 + (menory - 4.2f*(Config.tamanhoMatrizZona.y / 2)))*proporcaoTela.y - (40 * proporcaoTela.y * j), 0), Quaternion.identity, transform) as GameObject;
                    RectTransform rectTransform = gridMap[i, j].GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = Vector2.Scale(new Vector2((-210 - maiorx - 12.0f * (Config.tamanhoMatrizZona.x / 2)) + (42f * i), (400 + menory - 12.0f * (Config.tamanhoMatrizZona.y / 2)) - (40f * j)), scale);

                }

            }
            criado = true;
        }

        for (int i = 0; i < Config.tamanhoMatrizZona.x; i++)
        {
            for (int j = 0; j < Config.tamanhoMatrizZona.y; j++)
            {
                if (zonaAtual.zona[i, j] != null)
                {


                }

            }

        }




        for (int i = 0; i < Config.tamanhoMatrizZona.x; i++)
        {
            for (int j = 0; j < Config.tamanhoMatrizZona.y; j++)
            {
                scriptSalamap = gridMap[i, j].GetComponent<Salamap>();

                if (zonaAtual.zona[i, j] != null)
                {

                    if (scriptSalamap != null)
                    {
                        if (zonaAtual.zona[i, j].entradas[0] == null || zonaAtual.zona[i, j].secreta == true)
                        {
                            scriptSalamap.esquerda.enabled = false;
                        }
                        if (zonaAtual.zona[i, j].entradas[1] == null || zonaAtual.zona[i, j].secreta == true)
                        {
                            scriptSalamap.direita.enabled = false;
                        }
                        if (zonaAtual.zona[i, j].entradas[2] == null || zonaAtual.zona[i, j].secreta == true)
                        {
                            scriptSalamap.cima.enabled = false;
                        }
                        if (zonaAtual.zona[i, j].entradas[3] == null || zonaAtual.zona[i, j].secreta == true)
                        {
                            scriptSalamap.baixo.enabled = false;
                        }
                        if (zonaAtual.zona[i, j].inicial == true)
                        {
                            scriptSalamap.chao.GetComponent<Image>().color = new Color32(0, 226, 226, 125);

                        }
                        if (zonaAtual.zona[i, j].final == true)
                        {
                            scriptSalamap.chao.GetComponent<Image>().color = new Color32(253, 106, 0, 125);

                        }
                        if (zonaAtual.zona[i, j].secreta == true)
                        {
                            scriptSalamap.chao.GetComponent<Image>().color = new Color32(0, 0, 0, 125);

                        }

                    }
                    if (scriptAtributos != null)
                    {
                        if (scriptAtributos.areaAtual != zonaAtual.zona[i, j])
                        {
                            scriptSalamap.atual.enabled = false;
                        }
                        else
                        {
                            //caso eu mude de ideia, é só deletar o if e o else, e a declaração de contadorPiscador;
                            if (contadorPiscador < 0)
                            {
                                scriptSalamap.atual.enabled = !(scriptSalamap.atual.enabled);
                                contadorPiscador = 0.8f;
                            }
                            else
                            {
                                contadorPiscador = contadorPiscador - Time.deltaTime;

                            }
                            //scriptSalamap.atual.enabled = true;
                            zonaAtual.zona[i, j].descoberta = true;
                        }
                    }
                    if(zonaAtual.zona[i, j].descoberta == true)
                    {
                        gridMap[i, j].SetActive(true);
                    }
                    else
                    {
                        gridMap[i, j].SetActive(false);
                    }
                    

                }
                else
                {
                    gridMap[i, j].SetActive(false);
                }
            }
        }


        //

    }
}

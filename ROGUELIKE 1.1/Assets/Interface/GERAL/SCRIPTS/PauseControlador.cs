using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControlador : MonoBehaviour
{
    public GameObject telaDePause;
    public bool pausado = false;
    public GameObject objectMiniMap;
    private bool miniMapVisible = true;
    //private GameObject jogador;
    //private Ataque scriptAtaque;

    
    void Start()
    {
        //jogador = GameObject.Find("MainCharacter");
        //scriptAtaque = jogador.GetComponent<Ataque>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Despausar();
        }
        //

        if (pausado == true)
        {
            telaDePause.SetActive(true);
            Time.timeScale = 0f;
            //scriptAtaque.enabled = false;

        }
        if (pausado == false)
        {
            telaDePause.SetActive(false);
            Time.timeScale = 1f;
            //scriptAtaque.enabled = true;
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            objectMiniMap.SetActive(!miniMapVisible);
            miniMapVisible = !miniMapVisible;
        }
    }

    public void Despausar()
    {
        pausado = !pausado;
    }
}

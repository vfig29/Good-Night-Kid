using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class mudarTecla : MonoBehaviour
{
    private int teclaMudando = 0;
    private bool shift = false;
    private KeyCode shiftKeycode;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void MudarTeclaCima()
    {
        Config.cima = KeyCode.None;

        teclaMudando = 1;


    }
    public void MudarTeclaBaixo()
    {
        Config.baixo = KeyCode.None;

        teclaMudando = 2;


    }
    public void MudarTeclaEsquerda()
    {

        Config.esquerda = KeyCode.None;

        teclaMudando = 3;


    }
    public void MudarTeclaDireita()
    {
        Config.direita = KeyCode.None;

        teclaMudando = 4;


    }
    public void MudarTeclaBoost()
    {
        Config.boost = KeyCode.None;

        teclaMudando = 5;


    }
    public void MudarTeclaInteract()
    {
        Config.interact = KeyCode.None;

        teclaMudando = 6;


    }

    public void setarTeclasDefault()
    {
        Config.cima = KeyCode.W;
        Config.baixo = KeyCode.S;
        Config.esquerda = KeyCode.A;
        Config.direita = KeyCode.D;
        Config.boost = KeyCode.LeftShift;
        Config.interact = KeyCode.E;
        teclaMudando = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shift = true;
            shiftKeycode = KeyCode.LeftShift;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            shift = true;
            shiftKeycode = KeyCode.RightShift;
        }
    }
    void OnGUI()
    {

        Event e = Event.current;

        if (e.isKey)
        {
            if (e.keyCode.ToString() != "None")
            {
                
                //string key = e.keyCode.ToString();
                //Debug.Log(key);
                if (teclaMudando == 3)
                {
                    Config.esquerda = e.keyCode;
                    teclaMudando = 0;
                }
                if (teclaMudando == 4)
                {
                    Config.direita = e.keyCode;
                    teclaMudando = 0;
                }
                if (teclaMudando == 1)
                {
                    Config.cima = e.keyCode;
                    teclaMudando = 0;
                }
                if (teclaMudando == 2)
                {
                    Config.baixo = e.keyCode;
                    teclaMudando = 0;
                }
                if (teclaMudando == 5)
                {
                    Config.boost = e.keyCode;
                    teclaMudando = 0;
                }
                if (teclaMudando == 6)
                {
                    Config.interact = e.keyCode;
                    teclaMudando = 0;
                }

            }

        }

        if (shift == true)
        {

            if (teclaMudando == 3)
            {
                Config.esquerda = shiftKeycode;
                teclaMudando = 0;
                shift = false;
            }
            if (teclaMudando == 4)
            {
                Config.direita = shiftKeycode;
                teclaMudando = 0;
                shift = false;
            }
            if (teclaMudando == 1)
            {
                Config.cima = shiftKeycode;
                teclaMudando = 0;
                shift = false;
            }
            if (teclaMudando == 2)
            {
                Config.baixo = shiftKeycode;
                teclaMudando = 0;
                shift = false;
            }
            if (teclaMudando == 5)
            {
                Config.boost = shiftKeycode;
                teclaMudando = 0;
                shift = false;
            }
            if (teclaMudando == 6)
            {
                Config.interact = shiftKeycode;
                teclaMudando = 0;
                shift = false;
            }

        }
        
    }





}

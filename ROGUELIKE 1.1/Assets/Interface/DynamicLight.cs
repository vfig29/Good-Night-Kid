using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DynamicLight : MonoBehaviour
{
    public Light light;
    public GameObject player;
    public bool permit;
    public bool permitRnd;
    public float cd;
    Vector3 corPretendida;
    Vector3 corAtual ;
    Vector3 vel = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        permit = false;
        permitRnd = false;
        cd = 10;
        light = GetComponent<Light>();
        corPretendida = new Vector3(253, 235, 180); //fase 1
        corAtual = new Vector3(255, 255, 255);



    }

    // Update is called once per frame
    void Update()
    {

        

        int mudaCor = 0;
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        if (permitRnd == true)
        {
            mudaCor = rnd.Next(1, 10000);
        }
        else
        {
            if (cd <= 0)
            {
                permitRnd = true;
            }
            else
            {
                cd = cd - Time.deltaTime;
            }
        }
        


        if (mudaCor == 5)
        {
            permitRnd = false;
            permit = true;
            cd = 10;
            
        }
        
        if (permit == true)
        {
            int R = rnd.Next(0, 255);
            int G = rnd.Next(0, 255);
            int B = rnd.Next(0, 255);
            corPretendida = new Vector3(R, G, B);
            permit = false;
        }

        corAtual = Vector3.SmoothDamp(corAtual, corPretendida, ref vel, 10.0f);
        //light.intensity
        light.color = new Color32(Convert.ToByte(corAtual.x) , Convert.ToByte(corAtual.y), Convert.ToByte(corAtual.z), 255);

    }
}

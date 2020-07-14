using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmissor : MonoBehaviour
{
    public float contador;
    // Start is called before the first frame update
    void Start()
    {
        contador = GetComponent<ParticleSystem>().startDelay + 1;
    }

    // Update is called once per frame
    void Update()
    {
        print(GetComponent<ParticleSystem>().IsAlive());
        if (contador < 0)
        {
            if (GetComponent<ParticleSystem>().IsAlive() == false)
            {
                Destroy(gameObject);
            }
            contador = GetComponent<ParticleSystem>().startDelay + 1;
        }
        else
        {
            contador = contador - Time.deltaTime;
        }
    }
}

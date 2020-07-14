using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
    public float margemErro;
    public Quaternion debug;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mira:
        Vector3 direcao = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotacaoZ = Mathf.Atan2( direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler( 0f, 0f, rotacaoZ + margemErro);
        debug = transform.rotation;
                      

    }
}

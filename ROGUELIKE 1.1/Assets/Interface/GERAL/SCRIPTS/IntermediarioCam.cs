using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediarioCam : MonoBehaviour
{

    CameraControle scriptCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptCamera == null)
        {
            scriptCamera = Camera.main.gameObject.GetComponent<CameraControle>();
        }
        


    }
    //em caso de bug nos trigger, tirar as chaves do if, deixar apenas a linha unica abaixo da condicao(como estava antes).
    private void OnTriggerEnter2D(Collider2D cameraBounds)
    {
        if (scriptCamera != null)
        {
            if (cameraBounds.gameObject.tag == "cameraBound")
            {
                scriptCamera.cameraBounds = cameraBounds;
            }
        }
        
        
    }
    private void OnTriggerStay2D(Collider2D cameraBounds)
    {
        if (scriptCamera != null)
        {
            if (cameraBounds.gameObject.tag == "cameraBound")
            {
                scriptCamera.cameraBounds = cameraBounds;
            }
        }
    
    }
}

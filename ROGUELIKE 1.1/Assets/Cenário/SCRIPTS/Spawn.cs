using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    GameObject KID;
    // Start is called before the first frame update
    void Start()
    {
        KID = GameObject.Find("KID");
        KID.transform.position = transform.position;
        Camera.main.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checagembounds : MonoBehaviour
{
    public Vector3 min, max;
    Collider2D bounds;
    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        min = bounds.bounds.min;
        max = bounds.bounds.max;
    }
}

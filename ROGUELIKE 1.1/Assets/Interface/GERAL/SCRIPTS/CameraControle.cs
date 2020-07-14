using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour
{
    public GameObject player;       //Variavel do gameObject do personagem principal.


    private Vector3 offset;         //Distancia offset entre o personagem principal e a camera
    private float smoothTime = 0.3f; // Original: 0.3f
    private Vector3 velocity = Vector3.zero;
    //
    private Vector2 margin = new Vector2(1, 1);
    //public Vector2 smoothing = new Vector2(3, 3);

    public Collider2D cameraBounds;

    public Vector3 min, max;
    bool isFollowing;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        isFollowing = true;
    }

    
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (cameraBounds!= null)
        {
            min = cameraBounds.bounds.min;
            max = cameraBounds.bounds.max;

            var x = transform.position.x;
            var y = transform.position.y;

            Vector3 posicaoDesejada = player.transform.position + offset;

            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, posicaoDesejada, ref velocity, smoothTime);
            




            var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);

            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, min.y + Camera.main.orthographicSize, max.y - Camera.main.orthographicSize);

            transform.position = smoothedPosition;

        }
        
    }


}



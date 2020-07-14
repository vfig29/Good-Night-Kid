using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlharMouse : MonoBehaviour
{
    private Vector3 direcao;
    private Animator playerAnimator;
    private bool mouseup, mousedown;
    private int lookSide;
    
    void Start()
    {
        mouseup = true;
        mousedown = false;
        playerAnimator = GetComponent<Animator>();
        lookSide = -1;
    }

    // Update is called once per frame
    void Update()
    {        
        direcao = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        


        if (direcao.x >= 0 && lookSide == -1)
        {
            flip();
        }
        else if (direcao.x < 0 && lookSide == 1)
        {
            flip();
        }
        if (direcao.y > 0)
        {
            mouseup = true;
            mousedown = false;
        }
        if (direcao.y == 0)
        {
            mouseup = !mouseup;
            mousedown = !mousedown;
        }

        else if (direcao.y < 0)
        {
            mouseup = false;
            mousedown = true;
        }

        //playerAnimator.SetBool("mouseup", mouseup);
        //playerAnimator.SetBool("mousedown", mousedown);
    }

    public void flip()
    {
        lookSide = lookSide * -1;
        float x = transform.localScale.x;
        x = x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

    }
}

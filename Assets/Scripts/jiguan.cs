using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiguan : MonoBehaviour
{
    public GameObject men;

    public float fi;
    public bool isON=false;

    void Start()
    {
        fi = men.transform.position.y;
    }


    void FixedUpdate()
    {
        if(isON)
        {
            if (men.transform.position.y <= fi + 8.0f)
            {
                men.transform.position += Vector3.up * Time.deltaTime * 1f;
            }
            else
            {
                isON = false;
            }
        }
        else
        {
            if (men.transform.position.y >= fi)
            {
                men.transform.position -= Vector3.up * Time.deltaTime * 2f;
            }
        }
    }


    private void OnCollisionEnter()
    {
        isON = true;
    }
}

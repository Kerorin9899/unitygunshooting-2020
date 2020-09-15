using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curosormoves : MonoBehaviour
{
    private Vector3 m_pos;
    private int w;
    private int h;

    public float RapidCursor = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        w = Screen.width;
        h = Screen.height;

        //Debug.Log(new Vector2(w, h));
    }

    // Update is called once per frame
    void Update()
    {
        /* Mouse Mode (Debug) */
        m_pos = Input.mousePosition;

        var vec = m_pos - transform.position;
        vec /= 150.0f / RapidCursor;

        //transform.position += vec;
        transform.position = m_pos;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, w), Mathf.Clamp(transform.position.y, 0, h),0);
        

        transform.Rotate(0,0,Time.deltaTime * 200 * RapidCursor);

        /* JoyCon Mode */ 

    }
}

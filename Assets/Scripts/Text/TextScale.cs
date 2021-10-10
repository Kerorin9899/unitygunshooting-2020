using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScale : MonoBehaviour
{
    private float scale_bias = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.localScale.x > 1.0f)
        {
            float sc = scale_bias * Time.deltaTime;
            transform.localScale -= new Vector3(sc, sc, sc);
        }
    }
}

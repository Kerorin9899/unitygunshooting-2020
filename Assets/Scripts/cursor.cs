using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursor : MonoBehaviour
{
    private GameObject cursor1;

    // Start is called before the first frame update
    void Start()
    {
        cursor1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    { 
        cursor1.transform.Rotate(new Vector3(0,0,Time.deltaTime * 100));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRib : MonoBehaviour
{
    public GameObject silinder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Spin()
    {
        silinder.transform.Rotate(new Vector3(0, 0, -60));
    }
}

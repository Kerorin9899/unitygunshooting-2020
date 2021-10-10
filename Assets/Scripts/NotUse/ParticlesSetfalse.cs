using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSetfalse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnBecameVisible()
    {
        Invoke("Setfalse",1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Setfalse()
    {
        gameObject.SetActive(false);
    }
}

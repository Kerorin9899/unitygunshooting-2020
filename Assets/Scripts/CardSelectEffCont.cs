using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectEffCont : MonoBehaviour
{
    private float time = 0.25f;
    private void OnBecameVisible()
    {
        time = 0.25f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;

            if(time <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

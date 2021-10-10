using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCard : MonoBehaviour
{
    public bool Isout;

    private Vector3 discardpos;

    private Vector3 norm;

    private void OnBecameVisible()
    {
        Isout = false;
        norm = (discardpos - transform.position).normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        discardpos = new Vector3(-20, 0.65f, -6);
    }

    // Update is called once per frame
    void Update()
    {
        if(Isout)
        {
            Discard();
        }
    }

    void Discard()
    {
        transform.position += norm * Time.deltaTime * 5;

        if(discardpos.x > transform.position.x)
        {
            transform.position = discardpos;

            gameObject.SetActive(false);
        }
    }
}

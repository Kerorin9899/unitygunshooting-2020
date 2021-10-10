using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealcards : MonoBehaviour
{
    private CardControl CCont;

    [SerializeField]
    private AudioClip CarddeelSound;

    private AudioSource soruce;

    public Vector3 arrivepos;
    public Vector3 vec;
    private float timeLeft = 1.0f;
    private float vec_bias = 2.0f;

    private bool arriveflag = false;

    private void Awake()
    {
        CCont = GetComponent<CardControl>();
        arrivepos = new Vector3(0, 0, 0);
        vec = new Vector3(0,0,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        soruce = GetComponent<AudioSource>();
    }

    private void OnBecameVisible()
    {
        timeLeft = 0.4f;
        arriveflag = false;
        soruce.PlayOneShot(CarddeelSound, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CCont.Isdeeling)
        {
            deeling();

            if (arriveflag)
            {
                gameObject.transform.position = arrivepos;
                timeLeft = 0;
                CCont.Isdeeling = false;
                CCont.CC.Isreadynum++;
            }
        }
    }

    private void deeling()
    {
        Vector3 dis = arrivepos - gameObject.transform.position;

        //Debug.Log(dis);

        var acc = Vector3.zero;

        acc += 2 * (dis - vec * timeLeft) / (timeLeft * timeLeft);

        timeLeft -= Time.deltaTime;


        if(timeLeft < 0)
        {
            Vector3 tmp = transform.position + vec * Time.deltaTime;
            float afterdisz = arrivepos.z - tmp.z;

            transform.position = arrivepos;
            arriveflag = true;
        }
            //if(afterdisz >= 0)
            //{
            //    transform.position = arrivepos;
            //    arriveflag = true;
            //}
            //else
            //{
            //    gameObject.transform.position = tmp;
            //}

        else 
        {
            vec += acc * Time.deltaTime;
            gameObject.transform.position += vec * Time.deltaTime;

            Vector3 tmp = transform.position + vec * Time.deltaTime;
            float afterdisz = arrivepos.z - tmp.z;

            if (afterdisz >= 0)
            {
                transform.position = arrivepos;
                arriveflag = true;
            }
        }

    }

    public void InstanceValue(Vector3 pos)
    {
        //Debug.Log("Thorow");

        arrivepos = pos;

        vec = (pos - gameObject.transform.position) * vec_bias;

        //Debug.Log(arrivepos + "\n" + vec);
    }
}

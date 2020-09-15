using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public int cardIndex;
    public int BackIndex;

    [SerializeField]
    private Sprite[] face;
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite BonusBack;

    private GameObject[] childrenobj = new GameObject[3];

    float fliptime = 0;

    private bool flag = false;

    public bool showface = false;

    public bool flipflag = false;
    private bool backflipflag = false;

    private void OnBecameVisible()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = true;
        gameObject.transform.localEulerAngles = new Vector3(0,180,0);
    }

    private void OnBecameInvisible()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        for(int i = 0;i < 3;i++)
        {
            childrenobj[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        int a = (int)(transform.localEulerAngles.y / 90);
        if(a == 1 || a == 2)
        {
            spriteRenderer.sprite = face[cardIndex];
        }
        else
        {
            if (BackIndex == 0)
            {
                spriteRenderer.sprite = cardBack;
            }
            else
            {
                spriteRenderer.sprite = BonusBack;
            }
        }*/
       

        if (flipflag)
        {
            transform.Rotate(0, Time.deltaTime * 1000, 0);
            if (backflipflag)
            {
                if (transform.localEulerAngles.y > 180)
                {
                    Vector3 p = transform.localEulerAngles;
                    p.y = 180;
                    transform.localEulerAngles = p;
                    backflipflag = false;
                    flipflag = false;
                }
            }
            else
            {
                if (transform.localEulerAngles.y < 180)
                {
                    Vector3 p = transform.localEulerAngles;
                    p.y = 0;
                    transform.localEulerAngles = p;
                    backflipflag = true;
                    flipflag = false;
                }
            }
        }

        Vector3 n_vec;
        Vector3 v1, v2;

        v1 = childrenobj[1].transform.position - childrenobj[0].transform.position;
        v2 = childrenobj[2].transform.position - childrenobj[0].transform.position;

        n_vec = CalcOuter(v1,v2);
        //Debug.Log(childrenobj[0].transform.position);
        Vector3 cpos =  Camera.main.transform.position;
        Vector3 eyevec = cpos - childrenobj[0].transform.position;
        float inner_vec;
        inner_vec = n_vec.x * eyevec.x + n_vec.y * eyevec.y + n_vec.z * eyevec.z;
        //Debug.Log(inner_vec);

        if(inner_vec >= 0)
        {
            spriteRenderer.sprite = face[cardIndex];
        }
        else
        {
            if (BackIndex == 0)
            {
                spriteRenderer.sprite = cardBack;
            }
            else
            {
                spriteRenderer.sprite = BonusBack;
            }
        }

        if(transform.position.z < -300)
        {
            gameObject.SetActive(false);
        }
    }

    private Vector3 CalcOuter(Vector3 v1, Vector3 v2)
    {
        Vector3 n;
        n.x = v1.y * v2.z - v1.z * v2.y;
        n.y = v1.z * v2.x - v1.x * v2.z;
        n.z = v1.x * v2.y - v1.y * v2.x;

        return n;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

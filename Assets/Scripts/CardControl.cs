using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody rigid;
    private Collider coll;

    public CardCreate CC;

    public int cardIndex;
    public int BackIndex;

    public bool isalpha = false;
    public bool Isdeeling = false;

    [SerializeField]
    private Sprite[] face;
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite OKcards;

    private GameObject[] childrenobj = new GameObject[3];

    float fliptime = 0;

    private bool flag = false;

    public bool showface = false;

    public bool flipflag = false;
    private bool backflipflag = false;

    private float lefttime = 180;

    private void OnBecameVisible()
    {
        spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        spriteRenderer.receiveShadows = true;
        gameObject.tag = "Cards";
        gameObject.layer = 0;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        rigid.isKinematic = true;
        //coll.enabled = true;
        backflipflag = false;
    }

    private void OnBecameInvisible()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        spriteRenderer.receiveShadows = true;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        rigid.isKinematic = true;
        //coll.enabled = true;
        backflipflag = false;
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
        //gameObject.transform.Rotate(0, Time.deltaTime * 50, 0);

        /*
        int a = (int)(transform.localEulerAngles.x / 90);
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

        rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -1.4f, 20.0f), rigid.velocity.z);


        if (flipflag)
        {
            lefttime -= Time.deltaTime * 1000;
            transform.Rotate(0,Time.deltaTime * 1000, 0);
            if (lefttime < 0)
            {
                flipflag = false;
                transform.Rotate(0, lefttime, 0);
                lefttime = 180;
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
            spriteRenderer.sprite = cardBack;
        }

        if(transform.position.y < -300)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if(isalpha == true)
        {
            spriteRenderer.material.color -= new Color(0,0,0,Time.fixedDeltaTime * 0.25f);
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

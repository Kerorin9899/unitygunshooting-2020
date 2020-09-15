using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class raygun : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject gunparentobj;
    [SerializeField]
    private GameObject Cardselected;

    [SerializeField]
    private GameObject Hiteff;

    [SerializeField]
    private GameObject Cursor1p;

    Curosormoves cur;
    private float defaultcursorspeed;

    public List<GameObject> particles = new List<GameObject>();
    public List<GameObject> cardselect = new List<GameObject>();
    private List<int> cardselectnum = new List<int>();
    private int part = 10;
    private int selnum = 6;

    public GameObject[] pair = new GameObject[10];
    private int pairIndex = 0;
    private bool Mistakeflag = false;

    private int snum;
    private float GunShotLagTime = 0.35f;
    public bool BulletLeft = true;
    public bool IsReloading = false;
    public bool IsShooting = false;

    CardCreate cc;
    Bulletcont bu;

    public AudioSource gunshot;
    public AudioSource gunreload;
    public AudioClip c;
    public AudioClip GunReload;
    public AudioClip GunReloadput;

    private Animator Gunanim;

    private float Reloadtime = 1.75f;

    public int reloadmode = 0;
    public bool rapidg = false;
    public bool twotimespoint = false;
    public bool skulleffect = false;

    float br;
    float bra;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CardCreate>();
        bu = GetComponent<Bulletcont>();
        cur = Cursor1p.GetComponent<Curosormoves>();
        defaultcursorspeed = cur.RapidCursor;
        Gunanim = gunparentobj.transform.GetChild(0).GetComponent<Animator>();

        br = Reloadtime;
        bra = cur.RapidCursor;

        for (int i = 0;i < part;i++)
        {
            GameObject a = Instantiate(Hiteff);
            particles.Add(a);
            a.SetActive(false);
        }

        for(int i = 0;i < selnum;i++)
        {
            GameObject a = Instantiate(Cardselected);
            cardselect.Add(a);
            a.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug FastReload
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log(fastre);
            switch (reloadmode)
            {
                case 0:
                case 2:
                    reloadmode = 1;
                    Reloadtime = 0.5f;
                    break;
                case 1:
                    reloadmode = 0;
                    Reloadtime = 1.75f;
                    break;
            }
        }

        //Debug RapidGun
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            rapidg = !rapidg;
            if(rapidg)
            {
                cur.RapidCursor = 2.0f;
            }
            else 
            {
                cur.RapidCursor = defaultcursorspeed;
            }
        }

        //Debug 2times
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            twotimespoint = !twotimespoint;
            if (twotimespoint)
            {

            }
            else
            {

            }
        }

        //Debug Skull
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            skulleffect = !skulleffect;

            if(skulleffect)
            {
                //int n = Random.Range(0,3);
                int n = 0;

                switch (n)
                {
                    case 0:
                        br = Reloadtime;
                        reloadmode = 2;
                        Reloadtime *= 2;
                        break;
                    case 1:
                        bra = cur.RapidCursor;
                        cur.RapidCursor /= 2.0f;
                        break;
                    case 2:
                        break;
                }
            }
            else
            {
                Reloadtime = br;
                reloadmode = 0;
                cur.RapidCursor = bra;
            }
        }

        if (!BulletLeft)
        {
            if (!IsReloading)
            {
                IsReloading = true;

                bu.reloading(Reloadtime);

                switch(reloadmode)
                {
                    case 0:
                        gunreload.clip = GunReloadput;
                        gunreload.pitch = 1.0f;
                        gunreload.PlayDelayed(0.7f);
                        Gunanim.SetTrigger("IsReroading");
                        break;
                    case 1:
                        gunreload.clip = GunReload;
                        gunreload.pitch = 1.0f;
                        gunreload.PlayDelayed(0.3f);
                        Gunanim.SetTrigger("IsfastReroading");
                        break;
                    case 2:
                        gunreload.clip = GunReloadput;
                        gunreload.pitch = 0.5f;
                        gunreload.PlayDelayed(0.9f);
                        Gunanim.SetTrigger("IsslowReloading");
                        break;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (!IsReloading)
            {
                IsReloading = true;

                bu.reloading(Reloadtime);

                switch (reloadmode)
                {
                    case 0:
                        gunreload.clip = GunReloadput;
                        gunreload.pitch = 1.0f;
                        gunreload.PlayDelayed(0.4f);
                        Gunanim.SetTrigger("IsReroading");
                        break;
                    case 1:
                        gunreload.clip = GunReload;
                        gunreload.pitch = 1.0f;
                        gunreload.PlayDelayed(0.3f);
                        Gunanim.SetTrigger("IsfastReroading");
                        break;
                    case 2:
                        gunreload.clip = GunReloadput;
                        gunreload.pitch = 0.5f;
                        gunreload.PlayDelayed(0.6f);
                        Gunanim.SetTrigger("IsslowReloading");
                        break;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsReloading)
            {
                if (!IsShooting)
                {
                    //shotdelay
                    IsShooting = true;
                    Invoke("shotflag", GunShotLagTime);

                    Gunanim.SetTrigger("IsShoot");
                    gunshot.PlayOneShot(c);
                    bu.bulletremove();
                    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.SphereCast(ray, 1, out hit, 200)/* && !Mistakeflag*/)
                    {
                        Vector3 f = hit.point;
                        onhiteffiect(f);
                        OnhitSelect(hit);
                        //hit.collider.gameObject.GetComponent<CardControl>().showface = true;
                        hit.collider.gameObject.GetComponent<CardControl>().flipflag = true;
                        pair[pairIndex] = hit.collider.gameObject;
                        pair[pairIndex].GetComponent<Collider>().enabled = false;

                        if (pairIndex == 1)
                        {
                            int a, b;
                            a = pair[0].GetComponent<CardControl>().cardIndex;
                            b = pair[1].GetComponent<CardControl>().cardIndex;
                            if (a == b)
                            {
                                snum = a;
                                int i;
                                for (i = 0; i < cc.Cards.Count; i++)
                                {
                                    if (pair[0] != cc.Cards[i] && pair[1] != cc.Cards[i])
                                    {
                                        if (cc.Cards[i].GetComponent<CardControl>().cardIndex == snum)
                                        {
                                            cc.Cards[i].GetComponent<Collider>().enabled = false;
                                            break;
                                        }
                                    }
                                }
                                CorrectCards();
                            }
                            else
                            {
                                //Mistakeflag = true;
                                Invoke("InvCards", 0.3f);
                            }
                        }

                        pairIndex++;

                        if (pairIndex >= 2)
                        {
                            pairIndex = 0;
                        }

                    }
                }

            }
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
        }

        //Debug.Log(cc);
    }

    void shotflag()
    {
        IsShooting = false;
    }

    void onhiteffiect(Vector3 pos)
    { 
        for(int i = 0;i < part;i++)
        {
            if (!particles[i].activeInHierarchy)
            {
                particles[i].SetActive(true);
                particles[i].transform.position = pos;
                break;
            }
        }
    }

    void OnhitSelect(RaycastHit h)
    {
        for(int i = 0;i < selnum;i++)
        {
            if(!cardselect[i].activeInHierarchy)
            {
                cardselect[i].SetActive(true);
                cardselect[i].transform.position = h.collider.gameObject.transform.position;
                cardselectnum.Add(i);
                break;
            }
        }
    }

    void InvCards()
    {
        cardselect[cardselectnum[0]].SetActive(false);
        cardselect[cardselectnum[1]].SetActive(false);
        cardselectnum.Remove(0);
        cardselectnum.Remove(1);
        pair[0].GetComponent<CardControl>().flipflag = true;
        pair[1].GetComponent<CardControl>().flipflag = true;
        pair[0].GetComponent<Collider>().enabled = true;
        pair[1].GetComponent<Collider>().enabled = true;
        //Mistakeflag = false;
    }

    void CorrectCards()
    {
        Rigidbody p1 = pair[0].GetComponent<Rigidbody>();
        Rigidbody p2 = pair[1].GetComponent<Rigidbody>();
        p1.isKinematic = false;
        p2.isKinematic = false;

        Vector3 f1 = new Vector3(UnityEngine.Random.Range(-100.0f, 100.0f), UnityEngine.Random.Range(100.0f, 200.0f), UnityEngine.Random.Range(-50.0f, 50.0f));
        Vector3 f2 = new Vector3(UnityEngine.Random.Range(-100.0f, 100.0f), UnityEngine.Random.Range(100.0f, 200.0f), UnityEngine.Random.Range(-50.0f, 50.0f));
        Vector3 f3 = new Vector3(UnityEngine.Random.Range(-100.0f, 100.0f), UnityEngine.Random.Range(100.0f, 200.0f), UnityEngine.Random.Range(-50.0f, 50.0f));

        p1.AddForce(f1, ForceMode.Impulse);
        p2.AddForce(f2, ForceMode.Impulse);

        p1.AddTorque(f2);
        p2.AddTorque(f3);

        StartCoroutine(activefalsecards(pair[0]));
        StartCoroutine(activefalsecards(pair[1]));

        cardselect[cardselectnum[0]].SetActive(false);
        cardselect[cardselectnum[1]].SetActive(false);
        cardselectnum.Remove(0);
        cardselectnum.Remove(1);

        int i;
        for (i = 0; i < cc.Cards.Count; i++)
        {
            if(pair[0] != cc.Cards[i] && pair[1] != cc.Cards[i])
            {
                if (cc.Cards[i].GetComponent<CardControl>().cardIndex == snum)
                {
                    Rigidbody p3;
                    p3 = cc.Cards[i].GetComponent<Rigidbody>();
                    StartCoroutine(activefalsecards(cc.Cards[i]));
                    //cc.Cards[i].SetActive(false);
                    p3.isKinematic = false;
                    p3.AddForce(f3, ForceMode.Impulse);
                    p3.AddTorque(f1);
                    break;
                }
            }
        }
    }

    IEnumerator activefalsecards(GameObject a)
    {
        yield return new WaitForSeconds(10.0f);
        a.SetActive(false);
    }
}

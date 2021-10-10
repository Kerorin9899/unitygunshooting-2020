using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCards : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject effect_bonus;

    private List<GameObject> efflist = new List<GameObject>();

    public GameObject[] pair = new GameObject[10];
    public int[] index_pair = new int[10];
    private int pairIndex = 0;

    CardCreate CC;
    private CardControl CCont;
    Curosormoves Cur;

    int same_num;

    private bool Isselect = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        int i = 0;
        for(i = 0;i < 10;i++)
        {
            GameObject a = Instantiate(effect_bonus);
            efflist.Add(a);
            a.SetActive(false);
        }

        CC = GetComponent<CardCreate>();
        Cur = GetComponent<Curosormoves>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CC.Issetting == 2)
        {
            select();
        }
        else if(CC.Issetting == 3)
        {
            selectok();
        }
    }

    void selectok()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f, false);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
        {
            if (hit.collider.CompareTag("Cards"))
            {
                if (Input.GetMouseButtonDown(0) && Isselect == false)
                {

                    GameObject hit_obj = hit.collider.gameObject;
                    CCont = hit_obj.GetComponent<CardControl>();
                    CCont.flipflag = true;

                    //Sound
                    PlayTurnSounds(hit_obj);

                    //Effect
                    ActiveEff(hit.collider.gameObject);

                    Debug.Log(CCont.cardIndex);

                    if (CCont.cardIndex == 32)
                    {
                        CC.Issetting = 10;
                        Invoke("ready", 2.0f);
                        StartCoroutine(Discards(hit_obj));
                    }
                }
            }
        }
    }

    IEnumerator Discards(GameObject c)
    {
        yield return new WaitForSeconds(0.5f);
        c.GetComponent<OutCard>().Isout = true;
    }

    private void ready()
    {
        CC.Issetting = 0;
    }

    void select()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f, false);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
        {
            if (hit.collider.CompareTag("Cards"))
            {
                if (Input.GetMouseButtonDown(0) && Isselect == false)
                {

                    Vector3 f = hit.point;
                    GameObject hit_obj = hit.collider.gameObject;
                    CCont = hit_obj.GetComponent<CardControl>();
                    CCont.flipflag = true;  

                    //Sound
                    PlayTurnSounds(hit_obj);

                    if(CCont.cardIndex >= 30)
                    {
                        checkBonus(CCont.cardIndex);
                    }
                    else
                    { 
                        //Effect
                        ActiveEff(hit.collider.gameObject);

                        pair[pairIndex] = hit_obj;
                        pair[pairIndex].layer = 2;
                        index_pair[pairIndex] = pair[pairIndex].GetComponent<CardControl>().cardIndex;

                        if (pairIndex == 1)
                        {
                            if (index_pair[0] == index_pair[1])
                            {
                                same_num = index_pair[0];
                                for (int i = 0; i < CC.Cards.Count; i++)
                                {
                                    if (pair[0] != CC.Cards[i] && pair[1] != CC.Cards[1])
                                    {
                                        if (CC.Cards[i].GetComponent<CardControl>().cardIndex == index_pair[0])
                                        {
                                            CC.Cards[i].GetComponent<Collider>().enabled = false;
                                            break;
                                        }
                                    }
                                }

                                Invoke("CorrectCards", 0.3f);
                            }
                            else
                            {
                                StartCoroutine(incorrectwaittime());
                            }
                        }
                        pairIndex++;
                    }


                    if (pairIndex >= 2)
                    {
                        pairIndex = 0;
                    }
                }

            }
        }
    }

    void checkBonus(int idx)
    {
        switch(idx)
        {
            case 30:

                break;
            case 31:

                break;
        }
    }

    void ActiveEff(GameObject p)
    {
        int i = 0;
        for (i = 0; i < 10; i++)
        {
            if (!efflist[i].activeInHierarchy)
            {
                efflist[i].SetActive(true);

                var tmp = p.transform.position;
                tmp.y += 0.1f;
                efflist[i].transform.position = tmp;

                break;
            }
        }
    }

    private void PlayTurnSounds(GameObject obj)
    {
        //Debug.Log("ok");

        ExecuteEvents.Execute<IEventCaller>(
            target: obj,
            eventData: null,
            functor: (reciever, eventData) => reciever.OnTurn()
            );
    }

    void InvCards()
    {
        
    }

    void CorrectCards()
    {
        Vector3 f1 = new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), UnityEngine.Random.Range(5.0f, 5.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
        Vector3 f2 = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(1.0f, 5.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
        Vector3 f3 = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(6.0f, 8.0f), UnityEngine.Random.Range(-0.1f, 0.1f));

        int i;
        for (i = 0; i < CC.Cards.Count; i++)
        {
            if (pair[0] != CC.Cards[i] && pair[1] != CC.Cards[i])
            {
                if (CC.Cards[i].GetComponent<CardControl>().cardIndex == same_num)
                {
                    Rigidbody p3;
                    p3 = CC.Cards[i].GetComponent<Rigidbody>();
                    CC.Cards[i].layer = 2;
                    p3.GetComponent<Collider>().enabled = true;
                    StartCoroutine(activefalsecards(CC.Cards[i]));
                    p3.isKinematic = false;
                    p3.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    p3.AddForce(f3, ForceMode.Impulse);
                    p3.AddTorque(f1 * 200);
                    p3.tag = "selectedcards";
                    break;
                }
            }
        }

    }

    private void alphablend()
    {

    }

    IEnumerator activefalsecards(GameObject a)
    {
        yield return new WaitForSeconds(8.0f);
        a.SetActive(false);
    }

    IEnumerator incorrectwaittime()
    {
        Isselect = true;
        yield return new WaitForSeconds(0.65f);
        pair[0].GetComponent<CardControl>().flipflag = true;
        pair[1].GetComponent<CardControl>().flipflag = true;
        pair[0].layer = 0;
        pair[1].layer = 0;
        Isselect = false;
    }
}

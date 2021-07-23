using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCards : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public GameObject[] pair = new GameObject[10];
    public int[] index_pair = new int[10];
    private int pairIndex = 0;

    CardCreate CC;
    Curosormoves Cur;

    int snum;

    public int Issetting;
    private bool Isselect = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        CC = GetComponent<CardCreate>();
        Cur = GetComponent<Curosormoves>();
        Issetting = 0;
    }

    // Update is called once per frame
    void Update()
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
                    hit.collider.gameObject.GetComponent<CardControl>().flipflag = true;
                    pair[pairIndex] = hit.collider.gameObject;
                    pair[pairIndex].GetComponent<Collider>().enabled = false;
                    index_pair[pairIndex] = pair[pairIndex].GetComponent<CardControl>().cardIndex;

                    if (pairIndex == 1)
                    {
                        if (index_pair[0] == index_pair[1])
                        {
                            snum = index_pair[0];
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

                    if (pairIndex >= 2)
                    {
                        pairIndex = 0;
                    }
                }

                //ここに選択時のエフェクトをかける

            }
        }
    }

    void InvCards()
    {
        
    }

    void CorrectCards()
    {
        Vector3 f1 = new Vector3(UnityEngine.Random.Range(-5.0f, 5.0f), UnityEngine.Random.Range(5.0f, 5.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
        Vector3 f2 = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(1.0f, 5.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
        Vector3 f3 = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(6.0f, 10.0f), UnityEngine.Random.Range(-0.2f, 0.2f));

        int i;
        for (i = 0; i < CC.Cards.Count; i++)
        {
            if (pair[0] != CC.Cards[i] && pair[1] != CC.Cards[i])
            {
                if (CC.Cards[i].GetComponent<CardControl>().cardIndex == snum)
                {
                    Rigidbody p3;
                    p3 = CC.Cards[i].GetComponent<Rigidbody>();
                    StartCoroutine(activefalsecards(CC.Cards[i]));
                    p3.isKinematic = false;
                    p3.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                    p3.AddForce(f3, ForceMode.Impulse);
                    p3.AddTorque(f1 * 200);
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
        yield return new WaitForSeconds(0.6f);
        pair[0].GetComponent<CardControl>().flipflag = true;
        pair[1].GetComponent<CardControl>().flipflag = true;
        pair[0].GetComponent<Collider>().enabled = true;
        pair[1].GetComponent<Collider>().enabled = true;
        Isselect = false;
    }
}

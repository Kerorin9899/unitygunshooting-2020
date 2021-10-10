using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject cardobj;
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite BonusBack;
    [SerializeField]
    private Sprite BonusCards;

    [SerializeField]
    private Text countdown_text;

    [SerializeField]
    private int cardkind_num;

    public List<GameObject> Cards = new List<GameObject>();

    private List<GameObject> BoardCards = new List<GameObject>();
    private List<CardControl> CClist = new List<CardControl>();
    private List<dealcards> dclist = new List<dealcards>();


    //public List<SpriteRenderer> cardren = new List<SpriteRenderer>();
    //public List<SpriteRenderer> bonuses = new List<SpriteRenderer>();

    private List<int> num = new List<int>();

    [SerializeField]
    private GameObject changegameobj;

    private int cardIndex = 0;
    public int numcard = 40;

    private bool testflag = false;
    private int testsum = 0;
    private int r = 30;

    private int j = 0;
    private int l = 0;

    private Vector3 cardfirstpos = new Vector3(0,0,0);

    //debug
    private float x_offset = 0.35f;
    private float z_offset = 0.35f;
    private float ypos = 0.65f; //def0.65;
    private float defaultzpos = -2.7f;
    private List<Vector3> debugCardposition = new List<Vector3>();
    private int randombonuspoint;
    private bool setbonus = false;

    private float cardvec_bias = 3;

    //mode管理
    public int Issetting;

    public int Isreadynum;

    //private void deelcardcall(GameObject obj, Vector3 pos)
    //{
    //    //Debug.Log("ok");

    //    ExecuteEvents.Execute<IEventCaller>(
    //        target:obj,
    //        eventData:null,
    //        functor: (reciever, eventData) => reciever.InstanceValue(pos)
    //        );
    //}

    // Start is called before the first frame update
    void Awake()
    {
        int i;

        cardfirstpos = new Vector3(0, ypos, -0.6f);

        //何枚縦に並べるか
        int height = 5;

        float s = numcard / height;
        float x, z;
        float dx = (s / 2.0f) - 0.5f;
        float dz = (height / 2.0f) - 0.5f;

        x = - dx * x_offset;
        z = dz * z_offset;

        for(i = 0;i < height;i++)
        {
            for(int j = 0;j < (int)s;j++)
            {
                Vector3 pos = new Vector3(x,ypos,z + defaultzpos);
                debugCardposition.Add(pos);
                x += x_offset;
            }
            x -= s * x_offset;
            z -= z_offset;
        }

        cardkind_num = 21;

        //debugCardposition = debugCardposition.OrderBy(a => Guid.NewGuid()).ToList();

        for (i = 0;i < numcard;i++)
        {
            GameObject a = cardobj;
            //test
            GameObject c = Instantiate(a);
            c.GetComponent<SpriteRenderer>().sprite = cardBack;
            c.GetComponent<CardControl>().BackIndex = 0;
            //cardren.Add(c.GetComponent<SpriteRenderer>());
            CClist.Add(c.GetComponent<CardControl>());
            c.GetComponent<CardControl>().CC = gameObject.GetComponent<CardCreate>();
            dclist.Add(c.GetComponent<dealcards>());

            c.SetActive(false);
            Cards.Add(c);
        }

        //for (i = 0; i < 10; i++)
        //{
        //    GameObject a = cardobj;
        //    GameObject c = Instantiate(a);
        //    c.GetComponent<SpriteRenderer>().sprite = BonusBack;
        //    c.GetComponent<CardControl>().BackIndex = 1;
        //    cardren.Add(c.GetComponent<SpriteRenderer>());
        //    c.SetActive(false);
        //    Cards.Add(c);
        //}

        //Random rnd = new Random();

        /*
        for (i = 0;i < numcard;i++)
        {
            Cards[i].transform.position = new Vector3(Random.Range(-50,51), Random.Range(-50, 51),50);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Issetting == 0)
        {
            Isreadynum = 0;
            Issetting = 1;
            num.Clear();
            int n = 0;
            setbonus = false;

            randombonuspoint = UnityEngine.Random.Range(0,numcard);

            Cards = Cards.OrderBy(a => Guid.NewGuid()).ToList();
            InvokeRepeating("MakeCardrepeat", 0, 0.1f);


            //Eventcall
            //CallChangeGameEvent();

            for (n = 0;n < cardkind_num;n++)
            {
                num.Add(n);
            }

        }

        //Debug.Log(testflag);
    }


    private void FixedUpdate()
    {
        if(Issetting == 1)
        {
            Checkdeeling();
        }
        

        //if(Issetting == 1)
        //{
        //    int i = 0;
        //    for(i = 0;i < 3;i++)
        //    {
        //        MakeCards();
        //    }

        //    testsum++;

        //    if(testsum >= (numcard-1) / 3)
        //    {
        //        testsum = 0;
        //        MakeCardBonus();
        //    }
        //}

    }

    private void Checkdeeling()
    {
        if(Isreadynum >= numcard)
        {
            Issetting = 0;
            Isreadynum = 0;
        }
    }

    void MakeCardrepeat()
    {
        int i;

        if(j == randombonuspoint && setbonus == false)
        {
            setbonus = true;
            MakeCardBonus();
            return;
        }

        for (i = 0; i < numcard; i++)
        {
            if (!Cards[i].activeInHierarchy)
            {
                if (j % 3 == 0)
                {
                    //Debug.Log(num.Count);
                    int d = UnityEngine.Random.Range(0, num.Count);
                    //Debug.Log("d = " + d.ToString());
                    r = num[d];
                    //Debug.Log("r = " + r.ToString());
                    num.RemoveAt(d);
                }

                //Debug.Log(Cards[i].GetComponent<SpriteRenderer>().sprite);
                CClist[i].cardIndex = r;
                Cards[i].transform.position = cardfirstpos;

                //Getcomponetする場合
                Cards[i].GetComponent<CardControl>().Isdeeling = true;
                Cards[i].GetComponent<dealcards>().arrivepos = debugCardposition[i];
                Cards[i].GetComponent<dealcards>().vec = (debugCardposition[i] - cardfirstpos) * cardvec_bias;

                //GetComponenyしない場合
                /*
                dclist[i].arrivepos = debugCardposition[i];
                dclist[i].vec = (debugCardposition[i] - cardfirstpos) * cardvec_bias;
                CClist[i].Isdeeling = true;
                */

                //Cards[i].transform.position = debugCardposition[i];
                Cards[i].transform.localEulerAngles = new Vector3(-90, 90, 90);
                Cards[i].SetActive(true);

                //deelcardcall(Cards[i], debugCardposition[i]);

                j++;
                break;
            }
        }

        Debug.Log(j);

        if(j >= numcard - 1)
        {
            j = 0;
            Issetting = 2;
            CancelInvoke("MakeCardrepeat");
        }

        //Debug.Log(Cards.Count);
    }

    void MakeCards()
    {
        int i;

        for (i = 0;i < numcard - 1;i++)
        {
            if(!Cards[i].activeInHierarchy)
            {
                if(j % 3 == 0)
                {
                    //Debug.Log(num.Count);
                    int d = UnityEngine.Random.Range(0, num.Count);
                    //Debug.Log("d = " + d.ToString());
                    r = num[d];
                    //Debug.Log("r = " + r.ToString());
                    num.RemoveAt(d);
                }

                Cards[i].transform.position = cardfirstpos;

                //Getcomponetする場合
                Cards[i].GetComponent<CardControl>().Isdeeling = true;
                Cards[i].GetComponent<dealcards>().arrivepos = debugCardposition[i];
                Cards[i].GetComponent<dealcards>().vec = (debugCardposition[i] - cardfirstpos) * cardvec_bias;

                //Debug.Log(Cards[i].GetComponent<SpriteRenderer>().sprite);
                //CClist[i].cardIndex = r;
                //dclist[i].arrivepos = debugCardposition[i];
                //dclist[i].vec = debugCardposition[i] - cardfirstpos;

                CClist[i].Isdeeling = true;
                //Cards[i].transform.position = debugCardposition[i];
                Cards[i].transform.localEulerAngles = new Vector3(-90, 90, 90);
                Cards[i].SetActive(true);

                //deelcardcall(Cards[i], debugCardposition[i]);

                j++;
                break;
            }
        }

        //Debug.Log(Cards.Count);
    }

    void MakeCardBonus()
    {
        int i;

        for (i = 0; i < numcard + 5; i++)
        {
            if (!Cards[i].activeInHierarchy)
            {
                //bonuscardの番号
                int r = UnityEngine.Random.Range(30, 32);
                CClist[i].cardIndex = r;
                Cards[i].transform.position = cardfirstpos;

                //Getcomponetする場合
                Cards[i].GetComponent<CardControl>().Isdeeling = true;
                Cards[i].GetComponent<dealcards>().arrivepos = debugCardposition[i];
                Cards[i].GetComponent<dealcards>().vec = (debugCardposition[i] - cardfirstpos) * cardvec_bias;

                //Debug
                // r = 0;
                //dclist[i].arrivepos = debugCardposition[i];
                //dclist[i].vec = (debugCardposition[i] - cardfirstpos) * cardvec_bias;


                //CClist[i].Isdeeling = true;
                Cards[i].transform.localEulerAngles = new Vector3(-90, 90, 90);
                Cards[i].SetActive(true);

                break;
            }
        }
            //deelcardcall(Cards[i], debugCardposition[i]);
    }

    public void MakeOKCard()
    {
        GameObject a = Instantiate(cardobj);

        a.transform.position = cardfirstpos;
        a.GetComponent<CardControl>().cardIndex = 32;
        a.GetComponent<CardControl>().Isdeeling = true;

        StartCoroutine(OKdeeling(a));

        a.GetComponent<dealcards>().arrivepos = new Vector3(0 ,0.65f, -3.0f);
        a.GetComponent<dealcards>().vec = (new Vector3(0, 0.65f, -3.0f) - cardfirstpos) * cardvec_bias;
        a.transform.localEulerAngles = new Vector3(-90, 90, 90);
        Isreadynum--;
    }

    IEnumerator OKdeeling(GameObject a)
    {
        yield return new WaitForSeconds(0.3f);
        a.GetComponent<CardControl>().Isdeeling = false;
    }

    private void SetScaleText(int num)
    {
        countdown_text.text = num.ToString();
        countdown_text.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    IEnumerator countdown()
    {
        countdown_text.GetComponent<GameObject>().SetActive(true);

        for (int i = 0;i < 3;i++)
        {
            SetScaleText(i);
            yield return new WaitForSeconds(0.6f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine.EventSystems;

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
    private int cardkind_num;

    public List<GameObject> Cards = new List<GameObject>();
    public List<SpriteRenderer> cardren = new List<SpriteRenderer>();
    public List<SpriteRenderer> bonuses = new List<SpriteRenderer>();

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

    //debug
    private float x_offset = 0.35f;
    private float z_offset = 0.35f;
    private float ypos = 0.65f; //def0.65;
    private float defaultzpos = -2.7f;
    private List<Vector3> debugCardposition = new List<Vector3>();

    // Start is called before the first frame update
    void Awake()
    {
        int i;

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

        debugCardposition = debugCardposition.OrderBy(a => Guid.NewGuid()).ToList();


        for (i = 0;i < numcard * 2;i++)
        {
            GameObject a = cardobj;
            //test
            GameObject c = Instantiate(a);
            c.GetComponent<SpriteRenderer>().sprite = cardBack;
            c.GetComponent<CardControl>().BackIndex = 0;
            cardren.Add(c.GetComponent<SpriteRenderer>());
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
        if(Input.GetKeyDown(KeyCode.P) && !testflag)
        {
            testflag = true;
            num.Clear();
            int n = 0;

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
        if(testflag)
        {
            int i = 0;
            for(i = 0;i < 3;i++)
            {
                MakeCards();
            }

            testsum++;

            if(testsum >= (numcard-1) / 3)
            {
                testsum = 0;
                MakeCardBonus();
                testflag = false;
            }
        }
        
    }

    private void CallChangeGameEvent()
    {
        ExecuteEvents.Execute<IEventCaller>(
            target: changegameobj, 
            eventData: null, 
            functor: CallMyEvent
            );
    }

    void CallMyEvent(IEventCaller inf, BaseEventData eventData)
    {
        inf.Gamechange();
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

                //Debug.Log(Cards[i].GetComponent<SpriteRenderer>().sprite);
                Cards[i].GetComponent<CardControl>().cardIndex = r;
                Cards[i].transform.position = debugCardposition[i];
                Cards[i].transform.localEulerAngles = new Vector3(-90, 90, 90);
                Cards[i].SetActive(true);


                j++;
                break;
            }
        }

        //Debug.Log(Cards.Count);
    }

    void MakeCardBonus()
    {
        int i = numcard - 1;

        if(!Cards[i].activeInHierarchy)
        {
            //bonuscardの数  
            int r = UnityEngine.Random.Range(29,31);

            //Debug
            // r = 0;
            Cards[i].GetComponent<CardControl>().cardIndex = r;
            Cards[i].transform.position = debugCardposition[i];
            Cards[i].transform.localEulerAngles = new Vector3(-90, 90, 90);
            Cards[i].SetActive(true);
        }
    }
}

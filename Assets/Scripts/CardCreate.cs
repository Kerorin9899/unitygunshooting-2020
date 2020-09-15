using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

public class CardCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject cardobj;
    [SerializeField]
    private Sprite cardBack;
    [SerializeField]
    private Sprite BonusBack;

    public List<GameObject> Cards = new List<GameObject>();
    public List<SpriteRenderer> cardren = new List<SpriteRenderer>();

    private List<int> num = new List<int>();

    private int cardIndex = 0;
    public int numcard = 60;

    private bool testflag = false;
    private int testsum = 0;
    private int r = 30;
    private int j = 0;

    private int l = 0;

    //debug
    private int offset = 20;
    private List<Vector3> debugCardposition = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        int i;

        int s = numcard / 4;
        float x, y;
        float d = s / 2.0f;

        x = - d * offset;
        y = 2 * offset;
        for(i = 0;i < 4;i++)
        {
            for(int j = 0;j < s;j++)
            {
                Vector3 pos = new Vector3(x,y,50);
                debugCardposition.Add(pos);
                x += offset;
            }
            x -= s * offset;
            y -= offset;
        }

        debugCardposition = debugCardposition.OrderBy(a => Guid.NewGuid()).ToList();

        for (i = 0;i < numcard;i++)
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

        for (i = 0; i < 10; i++)
        {
            GameObject a = cardobj;
            GameObject c = Instantiate(a);
            c.GetComponent<SpriteRenderer>().sprite = BonusBack;
            c.GetComponent<CardControl>().BackIndex = 1;
            cardren.Add(c.GetComponent<SpriteRenderer>());
            c.SetActive(false);
            Cards.Add(c);
        }

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
            for (n = 0;n < 26;n++)
            {
                num.Add(n);
            }
        }
        
        if(testflag)
        {
            int i = 0;
            for(i = 0;i < 3;i++)
            {
                MakeCards();
                //Debug.Log("tho");
            }

            testsum++;

            if(testsum >= 14)
            {
                testsum = 0;
                testflag = false;
            }
        }

        //Debug.Log(testflag);
    }

    void MakeCards()
    {
        int i;

        for (i = 0;i < 52;i++)
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
                Cards[i].SetActive(true);
                j++;
                break;
            }
        }
    }
}

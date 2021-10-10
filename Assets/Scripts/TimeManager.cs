using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour, IEventSystemHandler
{
    public float gametime = 120;

    private CardCreate CC;
    private float countthree_time = 0.5f;

    [SerializeField]
    private Text count;

    // Start is called before the first frame update
    void Start()
    {
        count.text = "";
        StartCoroutine(countThree());
    }

    private void Awake()
    {
        CC = GetComponent<CardCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CC.Issetting == 0)
        {
            gametime -= Time.deltaTime;

            if(gametime < 0)
            {
                gametime = 0;
                CC.Issetting = 11;
            }
        }
    }

    IEnumerator countThree()
    {

        for (int i = 3; i >= 0; i--)
        {
            count.text = i.ToString();

            count.rectTransform.localScale = new Vector2(1.5f, 1.5f);
            StartCoroutine(scaledown());
            yield return new WaitForSeconds(countthree_time);
        }
    }

    IEnumerator scaledown()
    {
        while (true)
        {
            yield return null;

            count.rectTransform.localScale -= new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime, 0);

            if (count.rectTransform.localScale.x < 1.0f)
            {
                count.rectTransform.localScale = new Vector2(1.0f, 1.0f);

                break;
            }
        }
    }

}

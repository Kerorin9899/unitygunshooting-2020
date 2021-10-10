using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titlescript : MonoBehaviour
{
    private CardCreate CC;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject quit;
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject logo;

    [SerializeField]
    private Image Back;

    public int titleselecting_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CanPlayTitle", 2.0f);
    }

    private void Awake()
    {
        CC = GetComponent<CardCreate>();
        CC.Issetting = 5;

        Back.color += new Color(0,0,0,1);
    }

    // Update is called once per frame
    void Update()
    {
        if(CC.Issetting == 4)
        {
            switch(titleselecting_num)
            {
                case 0:

                    break;
                case 1:
                    CC.Issetting = 3;
                    animator.SetBool("Istitle", true);
                    quit.SetActive(false);
                    start.SetActive(false);
                    logo.SetActive(false);
                    Invoke("deelOKCard", 4.0f);
                    break;
            }
        }

        if(Back.color.a > 0)
        {
            Back.color -= new Color(0,0,0, Time.deltaTime * 0.5f);
        }
        else
        {
            if (Back.enabled == true)
            {
                Back.enabled = false;
            }
        }
    }

    void CanPlayTitle()
    {
        CC.Issetting = 4;
    }

    void deelOKCard()
    {
        CC.MakeOKCard();
    }
}

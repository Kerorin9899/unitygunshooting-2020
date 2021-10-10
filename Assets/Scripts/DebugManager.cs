using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    private CardCreate CC;

    [SerializeField]
    private Text test;

    private int check;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CardCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        if(check != CC.Issetting)
        {
            test.text = "Issetteing = " + CC.Issetting.ToString();
        }

        check = CC.Issetting;
    }
}

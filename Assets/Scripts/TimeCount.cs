using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCount : MonoBehaviour
{
    public TextMeshProUGUI time_text;
    public TextMeshProUGUI time_min_text;

    private Vector2 time_default_position;

    [SerializeField]
    private float clock;

    public bool CountDown = false;

    private bool LeftTen = false;

    private float ShiftDistance = 35;

    // Start is called before the first frame update
    void Start()
    {
        time_default_position = time_text.rectTransform.anchoredPosition;
        ResetTimeText();
    }

    private void ResetTimeText()
    {
        clock = 12.0f;
        time_min_text.text = "";
        time_min_text.color -= new Color(0, 0, 0, 1);
        LeftTen = false;
        time_text.rectTransform.anchoredPosition = time_default_position;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime();

        if(CountDown)
        {
            clock -= Time.deltaTime;
        }
    }

    private void DisplayTime()
    {

        string n = "N0";
        if (clock <= 10)
        {
            if(!LeftTen)
            {
                LeftTen = true;
                StartCoroutine(shiftleft());
                StartCoroutine(FadeInText());
                //Debug.Log("l");
            }

            float msec = clock - (int)clock;

            msec *= 100;

            if(msec < 9)
            {
                time_min_text.text = ".0" + msec.ToString(n);
            }
            else
            {
                time_min_text.text = "." + msec.ToString(n);
            }

            time_text.text = clock.ToString(n);
        }
        else
        {
            n = "N0";
            time_text.text = clock.ToString(n);
        }

    }

    IEnumerator shiftleft()
    {
        float shift_dis = 0;
        while (true)
        {
            time_text.rectTransform.anchoredPosition -= new Vector2(Time.deltaTime * 50, 0);
            shift_dis += Time.deltaTime * 50;

            if (shift_dis > ShiftDistance)
            {
                break;
            }

            yield return null;
        }
    }

    IEnumerator FadeInText()
    {
        while (true)
        {
            time_min_text.color += new Color(0,0,0,Time.deltaTime * 2);

            if (time_min_text.color.a >= 1)
            {
                Color tmp = time_min_text.color;
                tmp.a = 1;
                time_min_text.color = tmp;
                break;
            }

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSchedule : MonoBehaviour
{
    public Image SecondCount;
    public Text timetext;
    public GameObject clockthin;

    private int timesecond = 90;
    private float sec;

    //Debug
    private bool timeDebugFlag = true;
    private float tsec;

    // Start is called before the first frame update
    void Start()
    {
        sec = 1.0f;
        tsec = timesecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            timeDebugFlag = !timeDebugFlag;
        }

        if(timeDebugFlag)
        {
            timesecond = 90;
            tsec = timesecond;
            sec = 1;
            clockthin.transform.localEulerAngles = new Vector3(0, 0, 0);
            SecondCount.rectTransform.localScale = new Vector3(1, 1, 1);
            timetext.rectTransform.localScale = new Vector3(1, 1, 1);
            SecondCount.fillAmount = sec;
            timetext.text = timesecond.ToString();
        }
        else
        {
            float per = tsec / 90.00f;
            SecondCount.fillAmount = per;

            clockthin.transform.localEulerAngles = new Vector3(0,0, 360 * per);

            sec -= Time.deltaTime;
            tsec -= Time.deltaTime;

            if(SecondCount.rectTransform.localScale.x > 1.0)
            {
                float delta;
                delta = Time.deltaTime * 2;
                SecondCount.rectTransform.localScale -= new Vector3(delta,delta,delta);
                timetext.rectTransform.localScale -= new Vector3(delta, delta, delta);
            }
            else if(SecondCount.rectTransform.localScale.x < 1.0)
            {
                SecondCount.rectTransform.localScale = new Vector3(1, 1, 1);
                timetext.rectTransform.localScale = new Vector3(1, 1, 1);
            }

            if(sec <= 0)
            {
                sec = 1;
                timesecond--;

                if (timesecond > 30)
                {
                    timetext.color = new Color(0,0,0,1);
                }
                else
                {
                    timetext.color = new Color(1,0,0,1);
                    if (timesecond <= 10)
                    {
                        SecondCount.rectTransform.localScale = SecondCount.rectTransform.localScale * 1.3f;
                        timetext.rectTransform.localScale = timetext.rectTransform.localScale * 1.3f;
                    }
                }

                if(timesecond <= 0)
                {
                    timeDebugFlag = true;
                }

                timetext.text = timesecond.ToString();
            }
        }
    }
}

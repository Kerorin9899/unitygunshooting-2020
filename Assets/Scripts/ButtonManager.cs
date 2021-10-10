using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : BaseButtonCont
{
    private Titlescript ts;

    protected override void OnClick(string objectName)
    {
        // 渡されたオブジェクト名で処理を分岐
        //（オブジェクト名はどこかで一括管理した方がいいかも）
        if ("Start".Equals(objectName))
        {
            // Button1がクリックされたとき
            this.Button1Click();
        }
        else if ("Quit".Equals(objectName))
        {
            // Button2がクリックされたとき
            this.Button2Click();
        }
        else
        {
            throw new System.Exception("Not implemented!!");
        }
    }

    private void Button1Click()
    {
        Debug.Log("Start Click");
        ts.titleselecting_num = 1;
    }

    private void Button2Click()
    {
        Debug.Log("Quit Click");
        UnityEditor.EditorApplication.isPlaying = false;
        UnityEngine.Application.Quit();
    }

    private void Awake()
    {
        ts = GameObject.Find("Manager").GetComponent<Titlescript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testcaller : MonoBehaviour
{
    public GameObject targetObj;

    void Start()
    {
        //DoMyEvent();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            DoMyEvent();
    }
    /// <Summary>
    /// EventSystemsを使用してイベントを実行します。
    /// </Summary>
    void DoMyEvent()
    {
        NotifyEvent(targetObj);
    }

    /// <Summary>
    /// 対象のオブジェクトにイベントを通知します。
    /// </Summary>
    /// <param name="targetObj">対象のオブジェクト</param>
    void NotifyEvent(GameObject targetObj)
    {
        ExecuteEvents.Execute<IEventtest>(
                        target: targetObj,
                        eventData: null,
                        functor: (reciever, eventData) => reciever.EventCall()
                        );
    }

    /// <Summary>
    /// このイベントで指定するインタフェースのメソッドです。
    /// </Summary>
    void CallMyEvent(IEventtest inf, BaseEventData eventData)
    {
        inf.EventCall();
    }
}

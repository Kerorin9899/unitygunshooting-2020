using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour, IEventtest
{
    void Start()
    {

    }

    void Update()
    {

    }

    /// <Summary>
    /// EventSystemsを使用してこのメソッドを呼び出します。
    /// </Summary>
    public void EventCall()
    {
        // ログを表示します。
        Debug.Log("EventSystemsによるイベントが通知された！");
    }
}

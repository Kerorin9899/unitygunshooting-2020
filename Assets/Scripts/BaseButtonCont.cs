using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseButtonCont : MonoBehaviour
{
    private EventTrigger trigger;
    private EventTrigger.Entry entry;

    public BaseButtonCont button;

    [SerializeField]
    private AudioClip overide_se;

    [SerializeField]
    private AudioSource se;

    public void OnClick()
    {
        if (button == null)
        {
            throw new System.Exception("Button instance is null!!");
        }

        // 自身のオブジェクト名を渡す
        button.OnClick(this.gameObject.name);
    }

    protected virtual void OnClick(string objectName)
    {
        // 呼ばれることはない
        Debug.Log("Base Button");
    }

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        entry.callback.AddListener((data) => {
            se.PlayOneShot(overide_se, 0.15f);
        });

        trigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

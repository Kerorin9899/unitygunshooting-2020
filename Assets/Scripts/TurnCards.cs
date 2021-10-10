using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnCards : MonoBehaviour, IEventCaller
{
    [SerializeField]
    private AudioClip turnsound;

    private AudioSource soruce;

    // Start is called before the first frame update
    void Start()
    {
        soruce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTurn()
    {
        soruce.PlayOneShot(turnsound, 0.2f);
    }
}

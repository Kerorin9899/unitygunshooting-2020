using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMove : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private Vector3 dpos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        dpos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = dpos;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Vector3 toRay = ray.origin + ray.direction * 3000;

        Vector3 pos = gameObject.transform.position;

        pos.x += ray.origin.x * 50;
        pos.y += ray.origin.y * 3;

        Vector3 dir = toRay - pos;

        dpos = gameObject.transform.position;

        gameObject.transform.position = pos;

        //Debug.Log(ray.origin);

        //dir.x *= 100;
        //dir.y *= 20;

        gameObject.transform.LookAt(dir);
    }
}

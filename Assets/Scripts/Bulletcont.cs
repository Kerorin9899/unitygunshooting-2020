using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletcont : MonoBehaviour
{
    public List<GameObject> bullets1P = new List<GameObject>();
    public List<GameObject> bullets2P = new List<GameObject>();

    public int bulletpool1p = 6;
    public int bulletpool2p = 6;

    GameObject[] Bullet1Preload;

    raygun RayGunScript;
    SpinRib spinrib;

    // Start is called before the first frame update
    void Start()
    {
        RayGunScript = gameObject.GetComponent<raygun>();
        spinrib = gameObject.GetComponent<SpinRib>();

        Bullet1Preload = GameObject.FindGameObjectsWithTag("Bullet1p");
        foreach (GameObject b in Bullet1Preload)
        {
            bullets1P.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spinrib.Spin();
            shotbullet();
            /*
            if (bullets1P.Count != 0)
            {
                shotbullet();
                gunshot.PlayOneShot(c);
            }*/
        }
    }

    public void bulletremove()
    {
        bulletpool1p--;

        //Debug.Log(bulletpool1p);

        if(bulletpool1p == 0)
        {
            RayGunScript.BulletLeft = false;
        }
    }

    public void reloading(float time)
    {
        StartCoroutine(Reloadwait(time));
    }

    public void shotbullet()
    {
        for (int i = 0; i < bullets1P.Count; i++)
        {
            if (bullets1P[i].activeInHierarchy)
            {
                bullets1P[i].SetActive(false);
                break;
            }
        }
    }

    IEnumerator Reloadwait(float time)
    {
        RayGunScript.BulletLeft = true;
        bulletpool1p = 6;
        float t = time / 2.0f;
        float dt = time - t;
        yield return new WaitForSeconds(dt);
        for (int i = 0; i < 6; i++)
        {
            bullets1P[i].SetActive(true);
        }
        yield return new WaitForSeconds(t);
        RayGunScript.IsReloading = false;
    }
}

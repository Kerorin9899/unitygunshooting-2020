using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TitleScene : MonoBehaviour
{

    int GameMode = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameMode)
        {
            case 0://title

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}

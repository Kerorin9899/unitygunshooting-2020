using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private CardCreate CC;

    [SerializeField]
    private AudioSource title;
    [SerializeField]
    private AudioSource game;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CardCreate>();

        title.volume = 0;
        game.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(CC.Issetting == 5)
        {
            if(title.volume <= 0.1f)
            {
                playmusic(title, game);
            }
        }
        else if(CC.Issetting == 1)
        {
            if(game.volume <= 0.1f)
            {
                playmusic(game, title);
            }
        }
    }

    public void playmusic(AudioSource music, AudioSource down)
    {
        StepUpVolume(music);

        if (game.volume > 0)
        {
            StepDownVolume(down);
        }
    }

    void StepUpVolume(AudioSource audio)
    {
        audio.volume += Time.deltaTime * 0.05f;
    }

    void StepDownVolume(AudioSource audio)
    {
        audio.volume -= Time.deltaTime * 0.05f;
    }

}

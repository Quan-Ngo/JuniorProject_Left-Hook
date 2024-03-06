using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public AudioSource fightMusic;
    public FishingMinigameManager Minigame;
    public bool musicStopped;
    public int test;

    // Start is called before the first frame update
    void Start()
    {
        //fightMusic = GetComponent<AudioSource>();
        bgMusic.Play();
        musicStopped = false;
        //Debug.Log("BG PLAYING, MUSIC STOPPED = FALSE");
        test = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Minigame.inFishingGame == false && test == 0)
        {
            ChangeTrack();
            //Debug.Log("NOT IN FISH, CHANGE MUSIC");
        }
    }

    public void ChangeTrack()
    {
        /*if (test != 1)
        {
            bgMusic = music;
            Debug.Log("SET MUSIC TO NEW TRACK");
            if (musicStopped == false)
            {
                bgMusic.Stop();
                musicStopped = true;
                Debug.Log("MUSIC NOT STOPPED, STOPPING NOW");
                bgMusic = music;
                bgMusic.Play();
                Debug.Log("MUSIC IS STOPPED, PLAY NEW SONG");
            }
            if (musicStopped == true)
            {
                bgMusic = music;
                bgMusic.Play();
                Debug.Log("MUSIC IS STOPPED, PLAY NEW SONG");
            }
        }


        test = 1;*/
        bgMusic.Stop();
        bgMusic = fightMusic;
        if(test == 0)
        {
            bgMusic.Play();
            test = 1;
        }
       


    }
}

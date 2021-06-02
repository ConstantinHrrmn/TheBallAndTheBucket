using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip menuMusic;

    private AudioSource audio;

    private bool pause;
    private bool plays;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
        audio.clip = menuMusic;
        audio.loop = true;
        audio.Play();

        pause = false;
        plays = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audio.Stop();
        audio.clip = gameMusic;
        audio.Play();
    }

    public bool isPlaying()
    {
        return plays;
    }

    public void invertPlays()
    {
        this.plays = !this.plays;
    }

    public void Pause()
    {
        pause = !pause;

        if (pause)
        {
            this.audio.Pause();
        }
        else
        {
            this.audio.Play();
        }
    }
}

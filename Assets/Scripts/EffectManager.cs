using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public AudioClip Ball;
    public AudioClip Wall;
    public AudioClip Bucket;
    public AudioClip Dead;
    public AudioClip Pause;
    public AudioClip NextLevel;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        this.audio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        
    } 

    public void ball()
    {
        this.audio.PlayOneShot(this.Ball);
    }

    public void wall()
    {
        this.audio.PlayOneShot(this.Wall);
    }

    public void bucket()
    {
        this.audio.PlayOneShot(this.Bucket);
    }

    public void dead()
    {
        this.audio.PlayOneShot(this.Dead);
    }

    public void pause()
    {
        this.audio.PlayOneShot(this.Pause);
    }

    public void next()
    {
        this.audio.PlayOneShot(this.NextLevel);
    }
}

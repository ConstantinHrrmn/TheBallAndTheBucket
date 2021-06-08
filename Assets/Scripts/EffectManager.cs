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

    private AudioSource audioBall;
    private AudioSource audioWall;
    private AudioSource audioBucket;
    private AudioSource audioDead;
    private AudioSource audioPause;
    private AudioSource audioNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        //this.audio = this.gameObject.GetComponent<AudioSource>();
        this.audioBall = GameObject.Find("BallEffect").GetComponent<AudioSource>();
        this.audioWall = GameObject.Find("WallEffect").GetComponent<AudioSource>();
        this.audioBucket = GameObject.Find("BucketEffect").GetComponent<AudioSource>();
        this.audioDead = GameObject.Find("DeadEffect").GetComponent<AudioSource>();
        this.audioPause = GameObject.Find("PauseEffect").GetComponent<AudioSource>();
        this.audioNextLevel = GameObject.Find("NextLevelEffect").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        
    } 

    public void ball()
    {
        //this.audio.PlayOneShot(this.Ball);
        this.audioBall.PlayOneShot(this.Ball);
    }

    public void wall()
    {
        this.audioWall.PlayOneShot(this.Wall);
    }

    public void bucket()
    {
        this.audioBucket.PlayOneShot(this.Bucket);
    }

    public void dead()
    {
       this.audioDead.PlayOneShot(this.Dead);
    }

    public void pause()
    {
        //AudioSource AS = new AudioSource();
        //AS.PlayOneShot(this.Pause);
        this.audioPause.PlayOneShot(this.Pause);
    }

    public void next()
    {
       this.audioNextLevel.PlayOneShot(this.NextLevel);
    }
}

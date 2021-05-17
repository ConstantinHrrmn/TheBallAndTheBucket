using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{

    public int trys;
    public float time;

    public GameObject lblTrys;
    public GameObject lblTime;

    public int LevelAmount;
    private int actualLevel;

    private bool inGame = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.trys = 0;
        this.time = 0.0f;
        this.actualLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.inGame)
        {
            this.time += Time.deltaTime;

            float minutes = Mathf.Floor(this.time / 60);
            float seconds = Mathf.RoundToInt(this.time % 60);


            this.lblTrys.gameObject.GetComponent<TextMeshProUGUI>().text = "Shots : " + this.trys;
            this.lblTime.gameObject.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " : " + seconds.ToString();
        }
    }

    public void AddTry()
    {
        this.trys++;
    }

    public void EndGame()
    {
        this.lblTrys.SetActive(false);
        this.lblTime.SetActive(false);
        this.inGame = false;
    }

    public void StartGame()
    {
        this.lblTrys.SetActive(true);
        this.lblTime.SetActive(true);
        this.inGame = true;

    }

    public void NextLevel()
    {
        this.actualLevel++;
        if (this.actualLevel > this.LevelAmount)
        {
            SceneManager.LoadScene("End");
            this.EndGame();
        }
        else
        {
            SceneManager.LoadScene("level" + this.actualLevel);
            this.StartGame();
        }
    }

}

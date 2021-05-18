using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool End = false;

    private List<string> levels = new List<string>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void RandomizeLevels()
    {
        for (int i = 0; i < this.LevelAmount; i++)
            this.levels.Add("level" + i);

        this.levels = this.levels.OrderBy(x => Random.value).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.ResetValues();
        this.EndGame();
        this.RandomizeLevels();
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
        else if (this.End)
        {
            GameObject.FindGameObjectWithTag("finalTimer").GetComponent<TextMeshProUGUI>().text = "TIME : " + this.time;
            GameObject.FindGameObjectWithTag("finalShots").GetComponent<TextMeshProUGUI>().text = "SHOTS : " + this.trys;
        }
    }

    public void AddTry()
    {
        if (this.inGame)
        {
            this.trys++;
        }
    }

    public void EndGame()
    {
        this.lblTrys.SetActive(false);
        this.lblTime.SetActive(false);
        this.inGame = false;
    }

    private void ResetValues()
    {
        this.trys = 0;
        this.time = 0.0f;
        this.actualLevel = -1;
    }

    public void StartGame()
    {
        this.lblTrys.SetActive(true);
        this.lblTime.SetActive(true);
        this.inGame = true;
        this.End = false;
    }

    public void Restart()
    {
        this.ResetValues();
        this.StartGame();
    }

    public void NextLevel()
    {
        if (this.actualLevel >= 999)
        {
            this.RandomizeLevels();
            this.Restart();
        }

        this.actualLevel++;

        if (this.actualLevel >= this.LevelAmount)
        {
            SceneManager.LoadScene("End");
            this.actualLevel = 999;
            this.End = true;
            this.EndGame();
        }
        else
        {
            SceneManager.LoadScene(this.levels[this.actualLevel]);
            this.StartGame();
        }        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(this.levels[this.actualLevel]);
        this.trys += 5;
    }

}

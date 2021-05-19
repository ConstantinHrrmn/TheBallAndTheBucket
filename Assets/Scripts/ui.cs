using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{

    [HideInInspector] public int trys;
    [HideInInspector] public float time;

    public GameObject lblTrys;
    public GameObject lblTime;
    public GameObject lblPause;

    private GameObject gamebase;
    private GameObject camera;

    public int LevelAmount;
    private int actualLevel;
    private int maxRotationCamera = 180;
    private float actualCameraRotation = 0;
    public float cameraRotationSpeed; 

    private bool inGame = false;
    private bool End = false;
    private bool upsideDown = false;
    private bool turningCamera = false;

    private List<string> levels = new List<string>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.lblPause.SetActive(false);
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

        this.camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        // Est-ce que le jeu est en cours ou non
        if (this.inGame)
        {
            // On ajoute le temps de jeu à la variable 
            this.time += Time.deltaTime;

            // On créer les variables de minutes et secondes pour les afficher en haut de l'écran
            float minutes = Mathf.Floor(this.time / 60);
            float seconds = Mathf.RoundToInt(this.time % 60);


            this.lblTrys.gameObject.GetComponent<TextMeshProUGUI>().text = "Shots : " + this.trys;
            this.lblTime.gameObject.GetComponent<TextMeshProUGUI>().text = minutes.ToString("00") + " : " + seconds.ToString("00");
        }
        else if (this.End)
        {
            GameObject.FindGameObjectWithTag("finalTimer").GetComponent<TextMeshProUGUI>().text = "TIME : " + this.time;
            GameObject.FindGameObjectWithTag("finalShots").GetComponent<TextMeshProUGUI>().text = "SHOTS : " + this.trys;
        }

        if (GameObject.Find("GameBase") != null)
            this.gamebase = GameObject.Find("GameBase");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.Pause();
            this.gamebase.SetActive(!this.gamebase.activeSelf);
        }

        if (this.turningCamera)
        {
            this.actualCameraRotation += this.cameraRotationSpeed * Time.deltaTime;
            Debug.Log(this.actualCameraRotation);

            if (this.actualCameraRotation > this.maxRotationCamera)
            {
                Debug.Log("MAX ROTATION");
                this.camera.transform.rotation = new Quaternion(0, 0, (this.upsideDown ? 0: 180), 0);
                this.upsideDown = !this.upsideDown;
                this.turningCamera = false;
                this.actualCameraRotation = 0;
            }
            else
            {
                this.camera.transform.Rotate(new Vector3(0, 0, this.cameraRotationSpeed * Time.deltaTime));
            }
        }
    }

    public void AddTry()
    {
        if (this.inGame)
        {
            this.trys++;
        }
    }

    public void Pause()
    {
        this.inGame = !this.inGame;
        this.lblPause.gameObject.SetActive(!this.lblPause.gameObject.activeSelf);
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
        if (this.End)
        {
            SceneManager.LoadScene("End");
        }

        else if(this.actualLevel < 0){
            SceneManager.LoadScene("MainMenu");
        }

        else
        {
            SceneManager.LoadScene(this.levels[this.actualLevel]);
        }
        
        this.trys += 5;
    }

    public void ChangeRotation()
    {
        this.turningCamera = !this.turningCamera;
    }

}

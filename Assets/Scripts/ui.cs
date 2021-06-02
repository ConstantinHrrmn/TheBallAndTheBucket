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

    #region Public variables
    public GameObject lblTrys;
    public GameObject lblTime;
    public GameObject lblPause;
    public GameObject HomeBucket;

    public int LevelAmount;
    public int TutorielAmount;
    public float cameraRotationSpeed;

    public GameObject camera;
    public static ui i;
    #endregion

    #region Private variables
    private GameObject gamebase;
    private int actualLevel;

    private int maxRotationCamera = 180;
    private float actualCameraRotation = 0;
    

    private bool inGame = false;
    private bool End = false;
    private bool upsideDown = false;
    private bool turningCamera = false;
    private bool inTutorial = false;

    private System.Random rnd;

    private List<string> levels = new List<string>();
    private List<string> tutoriels = new List<string>();

    private Dictionary<int, string> SpecialCodes = new Dictionary<int, string>();

    private int MainMenuCode = 10000;
    private int LevelSelectorCode = 10001;
    private int EndCode = 10002;
    private int TutorielCode = 10003;

    private musicManager audio;
    private EffectManager effect;

    #endregion


    /// <summary>
    /// Ce produit une fois au moment du démarrage
    /// </summary>
    void Awake()
    {
        // On vérifie si l'objet à déjà été instancié ou non
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        // Gestion des menus et affichages divers
        this.lblPause.SetActive(false);
        this.HomeBucket.SetActive(false);

        this.SpecialCodes.Add(this.MainMenuCode, "MainMenu");
        this.SpecialCodes.Add(this.LevelSelectorCode, "levelSelector");
        this.SpecialCodes.Add(this.EndCode, "End");
        this.SpecialCodes.Add(this.TutorielCode, "Tutoriel");

        // Création d'un random
        this.rnd = new System.Random();

        this.audio = GameObject.Find("MusicPlayer").GetComponent<musicManager>();
        this.effect = GameObject.Find("EffectPlayer").GetComponent<EffectManager>();
    }

    /// <summary>
    /// Instantation des niveaux à charger dans la liste
    /// </summary>
    void InstantiateLevels()
    {
        this.levels.Clear();    // On vide la liste

        for (int i = 0; i < this.LevelAmount; i++)
            this.levels.Add("level" + i);

        // Afin de debugger pour vérifier les niveau ajoutés
        //this.DebugLevelList();
    }

    /// <summary>
    /// Instancie les niveaux de tutoriel
    /// </summary>
    void InstantiateTutoriels()
    {
        this.tutoriels.Clear();

        for (int i = 0; i < this.TutorielAmount; i++)
            this.tutoriels.Add("tutorial" + i);

        //this.DebugTutorialList();
    }

    /// <summary>
    /// Start avant le début du premier update
    /// </summary>
    void Start()
    {
        this.ResetValues();             // Remise à zéro des valeurs
        this.EndGame();                 // On met tous les paramètres de fin de jeu à 0
        this.InstantiateLevels();       // On charge les niveaux dans la liste
        this.InstantiateTutoriels();    // On charge les niveaux de tutoriels dans la liste

        // Le niveau actuel coresspond au niveau du code
        this.actualLevel = this.LevelSelectorCode;
    }

    /// <summary>
    /// Appeler à chaque frame
    /// </summary>
    void Update()
    {
        // On cherche une base de jeu si on en a pas 
        if (GameObject.Find("GameBase") != null)
            this.gamebase = GameObject.Find("GameBase");

        #region Game update
        // Est-ce que le jeu est en cours ou non
        if (this.inGame)
        {
            // On ajoute le temps de jeu à la variable 
            this.time += Time.deltaTime;

            // On créer les variables de minutes et secondes pour les afficher en haut de l'écran
            float minutes = Mathf.Floor(this.time / 60);
            float seconds = Mathf.RoundToInt(this.time % 60);

            // Met à jour les labels avec les données du jeu
            this.lblTrys.gameObject.GetComponent<TextMeshProUGUI>().text = "Shots : " + this.trys;
            this.lblTime.gameObject.GetComponent<TextMeshProUGUI>().text = minutes.ToString("00") + " : " + seconds.ToString("00");
        }
        // Si le jeu n'est plus en cours
        else if (this.End)
        {
            // On créer les variables de minutes et secondes pour les afficher en haut de l'écran
            float minutes = Mathf.Floor(this.time / 60);
            float seconds = Mathf.RoundToInt(this.time % 60);

            // Affichages des données finales dans les labels 
            GameObject.FindGameObjectWithTag("finalTimer").GetComponent<TextMeshProUGUI>().text = "TIME : " + minutes.ToString("00") + " : " + seconds.ToString("00");
            GameObject.FindGameObjectWithTag("finalShots").GetComponent<TextMeshProUGUI>().text = "SHOTS : " + this.trys;
        }
        #endregion

        #region Key Inputs
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Met le jeu en pause
            this.Pause();
            this.gamebase.SetActive(!this.gamebase.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            this.NextLevel();
        }
        #endregion

        #region Caméra rotation
        // Tourne la camera si le boolean est enclenché
        if (this.turningCamera)
        {
            // On ajoute une rotation à l'angle de la caméra
            this.actualCameraRotation += this.cameraRotationSpeed * Time.deltaTime;

            // Vérififcation dans l'angle de la caméra par rapport à l'angle max
            if (this.actualCameraRotation > this.maxRotationCamera)
            {
                // Bloquage de la rotation à 0 ou 180 degérs
                this.camera.transform.rotation = new Quaternion(0, 0, (this.upsideDown ? 0 : 180), 0);

                // Remise à zéro des valeurs de rotation
                this.upsideDown = !this.upsideDown;
                this.turningCamera = false;
                this.actualCameraRotation = 0;
            }
            else
            {
                // Tourner la caméra
                this.camera.transform.Rotate(new Vector3(0, 0, this.cameraRotationSpeed * Time.deltaTime));
            }
        }
        #endregion
    }

    /// <summary>
    /// Ajoute un coup au joueur
    /// </summary>
    public void AddTry()
    {
        if (this.inGame)
        {
            this.trys++;
        }
    }

    /// <summary>
    /// Met le jeu en pause
    /// </summary>
    public void Pause()
    {
        this.inGame = !this.inGame;
        this.lblPause.gameObject.SetActive(!this.lblPause.gameObject.activeSelf);
        this.HomeBucket.gameObject.SetActive(!this.HomeBucket.gameObject.activeSelf);

        this.audio.Pause();
        this.effect.pause();
        
    }

    /// <summary>
    /// Termine la partie 
    /// </summary>
    public void EndGame()
    {
        this.lblTrys.SetActive(false);
        this.lblTime.SetActive(false);
        this.inGame = false;
    }

    /// <summary>
    /// Remet toutes les valeurs à défaut
    /// </summary>
    private void ResetValues()
    {
        this.trys = 0;
        this.time = 0.0f;
        this.actualLevel = -1;
    }

    /// <summary>
    /// Démarre la partie en affichant les informations nécéssaires
    /// </summary>
    public void StartGame(bool showlabels)
    {
        if (showlabels)
        {
            this.ActivateGameLabels();
        }

        this.inGame = true;
        this.End = false;

        if (!this.audio.isPlaying())
        {
            this.audio.invertPlays();
            this.audio.StartGame();
        }
    }

    /// <summary>
    /// Active les labels du jeu
    /// </summary>
    public void ActivateGameLabels()
    {
        this.lblTrys.SetActive(true);
        this.lblTime.SetActive(true);
    }

    /// <summary>
    /// Redemarre le jeu
    /// </summary>
    public void Restart()
    {
        this.ResetValues();
        this.StartGame(true);
    }

    /// <summary>
    /// Charge le niveau suivant
    /// </summary>
    public void NextLevel()
    {
        // La variable qui va charger le niveau 
        string levelName = "";

        // Vérification pour voir si le code est dans la liste des codes spéciaux
        if (this.SpecialCodes.ContainsKey(this.actualLevel))
        {
            // Assignation du nom de niveau en fonction du code
            levelName = this.SpecialCodes[this.actualLevel];

            // Si le niveau actuel correspond au menu principal
            if (this.actualLevel == this.MainMenuCode)
            {
                this.EndGame();
                this.End = false;
                this.actualLevel = this.LevelSelectorCode;
            }
            else if(this.actualLevel == this.LevelSelectorCode)
            {
                this.EndGame();
                this.End = false;
            }

            // Remise à 0 de la liste des niveaux et tutoriels 
            this.InstantiateLevels();
            this.InstantiateTutoriels();

        }
        else
        {
            this.actualLevel++;

            if (this.inTutorial)
            {
                if (this.actualLevel >= this.TutorielAmount)
                {
                    levelName = this.SpecialCodes[this.LevelSelectorCode];
                    this.inTutorial = false;
                    this.End = false;
                    this.EndGame();
                }
                else
                {
                    levelName = this.tutoriels[this.actualLevel];
                    this.StartGame(false);
                }
            }
            else
            {
                if (this.actualLevel >= this.LevelAmount || this.actualLevel >= this.levels.Count)
                {
                    levelName = (this.SpecialCodes[this.EndCode]);
                    this.actualLevel = this.MainMenuCode;
                    this.End = true;
                    this.EndGame();
                }
                else
                {
                    levelName = this.levels[this.actualLevel];
                    this.StartGame(true);
                }
            }


        }

        SceneManager.LoadScene(levelName);

        if (this.upsideDown && !this.turningCamera)
        {
            this.turningCamera = true;
            Debug.LogError("Camera was upside down");
        }
    }

    /// <summary>
    /// Recharge à nouveau le niveau
    /// </summary>
    public void ReloadLevel()
    {
        if (this.SpecialCodes.ContainsKey(this.actualLevel))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else
        {
            if (!this.inTutorial)
            {
                this.trys += 5;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }

    }

    /// <summary>
    /// Tourne la rotation de la camera
    /// </summary>
    public void ChangeRotation()
    {
        this.turningCamera = !this.turningCamera;
    }

    public void SelectGameMode(int code, int amount)
    {
        switch (code)
        {
            case 1:
                Debug.LogWarning("Random ALL");
                this.RandomLevels(-1);
                break;

            case 2:
                Debug.LogWarning("Random with " + amount);
                this.RandomLevels(amount);
                break;

            case 3:
                Debug.LogWarning("Order ALL");
                this.InOrderLevels(-1);
                break;

            case 4:
                Debug.LogWarning("Order with " + amount);
                this.InOrderLevels(amount);
                break;

            case 5:
                Debug.LogWarning("Tutorial");
                this.TutorielLevels();
                break;

            default:
                break;
        }
    }

    public void RandomLevels(int amount)
    {
        this.Shuffle();
        if (amount > 0)
        {
            List<string> selected = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                selected.Add(this.levels[i]);
            }
            this.levels = selected;
        }

        this.ResetValues();

        this.NextLevel();
    }

    public void DebugLevelList()
    {
        string str = "";
        str += "\n -------------------------------\n";
        foreach (string item in this.levels)
        {
            str += item + "\n";
        }

        Debug.Log(str);
    }

    public void DebugTutorialList()
    {
        string str = "";
        str += "\n -------------------------------\n";
        foreach (string item in this.tutoriels)
        {
            str += item + "\n";
        }

        Debug.Log(str);
    }

    public void Shuffle()
    {
        List<string> list = this.levels;

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        this.levels = list;
    }

    public void InOrderLevels(int amount)
    {
        if (amount > 0)
        {
            List<string> selected = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                selected.Add(this.levels[i]);
            }
            this.levels = selected;
        }

        this.ResetValues();

        this.NextLevel();
    }

    public void TutorielLevels()
    {
        this.inTutorial = true;
        this.ResetValues();
        this.NextLevel();
    }

    public void GoBack()
    {
        this.actualLevel = this.LevelSelectorCode;
        this.inGame = false;

        this.Pause();
        this.NextLevel();

        try
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().NewLevel();
        }
        catch
        {
            Debug.LogWarning("GameManager non trouvé");
        }
        
    }

}

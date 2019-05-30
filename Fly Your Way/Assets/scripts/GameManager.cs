using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class GameManager : MonoBehaviour {

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;



	public static GameManager Instance;

	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countdownPage;
    public GameObject helpPage;
    public GameObject pauseButton;
	public Text scoreText;
    public PipeSpawner pipeSpawner;
    public Text gameOverScore;
    public TapController TapController;
    public GameObject NewHighscore;
    
    
    private string gameID = "3128029";
    private bool testmode = false;
    private bool HighscoreIsPlayed;
    private string video_ad = "video";

    public int score = 0;
    bool gameOver = true;

    


    //Einbindung der Werbung von Unity
    private void Start()
    {
       Advertisement.Initialize(gameID, testmode);
        
    }

   
    enum PageState {
		None,
		Start,
		Countdown,
		GameOver
	}


	public bool GameOver { get { return gameOver; } }

	void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
		}
		else {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void OnEnable() {
		TapController.OnPlayerDied += OnPlayerDied;
		TapController.OnPlayerScored += OnPlayerScored;
		CountdownText.OnCountdownFinished += OnCountdownFinished;
	}

	void OnDisable() {
		TapController.OnPlayerDied -= OnPlayerDied;
		TapController.OnPlayerScored -= OnPlayerScored;
		CountdownText.OnCountdownFinished -= OnCountdownFinished;
	}

	void OnCountdownFinished() {
		SetPageState(PageState.None);
		OnGameStarted();
		score = 0;
		gameOver = false;
        HighscoreIsPlayed = false;
	}

    IEnumerator NewHighscoreReached()               
    {

        if(HighscoreIsPlayed == false)
        {
            NewHighscore.SetActive(true);
            yield return new WaitForSeconds(2);
            NewHighscore.SetActive(false);
            HighscoreIsPlayed = true;
        }
        

    }
    public void OnPlayerScored()
    {

        if (TapController.pointsMultiplier == true)
        {
            score = score + 3;
        }
        else
        {
            score++;
        }
        int savedScore1 = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore1)
        {
            StartCoroutine(NewHighscoreReached());
        }
        scoreText.text = score.ToString();
        if (PipeSpawner.shiftSpeed < PipeSpawner.maxSpeed)                        // Max Speed of the Shift
        {
            //Aufrufen der Geschiwindigkeitserhöhung
            SpeedUp();
            SpeedUpPowerUp();
        }
    }

                                                                                 
    public void SpeedUp()                                                         // Erhöhen der Geschwindigkeit alle 10 Punkte um 0,5
    {
        if (score % 10 == 0)
        {
            PipeSpawner.shiftSpeed = PipeSpawner.shiftSpeed + PipeSpawner.speedUp;
            Debug.Log("Speed set to: " + PipeSpawner.shiftSpeed);                     //Log Ausgabe der Geschwindigeit
        }

    }

    public void SpeedUpPowerUp()                                                         // Erhöhen der Geschwindigkeit alle 10 Punkte um 0,5
    {
        if (score % 10 == 0)
        {
            PowerUpSpawner.shiftSpeed = PowerUpSpawner.shiftSpeed + PowerUpSpawner.speedUp;
            Debug.Log("PowerUp Speed set to: " + PowerUpSpawner.shiftSpeed);                     //Log Ausgabe der Geschwindigeit
        }

    }


    public void OnPlayerDied() {
        gameOverScore.text = score.ToString();                                    // Set score to GameOverScore
        gameOver = true;

                                                                                    // Reset Shiftspeed after death!
        PipeSpawner.shiftSpeed = 3.5f;
        PowerUpSpawner.shiftSpeed = 3.5f;

       //playTheAd();                                                              // Video Werbng abspielen

       // System.Random rnd = new System.Random();                                // Werbung nur abspielen wenn random Zahl gerade ist!
       // int adRnd = rnd.Next(1, 101);
       // if (adRnd % 2 == 0)
       // {
            playTheAd();
       // }
       // Debug.Log("Zufallszahl ist: " + adRnd);                                    // Log Ausgebe zur kontrolle der Funktion

        int savedScore = PlayerPrefs.GetInt("HighScore");
		if (score > savedScore) {
			PlayerPrefs.SetInt("HighScore", score);
		}
		SetPageState(PageState.GameOver);
	}

	void SetPageState(PageState state) {
		switch (state) {
			case PageState.None:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
                pauseButton.SetActive(true);
                helpPage.SetActive(false);
				break;
			case PageState.Start:
				startPage.SetActive(true);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
                pauseButton.SetActive(true);
                helpPage.SetActive(false);
                break;
			case PageState.Countdown:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(true);
                pauseButton.SetActive(true);
                helpPage.SetActive(false);
                break;
			case PageState.GameOver:
				startPage.SetActive(false);
				gameOverPage.SetActive(true);
				countdownPage.SetActive(false);
                pauseButton.SetActive(false);
                helpPage.SetActive(false);
                break;
		}
	}
	
	public void ConfirmGameOver() {
		SetPageState(PageState.Start);
		scoreText.text = "0";
		OnGameOverConfirmed();
	}

	public void StartGame() {
		SetPageState(PageState.Countdown);
        
	}

    //Play the Video AD
    void playTheAd()
    {

        //Video ready to play
        if (Advertisement.IsReady(video_ad))
        {
            Advertisement.Show();

        }

    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
        
    }

}

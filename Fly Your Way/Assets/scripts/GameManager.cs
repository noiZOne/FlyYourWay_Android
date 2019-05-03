using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;



	public static GameManager Instance;

	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countdownPage;
    public GameObject pauseButton;
	public Text scoreText;
    public PipeSpawner pipeSpawner;
    public Text gameOverScore;
    
    private string gameID = "3128029";
    private bool testmode = false;
    private string video_ad = "video";
    

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

	public int score = 0;
	bool gameOver = true;

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
	}


	public void OnPlayerScored() {
		score++;
        scoreText.text = score.ToString();
       
        SpeedUp();                                                                //Aufrufen der Geschiwindigkeitserhöhung
    }

                                                                                 
    public void SpeedUp()                                                         // Erhöhen der Geschwindigkeit alle 10 Punkte um 0,5
    {
        if (score % 10 == 0)
        {
            PipeSpawner.shiftSpeed = PipeSpawner.shiftSpeed + PipeSpawner.speedUp;
            Debug.Log("Speed set to: " + PipeSpawner.shiftSpeed);                     //Log Ausgabe der Geschwindigeit
        }

    }


    public void OnPlayerDied() {
        gameOverScore.text = score.ToString();                                        // Set score to GameOverScore
        gameOver = true;

                                                                            
        PipeSpawner.shiftSpeed = 3.5f;                                            // Reset Shiftspeed after death!

       //playTheAd();                                                              // Video Werbng abspielen

        System.Random rnd = new System.Random();                                // Werbung nur abspielen wenn random Zahl gerade ist!
        int adRnd = rnd.Next(1, 101);
        if (adRnd % 2 == 0)
        {
            playTheAd();
        }
        Debug.Log("Zufallszahl ist: " + adRnd);                                    // Log Ausgebe zur kontrolle der Funktion

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
				break;
			case PageState.Start:
				startPage.SetActive(true);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
                pauseButton.SetActive(true);
				break;
			case PageState.Countdown:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(true);
                pauseButton.SetActive(true);
                break;
			case PageState.GameOver:
				startPage.SetActive(false);
				gameOverPage.SetActive(true);
				countdownPage.SetActive(false);
                pauseButton.SetActive(false);
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

}

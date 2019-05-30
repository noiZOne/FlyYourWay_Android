using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerSelection : MonoBehaviour
{

 
    public Sprite  OriginalSprite;
    public Sprite DarkSprite;
    public Sprite PurpleSprite;
    public Sprite RosaSprite;
    public Sprite Dark101Sprite;
    public Sprite RosaHerzSprite;
    public GameObject DarkPlane;
    public GameObject DarkPlaneText;
    public GameObject PurplePlane;
    public GameObject PurplePlaneText;
    public GameObject PinkPlane;
    public GameObject PinkPlaneText;
    public GameObject HerzPlane;
    public GameObject HerzPlaneText;
    public GameObject HundretonePlane;
    public GameObject HundretonePlaneText;


    
    private SpriteRenderer playerRenderer;

    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (PlayerPrefs.GetInt("HighScore") >= 10)
        {
            DarkPlane.SetActive(false);
            DarkPlaneText.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 25)
        {
            PurplePlane.SetActive(false);
            PurplePlaneText.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 25)
        {
            PinkPlane.SetActive(false);
            PinkPlaneText.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 75)
        {
            HerzPlane.SetActive(false);
            HerzPlaneText.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 50)
        {
            HundretonePlane.SetActive(false);
            HundretonePlaneText.SetActive(false);
        }
    }

        public void originalPlane()
        {
            
                playerRenderer.sprite = OriginalSprite;
            

        }

        public void darkPlane()
        {
            if (PlayerPrefs.GetInt("HighScore") >= 10)
            {
            playerRenderer.sprite = DarkSprite;
            }
            

        }

        public void purplePlane()
        {
            if (PlayerPrefs.GetInt("HighScore") >= 25)
            {
                playerRenderer.sprite = PurpleSprite;
            }
        }

        public void rosaPlane()
        {
            if (PlayerPrefs.GetInt("HighScore") >= 25)
            {
            playerRenderer.sprite = RosaSprite;
            }
        }

        public void dark101Plane()
        {
            if (PlayerPrefs.GetInt("HighScore") >= 50)
            {
            playerRenderer.sprite = Dark101Sprite;
            }
        }
        public void rasaHerzPlane()
        {
            if (PlayerPrefs.GetInt("HighScore") >= 75)
            {
            playerRenderer.sprite = RosaHerzSprite;
            }
        }
    
}

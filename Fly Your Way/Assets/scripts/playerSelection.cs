using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [RequireComponent(typeof(Rigidbody2D))]
public class playerSelection : MonoBehaviour
{

   // Rigidbody2D rigidBody;
    public Sprite  OriginalSprite;
    public Sprite DarkSprite;
    public Sprite PurpleSprite;
    public Sprite RosaSprite;
    public Sprite Dark101Sprite;
    public Sprite RosaHerzSprite;

   // public GameObject Player;
    private SpriteRenderer playerRenderer;

    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // playerRenderer.sprite = OriginalSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

        public void originalPlane()
        {
            if (playerRenderer.sprite != null)
            {
                playerRenderer.sprite = OriginalSprite;
            }

        }

        public void darkPlane()
        {
            if (playerRenderer.sprite != null)
            {
                playerRenderer.sprite = DarkSprite;
            }

        }

        public void purplePlane()
        {
            playerRenderer.sprite = PurpleSprite;
        }

        public void rosaPlane()
        {
            playerRenderer.sprite = RosaSprite;
        }

        public void dark101Plane()
        {
            playerRenderer.sprite = Dark101Sprite;
        }

        public void rasaHerzPlane()
        {
            playerRenderer.sprite = RosaHerzSprite;
        }

    
}

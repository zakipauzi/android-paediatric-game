using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TracerMinigameScript : MonoBehaviour
{
    public Text scoreDisplay;
    public Animation animation;
    public Sprite glowing, notGlowing;
    public AudioClip popSound, winSound;

    private GameObject[] spots;
    private AudioSource popSource;
    private bool isGlowing;
    private int minigameScore = 0, spotsTapped = 0, max_spots = 10;
    
    public void Start()
    {
        spots = GameObject.FindGameObjectsWithTag("spot");

        SetPopSound();
        SetSpotRadius();

        isGlowing = false;
        SetGlowingState();
        InvokeRepeating("SetGlowingState", 1f, 1f);
    }
    
    // Checking whether a gameObject was clicked 
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            
            if (hitInfo && hitInfo.transform.gameObject.tag == "spot")
            {
                SpotTapped();
                // deactivate spot, so it can't be tapped again
                hitInfo.transform.gameObject.active = false;
            }
            
            UpdateScoreDisplay();
        }
    }

    // handles the situation if the user has tapped one of the targed areas
    public void SpotTapped()
    {
        minigameScore++;
        spotsTapped++;
        
        popSource.Play();

        if (spotsTapped == max_spots)
        {
            GameWon();
        }
    }

    private void GameWon()
    {
        AudioSource winSource = this.gameObject.GetComponent<AudioSource>();
        winSource.clip = winSound;
        winSource.Play(); 
        
        animation.Play("wellDone");

        StartCoroutine(Wait(3));
    }

    // sets the spots collider radius based on game difficulty
    // a higher radius value means less accuracy is needed for a successful spot tap
    private void SetSpotRadius()
    {
        bool easyMode = Difficulty.easyMode;
        float easyRadius = 0.6f, hardRadius = 0.4f;

        foreach (GameObject spot in spots)
        {
            CircleCollider2D collider = spot.GetComponent<CircleCollider2D>();

            if (easyMode)
            { collider.radius = easyRadius; }
            else
            { collider.radius = hardRadius; }
        }
    }

    // switch spot sprite between glowing and non-glowing
    private void SetGlowingState()
    {
        foreach (GameObject spot in spots)
        {
            SpriteRenderer sprite = spot.GetComponent<SpriteRenderer>();

            if (isGlowing)
            { sprite.sprite = glowing; }
            else
            { sprite.sprite = notGlowing; }
        }
        isGlowing = !isGlowing;
    }
    
    private void SetPopSound()
    {
        this.gameObject.AddComponent<AudioSource>();
        popSource = this.gameObject.GetComponent<AudioSource>();
        popSource.clip = popSound;
    }
    
    // wait then load scene; needed to show wellDone animation before proceeding
    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        LoadScene("10-cutscene");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.text = minigameScore.ToString() + " tapped!";
    }

}
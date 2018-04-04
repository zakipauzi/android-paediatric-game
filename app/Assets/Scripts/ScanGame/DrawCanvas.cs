using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawCanvas : MonoBehaviour {

    bool dif;
    public float brushsize = 20;

    private Texture2D newTex;
    public Texture2D oldTex;
    public UnityEngine.UI.Text textPercent;

    private bool easyMode = Difficulty.easyMode;
    private bool animationPlayed;
	private AudioSource successSource; 

    public int scanned = 0;

    Vector2 lastpos = new Vector2(-1,-1);

    bool complete;
    private int soundtimer = 0;
    public int soundmax = 1;

    void Start () {
        dif = Difficulty.easyMode;
        if (!dif) brushsize = 12;

        newTex = new Texture2D(oldTex.width,oldTex.height);       
        Color[] colors = oldTex.GetPixels(0, 0, oldTex.width, oldTex.height);
        newTex.SetPixels(colors);
        animationPlayed = false;
		successSource = GameObject.Find("WellDone").GetComponent<AudioSource> ();
		successSource.playOnAwake = false;
    }


    void Update () {
        
        if (Input.GetMouseButton(0))
        {            
            Vector2 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickedPosition, -Vector2.up);
            if (hit)
            {
                Vector2 hitPoint = this.transform.InverseTransformPoint(hit.point);
                GetComponent<AudioSource>().volume = 0.5f;
                BrushPlace(hitPoint);
                lastpos = hitPoint;
                UpdatePercent();
            }
        }
        else
        {
            GetComponent<AudioSource>().volume -= 0.050f;
        }

	}

    

	public void BrushPlace(Vector2 hp)
    {
        float fy = hp.y * 100 + newTex.height / 2;
        float fx = hp.x * 100 + newTex.width / 2;
        int xx = (int)fx;
        int yy = (int)fy;
        int dx = 0;
        int dy = 0;
        for (int x = -(int)brushsize; x < brushsize; x++)
        {
            dx = Mathf.Clamp(xx + x,0,newTex.width-1);
            for (int y = -(int)brushsize; y < brushsize; y++)
            {
                dy = Mathf.Clamp(yy + y, 0, newTex.height -1);     
                if (newTex.GetPixel(dx, dy) != Color.clear)
                {
                    newTex.SetPixel(dx, dy, Color.clear);                                    
                    scanned++;
                }
                                            
            }
        }
        newTex.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
        GetComponent<AudioSource>().volume += 0.03f;
    }

    private void OnMouseUp()
    {
        lastpos = new Vector2(-1, -1);
    }


    
    private void UpdatePercent()
    {        
        int percent = scanned / 512;
        percent *= 100;
        percent /= 300;
        if (percent < 99)
        {
            textPercent.text = "Percent Scanned: " + percent + "%";
        }
        else
        {
            textPercent.text = "Scan Complete!";

            if(!animationPlayed) {
                PlayAnimation();
            }

            Invoke("NextScene", 3);
        }
        
    }

    void PlayAnimation()
    {
        Animation animation = GameObject.Find("WellDone").GetComponent<Animation>();
        animation.Play("great");
		successSource.Play ();
        animationPlayed = true;
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

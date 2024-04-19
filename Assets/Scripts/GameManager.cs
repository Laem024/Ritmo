using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatsScroller theBS;
    public int currentScore;

    
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;


    public int currentMultiplier;
    public int multiplierTacker;
    public int[] multiplierThersholds;
    public Text scoreText;
    public Text multiText;

    public float totalNote;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedtHits;

    public GameObject resultScrren;
    public Text percentHitsText, normalHitsText, goodHitsText, perfectHitsText, missedHitsText, rankText, finalScoretext;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNote = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }else
        {
            if(!theMusic.isPlaying && !resultScrren.activeInHierarchy)
            {
                resultScrren.SetActive(true);

                normalHitsText.text = normalHits.ToString();
                goodHitsText.text = goodHits.ToString();
                perfectHitsText.text = perfectHits.ToString();
                missedHitsText.text = missedtHits.ToString();

                float totalHits = normalHits + goodHits + perfectHits;
                float percentHit = (totalHits/totalNote) *100f;

                percentHitsText.text = percentHit.ToString("F1")+"%";
            }
        }
    }

    public void NoteHit()
    {
        //Debug.Log("Hit on time");

        if (currentMultiplier - 1 < multiplierThersholds.Length)
        {

            multiplierTacker++;

            if (multiplierThersholds[currentMultiplier - 1] <= multiplierTacker)
            {
                multiplierTacker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplicador: x" + currentMultiplier;


        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }


    public void NoteMissed()
    {
        //Debug.Log("Missed note");
        currentMultiplier = 1;
        multiplierTacker = 0;
        multiText.text = "Multiplicador: x" + currentMultiplier;
        missedtHits++;
    }
}

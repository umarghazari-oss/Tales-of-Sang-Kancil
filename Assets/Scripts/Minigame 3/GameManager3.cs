using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(-100)]
public class GameManager3 : MonoBehaviour
{
    public static GameManager3 Instance { get; private set; }

    public Dog[] dogs;
    public SangKancil sangKancil;
    public Transform cucumbers;

    public GameObject winUI;
    public GameObject gameOverUI;
    public GameObject panelUI;
    public GameObject instructionsUI;
    public TextMeshProUGUI livesText;
    public Image livesImage;
    public Sprite noLives;
    public ScoreManager scoreManager;
    public CutsceneManager3 cm3;

    public AudioSource scoreSounds;
    public AudioClip cucumbSound;

    public float timeToRespawn = 3f;
    public float currentRespawnTime = 0f;
    public bool respawnTimerActive = false;   

    public int livesCount = 3;
    private int time;


    private void Awake()
    {
        scoreSounds = GetComponent<AudioSource>();
        scoreManager = FindFirstObjectByType<ScoreManager>();

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetLives(3);
    }
    public void GameOver()
    {
        panelUI.SetActive(true);
        gameOverUI.SetActive(true);
        for (int i = 0; i < dogs.Length; i++)
        {
            dogs[i].gameObject.SetActive(false);
        }
        sangKancil.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void Victory()
    {
        panelUI.SetActive(true);
        winUI.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Died()
    {
        sangKancil.gameObject.SetActive(false);
        SetLives(livesCount - 1);
        scoreManager.scoreCount = Mathf.Max(0, scoreManager.scoreCount - 30);

        if (livesCount > 0)
        {           
            Invoke(nameof(ResetState), 1.5f);
        }
        else
        {           
            GameOver();
            if (livesImage != null && noLives != null)
            {
                // Assign the new sprite to the Image component's sprite property
                livesImage.sprite = noLives;
                Debug.Log("Lives UI changed");
            }
            else
            {
                Debug.LogError("Lives UI unchanged");
            }
            
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    private void SetLives(int lives)
    {
        livesCount = lives;
        livesText.text = lives.ToString() + " X";
    } 

    public void NewRound()
    {
        foreach(Transform cucumber in this.cucumbers)
        {
            cucumber.gameObject.SetActive(true);
        }

        ResetState();
    }
    public void ResetState()
    {
        for (int i = 0; i < this.dogs.Length; i++)
        {
            this.dogs[i].ResetState();
        }

        sangKancil.ResetState();
    }

    public void CucumberEaten(CucumberBehavior cucumber)
    {
        cucumber.gameObject.SetActive(false);
        scoreManager.UpdateScore(10);

        if (!HasRemainingCucumbers())
        {
            cm3.endingCutscene = true;
        }
    }

    private bool HasRemainingCucumbers()
    {
        Debug.Log("keeping track of cucumbers");
        foreach(Transform cucumber in cucumbers)
        {
            if (cucumber.gameObject.activeSelf)
            {
                return true;
            }           
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance { get; private set; }

    public GameObject winUI;
    public GameObject gameOverUI;
    public GameObject panelUI;
    public GameObject instructionsUI;
    public TextMeshProUGUI livesText;
    public Image livesImage;
    public Sprite noLives;
    

    [SerializeField] public float timeToRespawn = 3f;
    [SerializeField] public float currentRespawnTime = 0f;
    public bool respawnTimerActive = false;
    public bool lilyPadDestroyed = false;

    public int livesCount = 3;

    private PlayerMovement1 pm1;
    public LilypadBehavior lpb; 

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
        pm1 = FindFirstObjectByType<PlayerMovement1>();
        lpb = GetComponent<LilypadBehavior>();   
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lilyPadDestroyed && respawnTimerActive)
        {
            currentRespawnTime += Time.deltaTime;

            if (currentRespawnTime >= timeToRespawn)
            {
                Debug.Log("Respawn times up");
                RespawnLilypad();
                StopRespawnLPTimer();
                lilyPadDestroyed = false;
            }
        }
    }

    private void NewGame()
    {
        SetLives(3);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        panelUI.SetActive(true);
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Victory()
    {
        Debug.Log("Victory");
        panelUI.SetActive(true);
        winUI.SetActive(true);      
        Time.timeScale = 0;
    }

    public void Respawn()
    {
        pm1 = GetComponent<PlayerMovement1>();

        StopAllCoroutines();
    }

    public void Died()
    {
        SetLives(livesCount - 1);

        if (livesCount > 0)
        {           
            Invoke(nameof(Respawn), 1f);
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

    void RespawnLilypad()
    {
        if (lpb.lilyPad != null)
        {
            lpb.lilyPad.SetActive(true);
        }
        StopRespawnLPTimer();
    }

    public void StartRespawnLPTimer()
    {
        currentRespawnTime = 0f; // Reset the timer
        respawnTimerActive = true;
        Debug.Log("Respawn timer started!");
    }

    void StopRespawnLPTimer()
    {
        respawnTimerActive = false;
        Debug.Log("Respawn timer stopped!");
    }
}

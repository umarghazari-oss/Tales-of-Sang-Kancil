using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }

    public GameObject[] spawnObstacles;
    public GameObject spawnPoint;
    public float timer;  

    public float speedMultiplier;
    public float distance;
    private int distanceInt;

    public GameObject winUI;
    public GameObject gameOverUI;
    public GameObject panelUI;
    public GameObject instructionsUI;

    public TextMeshProUGUI livesText;
    public Image livesImage;
    public Sprite noLives;
    public PlayerMovement2 pm2;
    public CutsceneManager2 cm2;

    public TextMeshProUGUI distanceText;


    public int livesCount = 3;

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
        livesImage = FindFirstObjectByType<Image>();
        Time.timeScale = 1;
    }

    private void Start()
    {
        NewGame();

    }

    private void NewGame()
    {
        SetLives(3);
    }
    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime * 0.1f;

        timer += Time.deltaTime;

        distance += Time.deltaTime * 0.8f;
        distanceInt = Mathf.RoundToInt(distance);
        distanceText.text = "Distance: " + distanceInt.ToString() + " / 50 M";

        SpawnLoop();    

        if (distance > 50f)
        {
            Debug.Log("distance reached");
            cm2.endingCutscene = true;
        }
    }

    private void SpawnLoop()
    {
        float timeBetweenSpawns = Random.Range(2f, 3f);
        if (timer > timeBetweenSpawns)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = spawnObstacles[Random.Range(0, spawnObstacles.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, spawnPoint.transform.position, Quaternion.identity);       
    }

    public void GameOver()
    {
        panelUI.SetActive(true);
        gameOverUI.SetActive(true);
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
        SetLives(livesCount - 1);

        if (livesCount <= 0)
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

    private void SetLives(int lives)
    {
        livesCount = lives;
        livesText.text = lives.ToString() + " X";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager3 : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public Image introCutsceneComponent;
    public GameObject introCutsceneFrame;
    public Image endingCutsceneComponent;
    public GameObject endingCutsceneFrame;
    public GameObject dialogueBox; 
    public GameObject cutscenePanel;
    public Sprite[] introCutscenes;
    public Sprite[] endingCutscenes;
   
    public bool introCutscene;
    public bool endingCutscene;
    public bool gameWon = false;

    private int textIndex;
    private int introCutsceneIndex;
    private int endingCutsceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        introCutscene = true;
        endingCutscene = false;
        Time.timeScale = 0;
        StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began)
            {
                if (textComponent.text == lines[textIndex])
                {
                   NextLine();         
                }
                else
                {
                   StopAllCoroutines();
                   textComponent.text = lines[textIndex];
                }
            }        
        }

        else if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[textIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[textIndex];
            }
        }
        GameWon();

        if (endingCutscene)
        {
            Debug.Log("end flag active");
            Time.timeScale = 0;
            dialogueBox.SetActive(true);
            endingCutsceneFrame.SetActive(true);
            cutscenePanel.SetActive(true);
        }

        if (textIndex == 31)
        {
            endingCutscene = false;
            gameWon = true;
            dialogueBox.SetActive(false);
            endingCutsceneFrame.SetActive(false);
            cutscenePanel.SetActive(false);
        }
    }

    void StartCutscene()
    {
        textIndex = 0;
        introCutsceneIndex = 0;
        dialogueBox.SetActive(true);
        introCutsceneFrame.SetActive(true);
        cutscenePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[textIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if(introCutscene && textIndex < lines.Length - 1)
        {
            textIndex++;
            textComponent.text = string.Empty;
            NextCutscene();
            StartCoroutine(TypeLine());        
        }
        else if(endingCutscene && textIndex < lines.Length - 1)
        {      
            dialogueBox.SetActive(true);
            endingCutsceneFrame.SetActive(true);   
            textIndex++;
            textComponent.text = string.Empty;
            EndingCutscenes();
            StartCoroutine(TypeLine());
        }
    }

    void NextCutscene()
    {
        if (introCutscene && textIndex == 0)
        {
            introCutsceneIndex += 1;
            //cutsceneFrame.transform.localPosition = new Vector3(0f, 27f, 0f);
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 1)
        {
            introCutsceneIndex += 1;
            //cutsceneFrame.transform.localPosition = new Vector3(1f, -111f, 0f);
            //cutsceneFrame.transform.localScale = new Vector3(0.95f, 0.84f, 0.8f);
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 2)
        {
            introCutsceneIndex += 1;
            // cutsceneFrame.transform.localPosition = new Vector3(6.9f, -110f, 0f);
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 3)
        {
            ///cutsceneFrame.transform.localPosition = new Vector3(0f, -24f, 0f);
            introCutsceneIndex += 1;
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 4)
        {
            introCutsceneIndex += 1;
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 5)
        {
            introCutsceneIndex += 1;
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 6)
        {
            introCutsceneIndex += 1;            
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (introCutscene && textIndex == 7)
        {
            introCutsceneIndex += 1;
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
            AudioManager.Instance.ChangeToGameMusic();
        }

        else if (introCutscene && textIndex == 8)
        {
            introCutsceneIndex += 1;
            introCutsceneComponent.sprite = introCutscenes[introCutsceneIndex];
        }

        else if (textIndex == 9)
        {
            introCutscene = false;
            dialogueBox.SetActive(false);
            introCutsceneFrame.SetActive(false);
            endingCutsceneFrame.SetActive(false);
            cutscenePanel.SetActive(false);
            GameManager3.Instance.instructionsUI.SetActive(true);
            Debug.Log("intro false");
        }

    }

    void EndingCutscenes()
    {
        if (endingCutscene && textIndex == 9)
        {
            Time.timeScale = 0;
            endingCutsceneIndex +=1 ;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 11)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 13)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 15)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 17)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 18)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 19)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 20)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 26)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 27)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 28)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
        }

        else if (endingCutscene && textIndex == 29)
        {
            endingCutsceneIndex += 1;
            endingCutsceneComponent.sprite = endingCutscenes[endingCutsceneIndex];
            AudioManager.Instance.ChangeToEndMusic();
        } 
    }

    private void GameWon()
    {
        if (gameWon)
        {
            GameManager3.Instance.Victory();
        } 
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager Instance = null;
    private AudioSource audioSource;
    public AudioClip introTheme;
    public AudioClip gameTheme;
    public AudioClip endTheme;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }
    
    public void ChangeToGameMusic()
    {
        audioSource.clip = gameTheme;
        audioSource.Play();
    }

    public void ChangeToEndMusic()
    {
        audioSource.clip = endTheme;
        audioSource.Play();
    }
}

using UnityEngine;

public class InstructionMenu : MonoBehaviour
{
    private PlayerMovement1 pm1;
    public GameObject kancil;
    private void Awake()
    {
        pm1 = FindFirstObjectByType<PlayerMovement1>();
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        if (kancil != null)
        {
            pm1.enabled = true;
        }
    }
}

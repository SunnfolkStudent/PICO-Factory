using UnityEngine;
using UnityEngine.SceneManagement;

public class HCController : MonoBehaviour
{
    public static bool Hardcore;
    public static int Lives;
    private bool gainedLives;
    public GameObject ui;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hardcore = false;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (!gainedLives)
            {
                gainedLives = true;
                Lives = 3;
            }
        }
        if (SceneManager.GetActiveScene().name == "Game Over Screen")
        {
            Destroy(gameObject);
        }
    }

}

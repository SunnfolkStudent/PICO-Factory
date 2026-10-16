using UnityEngine;
using UnityEngine.SceneManagement;

public class HCController : MonoBehaviour
{
    public static bool Hardcore;
    public static int Lives;
    public GameObject ui;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hardcore = false;
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level 1")
        {
            Lives = 3;
            if (Hardcore)
            {
                ui = GameObject.FindGameObjectWithTag("UI");
            }
        }
    }
}

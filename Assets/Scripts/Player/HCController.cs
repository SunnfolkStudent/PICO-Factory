using UnityEngine;

public class HCController : MonoBehaviour
{
    public static bool Hardcore;
    public static int Lives;
    private GameObject _ui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

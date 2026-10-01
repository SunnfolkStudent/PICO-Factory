using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirstLevelScript : MonoBehaviour
{ 
    void Start()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

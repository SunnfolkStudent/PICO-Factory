using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject Poster;
    public SpriteRenderer control_Poster;

    private void Awake()
    {
        control_Poster  = Poster.GetComponent<SpriteRenderer>();
        control_Poster.enabled = false;
    }
    // What am I even doing here? VVV 
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Saved Scene"));
    }

    public void Controls()
    {
        if (!control_Poster.enabled)
        {
            control_Poster.enabled = true;
        }

        if (control_Poster.enabled)
        {
            control_Poster.enabled = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public EventSystem eventSystem;
    public RectTransform arrow;
    
    // Update is called once per frame
    void Update()
    {
        print(eventSystem.currentSelectedGameObject.name);
        
        if (eventSystem.currentSelectedGameObject.transform.position.y != arrow.position.y)
        {
            arrow.position = new Vector2(arrow.position.x, eventSystem.currentSelectedGameObject.transform.position.y);
        }

        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(arrow.gameObject);
        }
    }
}

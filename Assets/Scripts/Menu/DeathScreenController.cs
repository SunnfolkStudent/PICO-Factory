using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public EventSystem eventSystem;
    public RectTransform arrow;

    public void Retry()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    // Update is called once per frame
    void Update()
    {
        
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

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
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

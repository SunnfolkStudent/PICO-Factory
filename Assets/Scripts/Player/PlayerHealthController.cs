using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public Image[] hearts;
    public Sprite heartEmpty;

    public void Awake()
    {
        heartEmpty = find
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            hearts[1].enabled = false;
            hearts[2].enabled = false;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < HCController.Lives)
            {
                hearts[i].sprite = find:
            }
            else
            {
                
            }
        }
    }
}
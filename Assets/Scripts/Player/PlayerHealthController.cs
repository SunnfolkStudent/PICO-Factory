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
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            hearts[0].enabled = false;
            hearts[1].enabled = false;
            hearts[2].enabled = false;
            if (HCController.Hardcore)
            {
                hearts[0].enabled = true;
            }
        }
        else
        {
            if (HCController.Hardcore)
            {
                for (int i = 0; i < hearts.Length; i++)
                {
                    if (i < HCController.Lives)
                    {
                        hearts[i].enabled = true;
                    }
                    else
                    {
                        hearts[i].enabled = false;
                    }
                }
            }
            else
            {
                hearts[0].enabled = false;
                hearts[1].enabled = false;
                hearts[2].enabled = false;
            }
        }
    }
}
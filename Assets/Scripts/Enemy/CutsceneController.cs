using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    private GameObject _player;
    private GameObject _cutscenegneiss;
    void Awake()
    {
        _player = GameObject.Find("Gneiss");
        _cutscenegneiss = GameObject.Find("Gneiss Wakes Up");
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            _player.SetActive(false);
            StartCoroutine(OpeningCutscene());
        }
    }

    private IEnumerator  OpeningCutscene()
    {
        yield return new WaitForSeconds(10.433f);
        _player.SetActive(true);
        _cutscenegneiss.SetActive(false);
    }
}

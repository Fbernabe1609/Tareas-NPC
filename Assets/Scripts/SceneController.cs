using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    public int difficulty;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnEasyButton()
    {
        difficulty = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMediumButton()
    {
        difficulty = 3;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnHardButton()
    {
        difficulty = 10;
        SceneManager.LoadScene("SampleScene");
    }
}

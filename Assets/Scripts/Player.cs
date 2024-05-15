using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private CharacterController controller;
    public TMP_Text timeText;
    public GameObject timePanel;
    public GameObject scorePanel;
    public TMP_Text scoreText;
    private float timeElapsed = 0;

    void Start()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        timeText.text = "Time: " + timeElapsed.ToString("0.00") + " seconds";
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, 0, moveVertical);
        movement = transform.TransformDirection(movement);
        controller.Move(movement * speed * Time.deltaTime);

        if (moveHorizontal != 0)
        {
            transform.Rotate(0, moveHorizontal * rotationSpeed * Time.deltaTime, 0); // Use rotationSpeed here
        }
    }

    public void OnEnemyAttack()
    {
        Time.timeScale = 0;

        timePanel.SetActive(false);
        scorePanel.SetActive(true);

        scoreText.text = "Score: " + timeElapsed.ToString("0.00") + " seconds";
    }

    public void OnGoToInitialGameSceneButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("InitalGameScen");
    }
}
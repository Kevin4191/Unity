using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 7;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject BackgroundObject;
    public GameObject LvlStart2Object;
    public GameObject LvlStart3Object;
    public GameObject StartAgainButton;
    public float bounciness = 7;
    private Vector3 startPosition = new(0.0f, 0.5f, 0.0f);
    void Start()
    {
        winTextObject.SetActive(false);
        StartAgainButton.SetActive(false);
        BackgroundObject.SetActive(false);
        LvlStart2Object.SetActive(false);
        LvlStart3Object.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        Time.timeScale = 1;
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        countText.text = "Count: " + count.ToString();
        if (activeScene.name == "minigame1")
        {
            if(count >= 5)
            {
                NextLevelScreen();
                Invoke(nameof(NextLevel), 2f);
            }
        } else if(activeScene.name == "minigame")
        {
            if(count >= 7)
            {
                NextLevelScreen();
                Invoke(nameof(NextLevel), 2f);
            }
        } else if(activeScene.name == "minigame2")
        {
            if(count >= 16)
            {
                BackgroundObject.SetActive(true);
                winTextObject.SetActive(true);
                StartAgainButton.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    void NextLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "minigame1")
        {
            SceneManager.LoadScene("minigame");
        } else if(activeScene.name == "minigame")
        {
            SceneManager.LoadScene("minigame2");
        }
        
    }

    void NextLevelScreen()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "minigame1")
        {
            BackgroundObject.SetActive(true);
            LvlStart2Object.SetActive(true);
        } else if(activeScene.name == "minigame")
        {
            BackgroundObject.SetActive(true);
            LvlStart3Object.SetActive(true);
        }

    }

    

    private void FixedUpdate()
    {
        Vector3 movement = new(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        float yPosition = rb.transform.position.y;
        if(yPosition < -5f)
        {
            rb.AddForce(-rb.velocity);
            rb.velocity = Vector3.zero;
            rb.transform.position = startPosition;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("BouncePad"))
        {
            rb.AddForce(Vector3.up * bounciness, ForceMode.Impulse);
        }
    }
}


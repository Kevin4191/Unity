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
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject BackgroundObject;
    public GameObject LvlStart2Object;
    public GameObject StartAgainButton;
    void Start()
    {
        winTextObject.SetActive(false);
        StartAgainButton.SetActive(false);
        BackgroundObject.SetActive(false);
        LvlStart2Object.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        Scene activeScene = SceneManager.GetActiveScene();
        if (count >= 5)
        {
            if (activeScene.name == "minigame1")
            {
                NextLevelScreen();
                Invoke("NextLevel", 2f);
            }
                
        }
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            StartAgainButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene("minigame");
    }

    void NextLevelScreen()
    {
        BackgroundObject.SetActive(true);
        LvlStart2Object.SetActive(true);
    }

    

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}


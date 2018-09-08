using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public AudioSource[] sound;

    public float speed;

    public Text countText;
    public Text winText;
    public Text goldCoinText;

    private Rigidbody rb;
    private int count;
    public int totalCount;

    bool hasCrossedFinishLine;
    bool collectedAllCoins;
    string timeString;

    private void Start()
    {
        sound = GetComponents<AudioSource>();
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winText.text = "";
        goldCoinText.text = "";
        hasCrossedFinishLine = false;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        timeString = string.Format("{0:N2}", Time.time);

        rb.AddForce(movement * speed);

        if ((transform.position.x < 230 && transform.position.y < -60) || 
            (transform.position.x >= 230 && transform.position.y < -160))
        {
            DeathSequence();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            count = count + 1;
            SetCountText();

            if (count == 50)
            {
                collectedAllCoins = true;
            }
        }

        if (other.gameObject.CompareTag("FinishLine") && !hasCrossedFinishLine)
        {
            speed = 0;
            winText.text = "Finish!";

            if (collectedAllCoins)
            {
                goldCoinText.text = "Achievement Unlocked! \r\n Gold Coin Connoisseur";
            }

            GameObject.Find("Canvas").GetComponent<AudioSource>().Pause();
            sound[1].Play();

            hasCrossedFinishLine = true;

            WriteString("You crossed the finish line in " + timeString + " seconds!");
            WriteString("You finished the course here: " + ReturnPosition());

            Invoke("OpenMainMenu", 5);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Death")
        {
            DeathSequence();
        }
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString() + "/" + totalCount;
    }

    public void DeathSequence()
    {
        WriteString("You died here: " + ReturnPosition() + "  :(");
        sound[0].Play();
        transform.position = new Vector3(-14, 3, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void WriteString(string String)
    {
        StreamWriter writer = new StreamWriter("test.txt", true);
        writer.WriteLine(String);
        writer.Close();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public string ReturnPosition()
    {
        Vector3 position = GetComponent<Transform>().position;
        string positionString = position.ToString();
        return positionString;
    }
}

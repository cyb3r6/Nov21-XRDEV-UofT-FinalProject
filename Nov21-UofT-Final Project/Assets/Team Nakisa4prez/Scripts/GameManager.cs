using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text TMPTime;
    private TMP_Text timeExpiryText;
    public int countHits;
    private float timeLeft = 30f; //1 minute ie. 60 seconds

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Timer
        TMPTime = GameObject.Find("TimeAmount").GetComponent<TMP_Text>();
        TMPTime.text = timeLeft.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //Update Level Timer
        if (timeLeft != 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateLevelTimer(timeLeft);
        }

        //if time expired, show expiry message and shutdown app
        if (timeLeft <= 0)
        {
            timeExpiryText = GameObject.Find("TimeExpiryMsg").GetComponent<TMP_Text>();
            timeExpiryText.text = "TIME EXPIRED";
            timeLeft = 0;
            UpdateLevelTimer(timeLeft);
            Invoke("ShutdownApp", 5f);
        }
    }

    private void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        TMPTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void ShutdownApp()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float? timer;

    public bool toggle;

    public float maxValue;
    
    public Text displayTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle)
        {
            timer = 0f;
            toggle = false;
        }
        if(timer.HasValue)
        {
            timer += Time.deltaTime;
            displayTimer.text = timer.Value.ToString("F2");
            if(timer.Value > maxValue)
            {
                timer = null;
            }
        }
    }
    
}

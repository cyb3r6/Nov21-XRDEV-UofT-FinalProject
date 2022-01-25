using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountDown : MonoBehaviour
{
  
  [SerializeField]
  public float myTimer = 30;
  public float realTimeToShow;
  public TMP_Text showTime;
  public bool kickoff = false;
  // Start is called before the first frame update
  void Start()
  {
  }
  // Update is called once per frame
  void Update()
  {
    if(kickoff == true)
    {
      StartTimer();
    }
    realTimeToShow = Mathf.Round(myTimer);
    //display my time
    showTime.text = realTimeToShow.ToString();
  }
  public void StartTimer()
  {
    kickoff = true;
    myTimer -= Time.deltaTime;
  }
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      kickoff = true;
    }
  }
}
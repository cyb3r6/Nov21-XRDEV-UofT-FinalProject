using UnityEngine;
using TMPro;

public class StackableCube : MoveableObject
{
    private TMP_Text timeExpiryText;
    private GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        grabbableRigidbody = GetComponent<Rigidbody>();
        grabbableRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //GameManager.countHits++;

        //if(GameManager.countHits == 3)
       // {
        //    timeExpiryText = GameObject.Find("TimeExpiryMsg").GetComponent<TMP_Text>();
//timeExpiryText.text = "YOU WIN!!!!";
      //  }

        //Destroy(other.gameObject);
        //Destroy(gameObject);
    }
}

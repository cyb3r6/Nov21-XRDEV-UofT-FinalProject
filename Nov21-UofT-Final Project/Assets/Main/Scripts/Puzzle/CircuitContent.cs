using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CircuitContent : MonoBehaviour
{
    [System.Serializable]
    public class Circuit
    {
        public string name;
        public string[] components;
        public int energy;
    }

    [System.Serializable]
    public class CircuitEvent : UnityEvent<Circuit> { };

    public Circuit[] circuits;
    public CircuitEvent OnCircuit;

    [Header("Effects")]
    public GameObject sparks;
    public Animator circuitAnimator;

    [Header("Audio")]
    public AudioSource circuitSoundSource;

    bool canComplete = false;

    private List<string> currentComponent = new List<string>();
    private int energy = 0;



    // Start is called before the first frame update
    void Start()
    {
        canComplete = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CircuitComponent cComponent = other.attachedRigidbody.GetComponentInChildren<CircuitComponent>();

        Vector3 contactPosition = other.attachedRigidbody.gameObject.transform.position;
        contactPosition.y = gameObject.transform.position.y;

        var spark = Instantiate(sparks, contactPosition, sparks.transform.rotation);
        Destroy(spark, 1);

        if (cComponent != null)
        {
            currentComponent.Add(cComponent.componentType);

        }
        else
        {
            // added an object that's not a component so we'll fail the circuit
            currentComponent.Add("Invald");

        }
    }

    public void ChangeVoltage(int voltage)
    {
        energy = voltage;
    }

    public void CircuitBuilding()
    {
        Debug.Log("circuit building");
        if (!canComplete)
        {
            return;
        }

        Circuit circuitCreated = null;

        foreach(Circuit cir in circuits)
        {
            //if (cir.energy != energy)
            //{
            //    continue;
            //}

            List<string> copyofCircut = new List<string>(currentComponent);

            int componentCount = 0;
            foreach(var x in cir.components)
            {
                if (copyofCircut.Contains(x))
                {
                    componentCount += 1;
                    copyofCircut.Remove(x);
                }
            }

            if(componentCount == cir.components.Length)
            {
                circuitCreated = cir;
                break;
            }
        }
        ResetCircuit();

        StartCoroutine(WaitForCircuitCompleted(circuitCreated));
    }

    public void ResetCircuit()
    {
        currentComponent.Clear();
    }

    private IEnumerator WaitForCircuitCompleted(Circuit circuit)
    {
        canComplete = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("circuit completed");
        OnCircuit.Invoke(circuit);
        canComplete = true;
    }
}

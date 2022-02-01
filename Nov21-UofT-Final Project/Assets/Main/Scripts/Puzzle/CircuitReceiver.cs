using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CircuitReceiver : MonoBehaviour
{
    public string[] acceptedType;

    public UnityEvent OnCircuitReceivedEvent;

   public void RecieveCircuit(string circuitType)
    {
        if (acceptedType.Contains(circuitType))
        {
            OnCircuitReceivedEvent.Invoke();
        }
    }
}

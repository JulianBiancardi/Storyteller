using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour
{
    void Start()
    {
    }

    public abstract void ReceiveDragOperation(GameObject dragObject, Container container);
    
}

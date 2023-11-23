using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour, Removable
{
    void Start()
    {
    }

    public abstract List<FrameResult> Compute();

    public void OnRemove()
    {
        Destroy(gameObject);
    }
}

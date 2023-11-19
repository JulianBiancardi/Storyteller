using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour
{
    void Start()
    {
    }

    public virtual FrameResult Compute(){
        return new FrameResult();
    }
    
}

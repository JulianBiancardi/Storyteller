using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    private Camera mainCamera;

    void Start(){
        mainCamera = Camera.main;
    }

    public void OnRemove(){
        
        int layerMask = 1 << LayerMask.NameToLayer("Character");
        layerMask |= 1 << LayerMask.NameToLayer("FrameHolder");

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if(hit.collider != null){
            Debug.Log("Hit " + hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<Removable>().OnRemove();
            Level.Instance.ComputeAll();
        }
    }
}

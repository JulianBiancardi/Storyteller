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
        Debug.Log("Remove");
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if(hit.collider != null){
            Debug.Log("Hit " + hit.collider.gameObject.name);
            Character character = hit.collider.gameObject.GetComponent<Character>();
            if(character != null){
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

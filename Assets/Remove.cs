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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if(hit.collider != null){
            Debug.Log("Hit " + hit.collider.gameObject.name);
            Character character = hit.collider.gameObject.GetComponent<Character>();
            if(character != null){
                character.container.RemoveCharacter();
                Level.Instance.ComputeAll();
            }
        }
    }
}

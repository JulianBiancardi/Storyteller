using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    Transform characterSpawn;
    GameObject currentCharacter = null;

    void Start()
    {
        characterSpawn = transform.Find("CharacterSpawn");
    }

    public void ReceiveDragOperation(GameObject dragObject){
        Selection selection = dragObject.GetComponent<Selection>();
        if(selection != null){
            GameObject objectToDrop = selection.getObjectToDrop();
            if(objectToDrop != null){
                GameObject droppedObject = Instantiate(objectToDrop);
                droppedObject.transform.parent = characterSpawn;
                droppedObject.transform.position = characterSpawn.position;
                droppedObject.transform.localScale = new Vector3(1, 1, 1);
                Destroy(dragObject);
            }
        }
    }

    public Character ReceiveSelection(Selection selection){
        Destroy(currentCharacter);
        GameObject objectToDrop = selection.getObjectToDrop();

        GameObject droppedObject = Instantiate(objectToDrop);
        droppedObject.transform.parent = characterSpawn;
        droppedObject.transform.position = characterSpawn.position;
        droppedObject.transform.rotation = characterSpawn.rotation;
        droppedObject.transform.localScale = new Vector3(1, 1, 1);
        currentCharacter = droppedObject;
        return droppedObject.GetComponent<Character>();
    }
}

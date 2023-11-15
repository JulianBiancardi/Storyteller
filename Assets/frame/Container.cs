using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    Transform characterSpawn;
    protected GameObject currentCharacter = null;
    protected CharacterType currentCharacterType = CharacterType.None;

    void Start()
    {
        characterSpawn = transform.Find("CharacterSpawn");
    }

    public virtual void ReceiveDragOperation(Selection selection){
        if(currentCharacterType == selection.character){
            Debug.Log("Same character");
            return;
        }

        GameObject objectToDrop = selection.getObjectToDrop();
        if(objectToDrop != null){
            currentCharacterType = selection.character;
            SpawnCharacter(objectToDrop);
        }
    }

    protected void SpawnCharacter(GameObject character){
        Destroy(currentCharacter);
        GameObject droppedObject = Instantiate(character);
        droppedObject.transform.parent = characterSpawn;
        droppedObject.transform.position = characterSpawn.position;
        droppedObject.transform.rotation = characterSpawn.rotation;
        droppedObject.transform.localScale = new Vector3(1, 1, 1);
        currentCharacter = droppedObject;
        currentCharacter.GetComponent<Character>().container = this;
        currentCharacter.GetComponent<Character>().characterType = currentCharacterType;
    }

    public void RemoveCharacter(){
        Destroy(currentCharacter);
        currentCharacterType = CharacterType.None;
        OnRemove();
    }

    public virtual void OnRemove(){

    }

    public bool IsEmpty(){
        return currentCharacterType == CharacterType.None;
    }
}

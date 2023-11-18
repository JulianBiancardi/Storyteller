using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    Transform characterSpawn;
    protected GameObject currentCharacter = null;
    protected Actor actor;

    void Start()
    {
        characterSpawn = transform.Find("CharacterSpawn");
        actor = null;
    }

    public void SetActor(Actor actor){
        this.actor = actor;
    }
    public Actor GetActor(){
        return actor;
    }

    public virtual void ReceiveDragOperation(Selection selection){
        if(actor == null){
            actor = selection.actor;
            SpawnCharacter(selection.getObjectToDrop());
            return;
        }

        if(actor.GetActorId() == selection.actor.GetActorId()){
            Debug.Log("Same character");
            return;
        }
        
        actor = selection.actor;
        SpawnCharacter(selection.getObjectToDrop());
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
    }

    public void RemoveCharacter(){
        Destroy(currentCharacter);
        currentCharacter = null;
        actor = null;
        OnRemove();
    }

    public virtual void OnRemove(){

    }

    public bool IsEmpty(){
        return actor == null;
    }
}

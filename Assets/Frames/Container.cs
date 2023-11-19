using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    Transform characterSpawn;
    protected GameObject currentCharacter = null;
    protected Actor actor;
    private AudioSource audioSource;
    public List<AudioClip> onPlaceSounds;
    public List<Container> connectedContainers;

    void Start()
    {
        characterSpawn = transform.Find("CharacterSpawn");
        actor = null;
        audioSource = GetComponent<AudioSource>();
    }
    public Actor GetActor(){
        return actor;
    }

    public virtual void ReceiveDragOperation(Selection selection){
        if(!IsEmpty() && selection.actor.GetActorId() == actor.GetActorId()){
            Debug.Log("Same character");
            return;
        }

        RemoveAlreadyPlacedActor(selection.actor.GetActorId());
        actor = selection.actor;
        SpawnCharacter(selection.getObjectToDrop());
    }

    private void RemoveAlreadyPlacedActor(ActorId actorId){
        foreach(Container container in connectedContainers){
            if(container.actor != null && container.actor.GetActorId() == actorId){
                Debug.Log("Actor already in other container");
                container.RemoveCharacter();
                break;
            }
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
        audioSource.clip = onPlaceSounds[Random.Range(0, onPlaceSounds.Count)];
        audioSource.Play();
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

    public void ChangeCharacterState(Feeling feeling){
        if(currentCharacter == null){
            return;
        }
        currentCharacter.GetComponent<Character>().ChangeFeeling(feeling);
    }
}

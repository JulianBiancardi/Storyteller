using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    Transform characterSpawn;
    protected GameObject currentCharacter = null;
    protected Actor actor;
    private AudioSource audioSource;
    public List<AudioClip> onPlaceSounds;
    private List<AudioClip> onRemoveSounds;
    public List<Container> connectedContainers;
    public bool facingLeft = false;

    void Start()
    {
        characterSpawn = transform.Find("CharacterSpawn");
        actor = null;
        audioSource = GetComponent<AudioSource>();

        onRemoveSounds = new List<AudioClip>();
        onRemoveSounds.Add(Resources.Load<AudioClip>("Sound/Sfx/removed_actor_01"));
    }

    public Actor GetActor(){
        return actor;
    }

    public virtual void ReceiveDragOperation(Selection selection){
        if(selection.IsSet()){
            Debug.Log("Dropped a set into container");
            return;
        }

        if(!IsEmpty() && selection.actor.GetActorId() == actor.GetActorId()){
            Debug.Log("Same character");
            return;
        }

        RemoveAlreadyPlacedActor(selection.actor.GetActorId());
        actor = selection.actor;
        SpawnCharacter();
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

    protected void SpawnCharacter(){
        Destroy(currentCharacter);
        
        GameObject character = CharacterFactory.CreateCharacter(actor.GetActorId(), this);
        character.transform.parent = characterSpawn;
        character.transform.position = characterSpawn.position;
        character.transform.localScale = new Vector3(1, 1, 1);

        if(facingLeft){
            character.GetComponent<Character>().FacingLeft();
        }
        audioSource.clip = onPlaceSounds[Random.Range(0, onPlaceSounds.Count)];
        audioSource.Play();
        currentCharacter = character;
    }

    public void RemoveCharacter(){
        Destroy(currentCharacter);
        currentCharacter = null;
        actor = null;
        audioSource.clip = onRemoveSounds[Random.Range(0, onRemoveSounds.Count)];
        audioSource.Play();
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

    public void UpdateCharacter(Event e){
        if(currentCharacter == null){
            return;
        }
        currentCharacter.GetComponent<Character>().Update(e);
    }
}

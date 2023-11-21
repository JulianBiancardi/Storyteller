using System.Collections.Generic;
using UnityEngine;

public struct CharacterSpec
{
    public Sprite tombSprite;
    public RuntimeAnimatorController animatorController;

    public CharacterSpec(Sprite tombSprite, RuntimeAnimatorController animatorController)
    {
        this.tombSprite = tombSprite;
        this.animatorController = animatorController;
    }
}

public static class CharacterFactory
{
    static readonly Dictionary<ActorId, CharacterSpec> charactersSpecs = new();

    static CharacterFactory(){
        Sprite adamTombSprite =  Resources.Load<Sprite>("Characters/Tomb/tomb_icon_adamgen_normal");
        Sprite eveTombSprite =  Resources.Load<Sprite>("Characters/Tomb/tomb_icon_evegen_normal");  
        RuntimeAnimatorController adamAnimator = Resources.Load<RuntimeAnimatorController>("Characters/adam");
        RuntimeAnimatorController eveAnimator = Resources.Load<RuntimeAnimatorController>("Characters/eve");

        charactersSpecs.Add(ActorId.Adam, new CharacterSpec(adamTombSprite, adamAnimator));
        charactersSpecs.Add(ActorId.Eve, new CharacterSpec(eveTombSprite, eveAnimator));
    }

    public static GameObject CreateCharacter(ActorId actorId, Container container){
        CharacterSpec characterInfo = charactersSpecs.GetValueOrDefault(actorId);
        GameObject characterPrefab = Level.Instance.characterPrefab;
        GameObject characterObject = GameObject.Instantiate(characterPrefab);

        characterObject.GetComponent<Animator>().runtimeAnimatorController = characterInfo.animatorController;

        Character character = characterObject.GetComponent<Character>();
        character.tombSprite = characterInfo.tombSprite;
        character.container = container;
        return characterObject;
    }	
}

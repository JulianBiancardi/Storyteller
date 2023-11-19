using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterInformation
{
    public Sprite toolSprite;
    public Sprite tombSprite;
    public RuntimeAnimatorController animatorController;
    public List<AudioClip> onDragClips;

    public CharacterInformation(Sprite toolSprite, Sprite tombSprite, RuntimeAnimatorController animatorController, List<AudioClip> onDragClips)
    {
        this.toolSprite = toolSprite;
        this.tombSprite = tombSprite;
        this.animatorController = animatorController;
        this.onDragClips = onDragClips;
    }
}

public class SelectableFactory{

    static readonly Dictionary<ActorId, CharacterInformation> sprites = new();

    static SelectableFactory(){
        Sprite adamSprite =  Resources.Load<Sprite>("ToolBox/toolbox_adamgen");
        Sprite eveSprite =  Resources.Load<Sprite>("ToolBox/toolbox_evegen");
        Sprite adamTombSprite =  Resources.Load<Sprite>("Characters/Tomb/tomb_icon_adamgen_normal");
        Sprite eveTombSprite =  Resources.Load<Sprite>("Characters/Tomb/tomb_icon_evegen_normal");  
        RuntimeAnimatorController adamAnimator = Resources.Load<RuntimeAnimatorController>("Characters/adam");
        RuntimeAnimatorController eveAnimator = Resources.Load<RuntimeAnimatorController>("Characters/eve");
        AudioClip maleDrag1 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_male_generic_01");
        AudioClip maleDrag2 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_male_generic_02");
        
        AudioClip femaleDrag1 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_female_generic_01");
        AudioClip femaleDrag2 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_female_generic_02");

        sprites.Add(ActorId.Adam, new CharacterInformation(adamSprite, adamTombSprite, adamAnimator, new List<AudioClip>(){maleDrag1, maleDrag2}));
        sprites.Add(ActorId.Eve, new CharacterInformation(eveSprite, eveTombSprite, eveAnimator, new List<AudioClip>(){femaleDrag1, femaleDrag2}));
    }

    public static GameObject CreateSelectable(Actor actor){
        GameObject selectableCharacterPrefab = Level.Instance.selectableCharacterPrefab;
        GameObject selectableCharacter = GameObject.Instantiate(selectableCharacterPrefab);
        selectableCharacter.GetComponent<CharacterSelectable>().Instanciate(actor, sprites.GetValueOrDefault(actor.GetActorId()));
        return selectableCharacter;
    }
}

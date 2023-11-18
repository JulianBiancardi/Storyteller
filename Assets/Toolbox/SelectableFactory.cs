using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public struct CharacterInformation
{
    public Sprite sprite;
    public AnimatorController animatorController;
    public List<AudioClip> onDragClips;

    public CharacterInformation(Sprite sprite, AnimatorController animatorController, List<AudioClip> onDragClips)
    {
        this.sprite = sprite;
        this.animatorController = animatorController;
        this.onDragClips = onDragClips;
    }
}

public class SelectableFactory{

    static readonly Dictionary<ActorId, CharacterInformation> sprites = new();

    static SelectableFactory(){
        Sprite adamSprite =  Resources.Load<Sprite>("ToolBox/toolbox_adamgen");
        Sprite eveSprite =  Resources.Load<Sprite>("ToolBox/toolbox_evegen");
        AnimatorController adamAnimator = Resources.Load<AnimatorController>("Characters/adam");
        AnimatorController eveAnimator = Resources.Load<AnimatorController>("Characters/eve");
        AudioClip maleDrag1 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_male_generic_01");
        AudioClip maleDrag2 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_male_generic_02");
        
        AudioClip femaleDrag1 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_female_generic_01");
        AudioClip femaleDrag2 = Resources.Load<AudioClip>("ToolBox/sounds/pick_toolbox_actor_female_generic_02");

        sprites.Add(ActorId.Adam, new CharacterInformation(adamSprite, adamAnimator, new List<AudioClip>(){maleDrag1, maleDrag2}));
        sprites.Add(ActorId.Eve, new CharacterInformation(eveSprite, eveAnimator, new List<AudioClip>(){femaleDrag1, femaleDrag2}));
    }

    public static GameObject CreateSelectable(Actor actor){
        GameObject selectableCharacterPrefab = Level.Instance.selectableCharacterPrefab;
        GameObject selectableCharacter = GameObject.Instantiate(selectableCharacterPrefab);
        selectableCharacter.GetComponent<CharacterSelectable>().Instanciate(actor, sprites.GetValueOrDefault(actor.GetActorId()));
        return selectableCharacter;
    }
}

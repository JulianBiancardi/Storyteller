using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject objectToDrop;
    public CharacterType character;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public GameObject getObjectToDrop(){
        return objectToDrop;
    }

    public void instanciate(CharacterType character, Sprite sprite, AnimatorController animatorController){
        this.character = character;
        spriteRenderer.sprite = sprite;
        objectToDrop.GetComponent<Animator>().runtimeAnimatorController = animatorController;
    }
}

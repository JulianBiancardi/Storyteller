using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject objectToDrop;
    public Actor actor;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public GameObject getObjectToDrop(){
        return objectToDrop;
    }

    public void Instanciate(Actor actor, Sprite sprite, AnimatorController animatorController){
        this.actor = actor;
        spriteRenderer.sprite = sprite;
        objectToDrop.GetComponent<Animator>().runtimeAnimatorController = animatorController;
    }
}

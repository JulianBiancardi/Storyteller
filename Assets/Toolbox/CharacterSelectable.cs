using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterSelectable : BasicDraggeable
{
    public GameObject selectionPrefab;
    public CharacterType character;
    public SpriteRenderer spriteRenderer;
    public AnimatorController animatorController;
    private AudioSource audioSource;
    public List<AudioClip> onDragClips = new();

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Instanciate(CharacterType character, CharacterInformation characterInformation)
    {
        this.character = character;
        spriteRenderer.sprite = characterInformation.sprite;
        this.animatorController = characterInformation.animatorController;
        this.onDragClips = characterInformation.onDragClips;
    }

    public override GameObject OnDrag()
    {
        audioSource.clip = onDragClips[UnityEngine.Random.Range(0, onDragClips.Count)];
        audioSource.Play();
        GameObject dragObject = Instantiate(selectionPrefab);
        dragObject.transform.position = transform.position;
        dragObject.transform.localScale = new Vector3(1, 1, 1);
        dragObject.GetComponent<Selection>().instanciate(character, spriteRenderer.sprite, animatorController);
        return dragObject;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public enum ItemType
{
    Set,
    Actor
}

public class ToolItem : BasicDraggeable
{
    public ItemType itemType;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro nameTextMesh;
    private AudioSource audioSource;

    private List<AudioClip> onDragClips;
    public GameObject selectionPrefab;
    private GameObject objectToDrop;
    private Actor actor;
    private FrameSet frameSet;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nameTextMesh = transform.Find("Name").GetComponent<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Instanciate(ItemType itemType, Sprite iconSprite, string name, List<AudioClip> onDragClips, GameObject objectToDrop, Actor actor = null, FrameSet frameSet = FrameSet.None)
    {
        this.itemType = itemType;
        spriteRenderer.sprite = iconSprite;
        nameTextMesh.text = name.ToLower();
        this.onDragClips = onDragClips;
        this.objectToDrop = objectToDrop;
        this.actor = actor;
        this.frameSet = frameSet;
    }

    public override GameObject OnDrag()
    {
        audioSource.clip = onDragClips[UnityEngine.Random.Range(0, onDragClips.Count)];
        audioSource.Play();
        GameObject dragObject = Instantiate(selectionPrefab);
        dragObject.transform.position = transform.position;
        dragObject.transform.localScale = new Vector3(1, 1, 1);
        dragObject.GetComponent<Selection>().Instanciate(itemType, spriteRenderer.sprite, objectToDrop, actor, frameSet);
        return dragObject;
    }
}

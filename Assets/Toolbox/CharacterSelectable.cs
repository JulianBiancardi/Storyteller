using TMPro;
using UnityEngine;

public class CharacterSelectable : BasicDraggeable
{
    public GameObject selectionPrefab;
    public Actor actor;
    public CharacterInformation characterInformation;

    public SpriteRenderer spriteRenderer;
    public TextMeshPro nameTextMesh;
    private AudioSource audioSource;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Instanciate(Actor actor, CharacterInformation characterInformation)
    {
        this.actor = actor;
        this.characterInformation = characterInformation;
        spriteRenderer.sprite = characterInformation.toolSprite;
        nameTextMesh.text = actor.GetName().ToLower();
    }

    public override GameObject OnDrag()
    {
        audioSource.clip = characterInformation.onDragClips[Random.Range(0, characterInformation.onDragClips.Count)];
        audioSource.Play();
        GameObject dragObject = Instantiate(selectionPrefab);
        dragObject.transform.position = transform.position;
        dragObject.transform.localScale = new Vector3(1, 1, 1);
        dragObject.GetComponent<Selection>().Instanciate(actor, characterInformation);
        return dragObject;
    }
}

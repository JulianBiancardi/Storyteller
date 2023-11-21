using UnityEngine;

public class Selection : MonoBehaviour
{
    private ItemType itemType;
    public GameObject objetToDrop;
    public Actor actor;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public GameObject getObjectToDrop(){
        return objetToDrop;
    }

    public void Instanciate(Actor actor, CharacterInformation characterInfo){
        this.actor = actor;
        spriteRenderer.sprite = characterInfo.toolSprite;
        //characterToDrop.GetComponent<Animator>().runtimeAnimatorController = characterInfo.animatorController;
        //characterToDrop.GetComponent<Character>().tombSprite = characterInfo.tombSprite;
    }
    public void Instanciate(ItemType itemType, Sprite icon, GameObject objectToDrop, Actor actor){
        this.itemType = itemType;
        spriteRenderer.sprite = icon;
        this.objetToDrop = objectToDrop;
        this.actor = actor;
    }


    public bool IsSet(){
        return itemType == ItemType.Set;
    }

    public bool IsActor(){
        return itemType == ItemType.Actor;
    }
}

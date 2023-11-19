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

    public void Instanciate(Actor actor, CharacterInformation characterInfo){
        this.actor = actor;
        spriteRenderer.sprite = characterInfo.toolSprite;
        objectToDrop.GetComponent<Animator>().runtimeAnimatorController = characterInfo.animatorController;
        objectToDrop.GetComponent<Character>().tombSprite = characterInfo.tombSprite;
    }
}

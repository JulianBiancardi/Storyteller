using UnityEngine;

public enum Feeling{
    Neutral,
    Love,
    Lonely,
    SadTomb
}

public class Character : BasicDraggeable, Removable
{
    Animator animatorController;
    private Feeling feeling = Feeling.Neutral;
    public Container container;
    public Sprite tombSprite;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private GameObject pupil;
    private GameObject expressionContainer;

    void Awake()
    {
        animatorController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        pupil = transform.Find("Pupil").gameObject;
        expressionContainer = transform.Find("ExpressionContainer").gameObject;
    }

    public void FacingLeft(){
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public override GameObject OnDrag()
    {
        throw new System.NotImplementedException();
    }

    
    public void Death(){
        animatorController.runtimeAnimatorController = null;
        pupil.SetActive(false);
        spriteRenderer.sprite = tombSprite;
    }
    
    public void ChangeFeeling(Feeling newFeeling){
        if(newFeeling == feeling){
            return;
        }
        feeling = newFeeling;

        switch(newFeeling) {
            case Feeling.Lonely:
                animatorController.SetTrigger("isLonely");
                audioSource.clip = Resources.Load<AudioClip>("Sound/Sfx/sad_at_self");
                GameObject expression = Instantiate(Resources.Load<GameObject>("Expression"), expressionContainer.transform);
                expression.transform.localPosition = new Vector3(0.5f, 0, 0);
                break;
            case Feeling.SadTomb:
                animatorController.SetTrigger("isSadTomb");
                break;
            case Feeling.Love:
                animatorController.SetTrigger("isLove");
                break;
            case Feeling.Neutral:
            default:
                animatorController.SetTrigger("isNeutral");
                break;
        }

        audioSource.Play();
    }

    public void OnRemove()
    {
        container.RemoveCharacter();
    }
}

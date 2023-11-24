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
    private ExpressionType lastExpressionType = ExpressionType.None;

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
        Debug.Log("Death");
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

    public void UpdateState(Event e){
        if(e == null){
            return;
        }
        
        if(lastExpressionType == e.expressionInfo.expressionType){
            return;
        }
        lastExpressionType = e.expressionInfo.expressionType;
        expressionContainer.SetActive(false);
        ResetAllTriggers();
        switch(e.expressionInfo.expressionType){
            case ExpressionType.Ke:
                animatorController.SetTrigger("Ke");
                audioSource.clip = Resources.Load<AudioClip>("Sound/Sfx/confused");
                audioSource.Play();
                Debug.Log("Ke");
                break;
            case ExpressionType.Sad_At_Self:
                animatorController.SetTrigger("isLonely");
                audioSource.clip = Resources.Load<AudioClip>("Sound/Sfx/sad_at_self");
                GameObject expression = Instantiate(Resources.Load<GameObject>("Expression"), expressionContainer.transform);
                expression.transform.localPosition = new Vector3(0.5f, 0, 0);
                expressionContainer.SetActive(true);
                audioSource.Play();
                break;
            case ExpressionType.FallingInLove:
                animatorController.SetTrigger("isLove");
                break;
            case ExpressionType.Mourning:
                animatorController.SetTrigger("isSadTomb");
                audioSource.clip = Resources.Load<AudioClip>("Sound/Sfx/heartbroken");
                audioSource.Play();
                break;
            case ExpressionType.TombDeath:
                Death();
                break;
            case ExpressionType.Ghost_Idle:
                animatorController.SetTrigger("Ghost_Idle");
                break;
            case ExpressionType.Shocked_Horrified:
                AudioClip audioClip = Resources.Load<AudioClip>("Sound/Sfx/" + e.source.ToString().ToLower() + "_" + e.expressionInfo.expressionType.ToString().ToLower());
                audioSource.clip = audioClip;
                audioSource.Play();
                animatorController.SetTrigger("Shocked_Horrified");
                break;
            case ExpressionType.Idling:
            default:
                animatorController.SetTrigger("isNeutral");
                break;
        }
    }

    private void ResetAllTriggers(){
        foreach(AnimatorControllerParameter parameter in animatorController.parameters){
            if(parameter.type == AnimatorControllerParameterType.Trigger){
                animatorController.ResetTrigger(parameter.name);
            }
        }
    }
}

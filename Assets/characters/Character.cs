using UnityEngine;

public enum Feeling{
    Happy,
    Sad,
    Angry,
    Scared,
    Surprised,
    Disgusted,
    Neutral,
    Love
}

public class Character : BasicDraggeable
{
    Animator animatorController;
    public CharacterType characterType;
    private Feeling feeling = Feeling.Neutral;
    public Container container;

    void Awake()
    {
        animatorController = GetComponent<Animator>();
    }

    
    public override GameObject OnDrag()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeFeeling(Feeling newFeeling){
        if(newFeeling == feeling){
            return;
        }
        feeling = newFeeling;

        switch(newFeeling) {
            case Feeling.Love:
                animatorController.SetTrigger("isLove");
                break;
            case Feeling.Neutral:
            default:
                animatorController.SetTrigger("isNeutral");
                break;
        }
    }

}

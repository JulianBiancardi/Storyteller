using UnityEngine;

public enum Feeling{
    Neutral,
    Love,
    SadTomb
}

public class Character : BasicDraggeable
{
    Animator animatorController;
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
    }

}

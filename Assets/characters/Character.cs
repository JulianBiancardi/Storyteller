using UnityEngine;

public enum Feeling{
    Happy,
    Sad,
    Angry,
    Scared,
    Surprised,
    Disgusted,
    Neutral
}

public class Character : BasicDraggeable
{
    Animator animatorController;
    private Feeling feeling = Feeling.Neutral;

    void Awake()
    {
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override GameObject OnDrag()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeFeeling(Feeling newFeeling){
        feeling = newFeeling;
        if(newFeeling == Feeling.Sad){
            animatorController.SetTrigger("isSad");
        }
    }

}
